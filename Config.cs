using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator_bot
{
    public class Config
    {
        public static string Token  = "токен бота";
        public static string Iam = "Iam токен";

        public static string yandex = @"https://translate.api.cloud.yandex.net/translate/v2/translate";

        public static string folder_id = "индефикатор каталога";

        public static string result { get; set; }
        
        public static string search2 { get; set; }

        public string Searcn2
        {
            get { return search2; }
            set { search2 = value; }    
        }

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public static string x1 { get; set; }

        public string X1
        {
            get { return x1; }  
            set { x1 = value; }
        }

        public static bool xset = false;

    }
}
