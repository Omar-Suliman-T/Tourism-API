namespace Tourist.API.Services.UploadService
{
    public class UploadService
    {
        private readonly IWebHostEnvironment _env;

        public UploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadReviewImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(
                _env.WebRootPath,
                "uploads",
                "reviews"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/reviews/{fileName}";
        }
    }
}
