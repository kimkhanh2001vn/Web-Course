using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Common
{
    public static class StringHelper
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

        public static string UpperCase(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                    {
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    }
                    else
                    {
                        result += word.ToUpper() + " ";
                    }
                }

            }
            return result.Trim();
        }

        public static string ShortArticle(string s, int x)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            string result = "";

            //lấy danh sách các từ  
            //string str =  HttpUtility.HtmlDecode(s);
            string[] words = s.Split(' ');


            if (x <= words.Count())
            {
                for (int i = 0; i < x; i++)
                {
                    result += words[i] + " ";
                }
                result += "...";
            }
            else
            {
                for (int i = 0; i < words.Count(); i++)
                {
                    result += words[i] + " ";
                }

            }
            return result.Trim();
        }

        public static string ToAliasWithRandomNumber(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                Random r = new Random();
                input = input.Trim();
                input = input.Replace(" ", "-");
                Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
                string str = input.Normalize(NormalizationForm.FormD);
                str = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D').ToLower();
                str = Regex.Replace(str, @"([^0-9a-z-])", "");
                str = Regex.Replace(str, @"([-]{2,})", "-");
                str = str + "-"; //+ r.Next(10000, 1000000);
                str = str.Substring(0, str.Length-1);

                return str;
            }
            return input;
        }

        public static string ToAlias(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                Random r = new Random();
                input = input.Trim();
                input = input.Replace(" ", "-");
                Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
                string str = input.Normalize(NormalizationForm.FormD);
                str = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D').ToLower();
                str = Regex.Replace(str, @"([^0-9a-z-])", "");
                str = Regex.Replace(str, @"([-]{2,})", "-");                
                return str;
            }
            return input;
        }

        public static string CalculateDateTime(DateTime postDateTime,int hoursLimit)
        {
            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(postDateTime);            
            if(span.TotalHours > hoursLimit || span.TotalHours < 0)
            {
                return postDateTime.ToString("HH:mm dd/MM/yyyy");
            }
            if(span.TotalHours < 1)
            {
                return Math.Floor(span.TotalMinutes) + " phút trước";
            }
            return Math.Floor(span.TotalHours) + " giờ trước";
        }
    }
}
