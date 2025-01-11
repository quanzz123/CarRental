using elFinder.NetCore;
using SlugGenerator;
using System.Security.Cryptography;
using System.Text;
namespace CarRental.Utilities
{
    public class Function
    {
        public static int _AccountId = 0;
        public static string _UserName = String.Empty;
        public static string _Email = String.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;
        public static string _address = String.Empty;
        public static string _Phone = String.Empty;
        //public static string CART_KEY = "MYCART";
        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strbui = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strbui.Append(result[i].ToString("x2"));
            }
            return strbui.ToString();
        }
        public static string MD5Password(string? text)
        {
            string str = MD5Hash(text);
            //lap 5 lan ma hoa xau dam bao tinh bao mat
            //moi lan laao nhan doi xau, o giua them "_"
            for (int i = 0; i <= 5; i++)
            {
                str = MD5Hash(str + "_" + str);
            }
            return str;
        }
        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Function._UserName) || string.IsNullOrEmpty(Function._Email) || (Function._AccountId <= 0))
            {
                return false;
            }
            return true;     
        }
        /* public static string UploadImage(IFormFile image, string folder)
         {
             try
             {
                 var FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder, image.FileName);
                 using (var myfile = new FileStream(FullPath, FileMode.CreateNew))
                 {
                     image.CopyTo(myfile);
                 }
                 return image.FileName;
             } catch(Exception ex)
             {
                 return string.Empty;
             }


         }*/

        public static string UploadImage(IFormFile image, string folder)
        {
            try
            {
                // Tạo tên file duy nhất bằng cách thêm timestamp
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var extension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{extension}";

                // Xây dựng đường dẫn đầy đủ để lưu file
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folder, uniqueFileName);

                // Kiểm tra và tạo thư mục nếu chưa tồn tại
                var directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Lưu file vào thư mục
                using (var myFile = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyTo(myFile);
                }

                // Trả về tên file duy nhất
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return string.Empty;
            }
        }

    }
}
