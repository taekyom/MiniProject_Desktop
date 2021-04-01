using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNaverMovieFinder1
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientID = "QaMNyOF2DDPCUiXXrFPb";
            string clientSecret = "BZB8i_mI35";
            string search = "starwars"; //변경가능
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?query={search}";

            string responseJson = GetOpenAPIResult(openApiUrl, clientID, clientSecret);
            JObject parseJson = JObject.Parse(responseJson);

            int total = Convert.ToInt32(parseJson["total"]);
            Console.WriteLine($"총 검색결과 : {total}");
            int display = Convert.ToInt32(parseJson["display"]);0
            Console.WriteLine($"화면 출력 : {display}");

            JToken items = parseJson["items"];
            JArray json_array = (JArray)items;

            foreach (var item in json_array)
            {
                Console.WriteLine($"{item["title"]}/{item["image"]}/{item["subtitle"]}/{item["actor"]}");
            }
        }
        private static string GetOpenAPIResult(string openApiUrl, string clientID, string clientSecret)
        {
            string result = "";

            try
            {
                WebRequest request = WebRequest.Create(openApiUrl);
                request.Headers.Add("X-Naver-Client-Id", clientID);
                request.Headers.Add("X-Naver-Client-Secret", clientSecret);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                result = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외발생 : {ex}");
            }
            return result;
        }
    }
}
