namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        public async Task<bool> IsValidImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {

                stream.Seek(0, SeekOrigin.Begin);

                List<string> jpg = new List<string> { "FF", "D8" };
                List<string> bmp = new List<string> { "42", "4D" };
                List<string> gif = new List<string> { "47", "49", "46" };
                List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
                List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

                List<string> bytesIterated = new List<string>();

                for (int i = 0; i < 8; i++)
                {
                    string bit = stream.ReadByte().ToString("X2");
                    bytesIterated.Add(bit);

                    bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                    if (isImage)
                    {
                        return true;
                    }
                }

                return false;

                //try
                //{
                //    using (var image = Image.FromStream(stream))
                //    {
                //        return true;
                //    }
                //}
                //catch
                //{
                //    return false;
                //}
            }
        }

        public async Task<string> Sanitize(string input)
        {
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Clear();
            sanitizer.KeepChildNodes = true;
            var sanitizedText = sanitizer.Sanitize(input);

            return sanitizedText;
        }
    }
}
