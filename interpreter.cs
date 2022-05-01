using static translator_bot.Config;
using System.Net;
using System.Collections.Specialized;
using xNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using Nancy.Json;
using System.Text.Json;

namespace translator_bot
{
    internal class interpreter
    {
       
        

        public async Task yandextranslatAsync(string textToTranslate, string langTo)
        {
            
            // Формируем json параметры запроса
            string json = "{" +
                string.Format(@"""folder_id"": ""{0}"",
                               ""texts"": [""{1}""],
                               ""targetLanguageCode"": ""{2}""
                               ", folder_id, textToTranslate,langTo).Trim()
                               
                        + "}";



            var request = (HttpWebRequest)WebRequest.Create(yandex);

            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Bearer " + Iam;

            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            // Делаем запрос и получаем ответ сервера
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
      

                using (StreamReader reader2 = new StreamReader(responseStream))
                {
                    Config config = new Config();
                    string text1 = reader2.ReadToEnd();

                    dynamic stuff = JsonConvert.DeserializeObject(text1);
                    config.Result  = stuff.translations[0].text;
                    

                }
                
                
            }
        }
    }
}


