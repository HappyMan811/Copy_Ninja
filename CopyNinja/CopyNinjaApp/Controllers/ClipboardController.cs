using Implementation;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Windows.Forms;

namespace CopyNinjaApp.Controllers
{
    public class ClipboardController : ApiController
    {
        // GET api/demo 
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

        // GET api/demo/5 
        public string Get(int id)
        {
            return "Hello, World!";
        }

        // POST api/demo 
        public string Post([FromBody]string value)
        {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            var content = SkyNet.Copy($"{config.SkynetUrlPost}", "clipboard", value);

            var client = new RestClient($"http://localhost:54085/api/clipboard")
            {
                UserAgent = "Sia-Agent"
            };

            var request = new RestRequest()
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(content);

            var response = client.Execute(request);
            
            return content;
        }

        // PUT api/demo/5 
        public void Put([FromBody]string value)
        {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            var tuple = SkyNet.Paste($"{config.SkynetUrlGet}", value);

            Clipboard.SetData(DataFormats.FileDrop, tuple.Item2);

        }

        // DELETE api/demo/5 
        public void Delete(int id)
        {
        }
    }

}
