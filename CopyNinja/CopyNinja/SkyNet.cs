using RestSharp;
using RestSharp.Authenticators;
using System.Linq;
using System;
using System.Text;
using RestSharp.Extensions;
using System.IO;

namespace Implementation
{
    public class SkyNet
    {
        public static SkyNetPostData Copy(string url, string username, string password, string filename, string localPath)
        {
            var data = SkyNetPost(url, username, password, filename, localPath);

            return SimpleJson.SimpleJson.DeserializeObject<SkyNetPostData>(data);
        }

        public static void Paste(string url, string username, string password, string skylink)
        {
            SkyNetGet(url, username, password, skylink);
        }

        private  static string SkyNetPost(string url, string username, string password, string filename, string localPath)
        {
            var client = new RestClient(url)
            {
                UserAgent = "Sia-Agent",
                Authenticator = new HttpBasicAuthenticator(username, password)
            };

            var request = new RestRequest()
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            request.Resource = "/clipboard/{filename}";
            request.AddParameter("filename", filename, ParameterType.UrlSegment);
            request.AddParameter("force", true, ParameterType.QueryString);
            request.AddFile("file", localPath);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return string.Empty;

            return response.Content;
        }

        private static void SkyNetGet(string url, string username, string password, string skylink)
        {
            var client = new RestClient(url)
            {
                UserAgent = "Sia-Agent",
                Authenticator = new HttpBasicAuthenticator(username, password)
            };

            var request = new RestRequest()
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            request.Resource = "/{sialink}";
            request.AddQueryParameter("attachment", "false");
            request.AddParameter("sialink", skylink, ParameterType.UrlSegment);

            var response = client.Execute(request);

            var SkyNetGetData = SimpleJson.SimpleJson
                                          .DeserializeObject<SkyNetGetData>(response.Headers
                                                                                    .First(element => element.Name == "Skynet-File-Metadata")
                                                                                    .Value.ToString());

            File.WriteAllBytes(SkyNetGetData.filename, response.RawBytes);
        }







    }
}
