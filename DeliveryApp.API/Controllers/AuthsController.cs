using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DataLayers.Entities.Enums;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Helpers;
using DeliveryApp.API.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailSender;

        public AuthsController(DataContext context, IConfiguration _config, IEmailService emailSender)
        {
            _context = context;
            this._config = _config;
            _emailSender = emailSender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register registerDto)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(passwordSalt);
            }

            byte[] passwordHashByte = KeyDerivation.Pbkdf2(
            password: registerDto.Password,
            salt: passwordSalt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);

            string passwordHash = Convert.ToBase64String(passwordHashByte);

            Auth newAuthForUser = new Auth()
            {
                Email = registerDto.Email,
                PasswordHash = passwordHashByte,
                PasswordSalt = passwordSalt,

            };

            User newUser = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Address = registerDto.Address,
                City = registerDto.City,
                Role = UserRole.User,
                PhoneNumber = registerDto.PhoneNumber,

            };

            await _context.Users.AddAsync(newUser);
            await _context.Auths.AddAsync(newAuthForUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "The new User Was Created!", Auth = newAuthForUser, });
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginConfirmation login)
        {
            var authExist = _context.Auths.FirstOrDefault(u => u.Email == login.Email);
            if (authExist == null)
            {
                throw new Exception("The user dosent exist");
            }

            byte[] hashedPassword = KeyDerivation.Pbkdf2(
                password: login.Password,
                salt: authExist.PasswordSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            );

            if (!hashedPassword.SequenceEqual(authExist.PasswordHash))
            {
                return Unauthorized("Password is incorrect");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            var token = GenerateToken(user);


            return Ok(new { Message = $"Login Success for {user.Email} !", Token = token });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            return Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPassword.Email);
            if (user == null)
            {
                throw new Exception("user dosent exist");
            }

            var token = await GenerateToken(user);

            var resetLink = $"http://localhost:3000/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";
            var htmlBody = @"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <style>
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }
            .container {
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                background-color: #ffffff;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }
            .header {
                background-color: #4CAF50;
                color: white;
                text-align: center;
                padding: 10px 0;
            }
            .content {
                padding: 20px;
            }
            .footer {
                text-align: center;
                padding: 10px;
                background-color: #f4f4f4;
                font-size: 12px;
                color: #666666;
            }
            .button {
                display: inline-block;
                padding: 10px 20px;
                margin: 20px 0;
                font-size: 16px;
                color: #ffffff;
                background-color: #4CAF50;
                text-decoration: none;
                border-radius: 5px;
            }
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1>DeliveryApp</h1>
            </div>
            <div class='content'>
                <h2>Reset Your Password</h2>
                <p>Hello,</p>
                <p>We received a request to reset your password. Click the button below to reset your password:</p>
                <a href='" + resetLink + @"' class='button'>Reset Password</a>
                <p>If you did not request a password reset, please ignore this email or contact support if you have questions.</p>
                <p>Thanks,<br>The DeliveryApp Team</p>
            </div>
            <div class='footer'>
                <p>&copy; 2024 DeliveryApp. All rights reserved.</p>
            </div>
        </div>
    </body>
    </html>";
            Mailrequest mailrequest = new Mailrequest()
            {
                ToEmail = user.Email,
                Subject = "ForgotPassword",
                Body = htmlBody
            };
            _emailSender.SendEmailAsync(mailrequest);
            return Ok("Email was sent!");
            /*var resetLink = $"http://";*/
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == resetPasswordDto.Email);
            if (user is null)
            {
                throw new Exception("The user dosent exist");
            }

            var userResetPasswordValues = new ResetPasswordDto()
            {
                Email = user.Email,
                NewPassword = resetPasswordDto.NewPassword,
                ConfirmNewPassword = resetPasswordDto.NewPassword,
                Token = resetPasswordDto.Token

            };
            var isValidToken = await ResetPasswordAsync(userResetPasswordValues);
            return Ok(isValidToken);
        }


        private async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("TokenKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IActionResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordDto.Email);
            if (user is null)
            {
                throw new Exception("User does not exist");
            }

            var decodedTokenBytes = WebUtility.UrlDecode(resetPasswordDto.Token);
            /*            var token = Encoding.UTF8.GetString(decodedTokenBytes);*/

            // Compare the decoded token with the one generated for the user
            Console.WriteLine(decodedTokenBytes);
            if (!decodedTokenBytes.Equals(resetPasswordDto.Token))
            {
                throw new Exception("The tokens are not the same");
            }

            // Generate a new password salt
            byte[] passwordSalt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(passwordSalt);
            }

            // Generate the new password hash
            byte[] passwordHashedInBytes = KeyDerivation.Pbkdf2(
                password: resetPasswordDto.NewPassword,
                salt: passwordSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            );

            Auth? auth = await _context.Auths.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (auth is null)
            {
                throw new Exception("user dosent exist");
            }

            auth.PasswordSalt = passwordSalt;
            auth.PasswordHash = passwordHashedInBytes;
            await _context.SaveChangesAsync();

            return Ok("Password has been reset successfully!");
        }
    }
}
