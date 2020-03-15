using RestSharp;
using System;
using System.IO;
using System.Linq;

namespace Implementation
{
    public class SkyNet
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="filename"></param>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static string Copy(string url, string filename, string localPath)
        {
            var Jsondata = SkyNetPost(url, filename, localPath);

            var PostData = SimpleJson.DeserializeObject<SkyNetPostData>(Jsondata);

            return PostData.skylink;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="skylink"></param>
        public static Tuple<string,byte[]> Paste(string url, string skylink)
        {
            return SkyNetGet(url, skylink);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="filename"></param>
        /// <param name="localPath"></param>
        /// <returns></returns>
        private static string SkyNetPost(string url, string filename, string localPath)
        {
            var client = new RestClient(url)
            {
                UserAgent = "Sia-Agent"
            };

            var request = new RestRequest()
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            request.Resource = "/clipboard/{filename}";
            request.AddParameter("filename", filename, ParameterType.UrlSegment);
            request.AddParameter("force", true, ParameterType.QueryString);
            request.AddParameter("authenticate-api", false, ParameterType.UrlSegment);
            request.AddFile("file", localPath);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return string.Empty;

            return response.Content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="skylink"></param>
        private static Tuple<string, byte[]> SkyNetGet(string url, string skylink)
        {
            var client = new RestClient(url)
            {
                UserAgent = "Sia-Agent"
            };

            var request = new RestRequest()
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            request.Resource = "/{sialink}";
            request.AddQueryParameter("attachment", "false");
            request.AddParameter("authenticate-api", false, ParameterType.UrlSegment);
            request.AddParameter("sialink", skylink, ParameterType.UrlSegment);

            var response = client.Execute(request);

            var SkyNetGetData = SimpleJson.DeserializeObject<SkyNetGetData>(response.Headers
                                                                                    .First(element => element.Name == "Skynet-File-Metadata")
                                                                                    .Value.ToString());

            return new Tuple<string, byte[]>(SkyNetGetData.filename, response.RawBytes);
        }

    }
}
