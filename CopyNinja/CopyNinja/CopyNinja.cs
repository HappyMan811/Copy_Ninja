using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text;

namespace CopyNinja
{
    public class CopyNinja
    {

        public static void Copy(string url, string username,string password,string filename, string localPath)
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







    }
}
