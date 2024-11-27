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
    }
}
