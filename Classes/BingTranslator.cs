using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace Classes
{
    public class BingTranslator
    {
        public class Translated
        {
            public string DetedtedLanguage { get; set; }
            public string Text { get; set; }
            public Translated()
            {

            }
            public Translated(ResultTranslating t)
            {
                DetedtedLanguage = t.detectedLanguage["language"];
                Text = t.translations[0].text;
            }
        } 
        public class ResultTranslating
        {
            public class DetectedLanguage
            {
                public string language { get; set; }
                public int score { get; set; }
            }
            public class Translanions
            {
                public class Len
                {
                    public List<int> srcSentLen { get; set; }
                    public List<int> transSentLen { get; set; }
                }
                public Len sentLen { get; set; }
                public string text { get; set; }
                public string to { get; set; }
            }
            public List<Translanions> translations { get; set; }
            public Dictionary<string,string> detectedLanguage { get; set; }
        }
        private string ig;
        private string cid;
        private string link;
        private HttpClient webClient;
        private CookieContainer cookieContainer;
        public BingTranslator()
        {
            HttpClientHandler handler = null;
            cookieContainer = new CookieContainer();
            handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
                //Proxy = new WebProxy("127.0.0.1", 8888)
            };

            webClient = new HttpClient(handler);
            webClient = new HttpClient();
            string page = webClient.GetStringAsync("https://www.bing.com/translator ").GetAwaiter().GetResult();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            var nodes = doc.DocumentNode.SelectNodes("//script");
            string str = nodes[1].InnerText;
            str = str.Remove(0,str.IndexOf('{'));
            str = str.Remove(str.IndexOf('}')+1);
            str = str.Remove(str.IndexOf("ST"), str.IndexOf(')')+1);
            Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            ig = htmlAttributes["IG"];
            cid = htmlAttributes["CID"];
            link = "https://www.bing.com/ttranslatev3?isVertical=1&&IG="+ig+"&IID=translator.5026.4 ";
        }
        public Translated Translate(string text)
        {
            var tRez = new Translated( translateAdmin(text));
            return tRez;
        }
        private ResultTranslating translateAdmin(string text,string fromLang="ru", string to="uk")
        {
            var requestData = new Dictionary<string, string>();
            requestData["fromLang"] = fromLang;
            requestData["text"] = text;
            requestData["to"] = to;
            HttpResponseMessage response = null;
            response = webClient.PostAsync(link, new FormUrlEncodedContent(requestData)).GetAwaiter().GetResult();
            string rez = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            List<ResultTranslating> result = JsonConvert.DeserializeObject<List<ResultTranslating>>(rez);
            return result[0];
        }
    }
}
