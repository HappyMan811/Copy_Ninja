using Implementation;
using System.Collections.Generic;
using System.Web.Http;

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
            var content = SkyNet.Copy("https://siasky.net/skynet/skyfile/", "clipboard", value);

            return content;
        }

        // PUT api/demo/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/demo/5 
        public void Delete(int id)
        {
        }
    }

}
