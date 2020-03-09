using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text;

namespace Implementation
{
    public class SkyNet
    {

        public static void SkyNetPost(string url, string username, string password, string filename, string localPath)
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
            request.AddFile("file", localPath);

            var response = client.Execute(request);
        }

        public static void SkyNetGet(string url, string username, string password, string skylink)
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

        }







    }
}
