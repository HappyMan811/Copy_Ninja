using Implementation;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Web.Http;

namespace CopyNinjaApp.Controllers
{
    public class ClipboardController : ApiController
    {        

        public string Get(string folder)
        {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            var value = File.ReadAllText(@"clipboard\skyclipboard.txt");

            var tuple = SkyNet.Paste($"{config.SkynetUrlGet}", value);

            File.WriteAllBytes($@"{folder}\{tuple.Item1}", tuple.Item2);

            return $@"{folder}\{tuple.Item1}";
        }
       
        public string Post([FromBody]string value)
        {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            var content = SkyNet.Copy($"{config.SkynetUrlPost}", "clipboard", value);

            foreach (var peer in config.Peers)
            {
                var client = new RestClient($"{peer}/api/clipboard")
                {
                    UserAgent = "Sia-Agent"
                };

                var request = new RestRequest()
                {
                    Method = Method.PUT,
                    RequestFormat = DataFormat.Json
                };

                request.AddJsonBody(content);

                var asd = client.Execute(request);
            }          
            
            return content;
        }
        
        public void Put([FromBody]string value)
        {
            if (!Directory.Exists("clipboard"))
            {
                Directory.CreateDirectory("clipboard");
            }

            File.WriteAllText(@"clipboard\skyclipboard.txt", value);            
        }
    }

}
