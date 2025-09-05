namespace EMS_API.Helper
{
    public static class UploadFile
    {
        public static string UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null!;


            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "employees");
            


            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uniqueFileName;
        }

        public static string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }


    }


}


