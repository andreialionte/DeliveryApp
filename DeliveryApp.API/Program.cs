using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using DeliveryApp.API.DataLayers;
using DeliveryApp.API.Helpers;
using DeliveryApp.API.Repository;
using DeliveryApp.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opts.OperationFilter<SecurityRequirementsOperationFilter>(); //for the role based to read the Authorize addnotation
});

builder.Services.AddDbContext<DataContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

//email configuration
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
/*builder.Services.AddAzureClients(b =>
{
    b.AddBlobServiceClient(builder.Configuration.GetSection("Azure:ConnectionString").Value);
});*/

var awsSettings = builder.Configuration.GetSection("AWS");
var credential = new BasicAWSCredentials(awsSettings["AccessKeyId"], awsSettings["SecretAccessKey"]);

var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = credential;
awsOptions.Region = RegionEndpoint.USEast1;
builder.Services.AddDefaultAWSOptions(awsOptions);

builder.Services.AddAWSService<IAmazonS3>();


builder.Services.AddTransient<IFileService, FileService>();

//di for the repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenKey").Value))
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //always this first and after , add authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
