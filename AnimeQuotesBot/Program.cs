using System;
using System.IO;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AnimeQuotesBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient("1815279973:AAEGH0B0vOXv039H0DXgbM6vx_DcAbSNUrA");
            //var bot = new TelegramBotClient(File.ReadAllText("C:\\Work\\token.txt"));

            bot.OnMessage += (s, arg) =>
            {
                var anime_name = arg.Message.Text;
                if (anime_name == "/start")
                {
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Enter the name of the anime");
                      
                  //var url1 = $"https://animechan.vercel.app/api/available/anime";
                  //var request1 = WebRequest.Create(url1);
                  //
                  //var response1 = request1.GetResponse();
                  //var httpStatusCode1 = (response1 as HttpWebResponse).StatusCode;
                  //
                  //if (httpStatusCode1 != HttpStatusCode.OK)
                  //{
                  //    Console.WriteLine(httpStatusCode1);
                  //    return;
                  //}
                  //
                  //using (var streamReader = new StreamReader(response1.GetResponseStream()))
                  //{
                  //    string result = streamReader.ReadToEnd();
                  //    //Console.WriteLine(result);
                  //    var animeTitles = JsonConvert.DeserializeObject<Dictionary<int, FormTitles>>(result);
                  //    for (int i = 0; i < animeTitles.Count; i++)
                  //    {
                  //        //Console.WriteLine(animeTitles.Values);
                  //    }
                  //    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Все доступные аниме: {result}");
                  //}
                  //
                  return;
                }
               

                try
                {
                    var animeQuote = GetAnimeQuotes(anime_name);   
                    foreach (var data in animeQuote)
                    {
                        bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Anime:      {anime_name}");
                        bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Character:  {data.Character}");
                        bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Quote:      {data.Quote}");
                    }
                }

                catch
                {
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, "Incorrect name");
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, "If you are not sure about the spelling of the anime, enter the command /help");
                }           

            };

            bot.StartReceiving();

            Console.ReadKey();
        }

        static List<Form> GetAnimeQuotes(string anime_name)
        {
            var url = $"https://animechan.vercel.app/api/quotes/anime?title={anime_name.ToLower()}";

            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var animeQuote = JsonConvert.DeserializeObject<List<Form>>(result);
                return animeQuote.ToList();
            }
        }

        static void GetAnimeTitles()
        {
            var url = $"https://animechan.vercel.app/api/available/anime";

            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var animeTitles = JsonConvert.DeserializeObject<FormTitles>(result);
 //              return animeTitles.ToList();
            }
        }
    } 
}
