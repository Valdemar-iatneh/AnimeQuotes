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
            string Word = Console.ReadLine();
            var url = $"https://animechan.vercel.app/api/quotes/anime?title={Word.ToLower()}";
            //https://animechan.vercel.app/api/random
            //https://animechan.vercel.app/api/available/anime
            //}
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
                //Console.WriteLine($"Anime:      {Word}");  <Dictionary<string, Data>>(source);
                //Console.WriteLine($"Character:  {animeQuote.Character}");
                //Console.WriteLine($"Quote:      {animeQuote.Quote}");

                //var name = animeQuote.List[0].name;

                foreach (var data in animeQuote)
                {
                    Console.WriteLine($"Anime:      {Word}");
                    Console.WriteLine($"Character:  {data.Character}");
                    Console.WriteLine($"Quote:      {data.Quote}");
                    Console.WriteLine();
                }

            }
            //Darker than Black
            //Beck
        }


    }
}
