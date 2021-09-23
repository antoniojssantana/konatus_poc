using System;
using System.IO;

namespace sga.utils.Services
{
    public static class Upload
    {
        public static bool UploadFile(string folder, string file, string fileName)
        {
            var imageByteArray = Convert.FromBase64String(file.Split(",")[1].ToString());
            if (string.IsNullOrEmpty(file))
            {
                throw new Exception("Forneça o arquivo para Upload");
            }
            var filePath = Path.Combine(folder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                throw new Exception("Forneça o arquivo para Upload");
            }
            Directory.CreateDirectory(folder);
            File.WriteAllBytes(filePath, imageByteArray);
            return true;
        }
    }
}