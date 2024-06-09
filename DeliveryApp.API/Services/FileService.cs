using Amazon.S3;
using Amazon.S3.Model;

namespace DeliveryApp.API.Services
{
    public class FileService : IFileService
    {
        private readonly IAmazonS3 _s3Client;

        public FileService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> Upload(IFormFile file, string directory)
        {
            string bucketName = "deliveryapp24";
            string fileKey = directory + file.FileName;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position to the beginning

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileKey,
                    InputStream = memoryStream,
                    ContentType = file.ContentType,
                    /*                    CannedACL = S3CannedACL.PublicRead // Ensure the uploaded file is public*/
                };

                var response = await _s3Client.PutObjectAsync(putRequest);

                var url = $"https://{bucketName}.s3.amazonaws.com/{fileKey}";
                return url;
            }
        }
    }
}


// IN AWS S3 u need to change the permissions :
// {
/*"Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "PublicReadGetObject",
      "Effect": "Allow",
      "Principal": "*",
      "Action": "s3:GetObject",
      "Resource": "arn:aws:s3:::deliveryapp24/*"
    }
  ]
}*/
