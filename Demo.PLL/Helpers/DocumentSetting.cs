using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Demo.PL.Helpers
{
    
    public static class DocumentSetting
    {
        public static async Task<string> UplodeFile(IFormFile file, string FolderName) 
        {
            //File Loaction
            //string FileLoaction = @$"D:\Revision\C#_Rev\FirstProjectMVC_Se_3_Part_1\Demo.PLL\wwwroot\Files\Images\";
            //string FileLocation = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\Images\\";
            string FileLocation = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            if (!Directory.Exists(FileLocation))
            {
               Directory.CreateDirectory(FileLocation);
            }

            // Must Be Unique
          string FileName = $"{Guid.NewGuid()}{file.FileName}";

          string FilePath = Path.Combine(FileLocation, FileName);

            //save file as steming 
          using var filestreming = new FileStream(FilePath,FileMode.Create);

           await  file.CopyToAsync(filestreming);

            return FileName;
        }


        public static void DeleteFile(string FileName,string FolderName) 
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName,FileName);

            if(File.Exists(FilePath)) 
            {
                File.Delete(FilePath);
            }
        }
    }
}
