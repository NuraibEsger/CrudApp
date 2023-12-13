﻿namespace WebApplication1.Services
{
    public class FileService
    {
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot","img"));

        public string UploadFile(IFormFile file)
        {
            if (!Directory.Exists(_path)) { Directory.CreateDirectory(_path); }


            FileInfo fileInfo = new FileInfo(file.FileName);
            var imageName = file.FileName + Guid.NewGuid().ToString() + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(_path, imageName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return imageName;
        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_path, fileName);

            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
        }
    }
}
