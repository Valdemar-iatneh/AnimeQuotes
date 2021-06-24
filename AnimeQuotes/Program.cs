using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace AnimeQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            string anime_name = Console.ReadLine();
            var url = $"https://animechan.vercel.app/api/quotes/anime?title={anime_name.ToLower()}";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                //Console.WriteLine(result);
                var animeQuote = JsonConvert.DeserializeObject<List<Form>>(result);

                foreach (var data in animeQuote)
                {
                    Console.WriteLine($"Anime:      {anime_name}");
                    Console.WriteLine($"Character:  {data.Character}");
                    Console.WriteLine($"Quote:      {data.Quote}");
                    Console.WriteLine();
                }
            }
        }
    }
}
