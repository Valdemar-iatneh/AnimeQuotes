using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace AnimeQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            string Word = Console.ReadLine();
            var url = $"https://animechan.vercel.app/api/random";
            //
            //https://animechan.vercel.app/api/quotes/anime?title={Word.ToLower()}
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
                var animeQuote = JsonConvert.DeserializeObject<Form>(result);
                Console.WriteLine($"Anime:      {animeQuote.Anime}");
                Console.WriteLine($"Character:  {animeQuote.Character}");
                Console.WriteLine($"Quote:      {animeQuote.Quote}");
            }

        }
    }
}
