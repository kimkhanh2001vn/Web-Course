using System.Security.Cryptography;
using System.Text;
using TMDT;

namespace Common
{
    public class Encryptor
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string SimpleEncrypt(string pw)
        {
            if (!string.IsNullOrEmpty(pw))
            {
                byte[] stringCode = Encoding.ASCII.GetBytes(pw);
                pw = (stringCode[0] + 3).ToString();
                for (int x = 1; x < stringCode.Length; x++)
                {
                    pw += ',';
                    if (x < 4)
                    {
                        stringCode[x] += 3;
                    }

                    if (x >= 4 && x < 7)
                    {
                        stringCode[x] += 1;
                    }

                    if (x >= 7)
                    {
                        stringCode[x] += 4;
                    }

                    pw += stringCode[x];
                }
            }
            return pw;
        }
        public static string SimpleDecrypt(string pw)
        {
            if (!string.IsNullOrEmpty(pw))
            {
                string[] splitResult = pw.Split(',');
                pw = "";
                for (int x = 0; x < splitResult.Length; x++)
                {
                    if (int.TryParse(splitResult[x], out int value))
                    {
                        if (x < 4)
                        {
                            value -= 3;
                        }

                        if (x >= 4 && x < 7)
                        {
                            value -= 1;
                        }

                        if (x >= 7)
                        {
                            value -= 4;
                        }

                        pw += (char)value;
                    }
                }
            }
            return pw;
        }

        public static string HashAdminPassword(string userName,string password)
        {
            return Common.Encryptor.MD5Hash(userName + password);
        }
    }
}