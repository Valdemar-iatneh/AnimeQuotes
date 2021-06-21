using System;
using System.IO;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Net;
using Newtonsoft.Json;

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
                var Word = arg.Message.Text;
                if (Word == "/start")
                {
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Все доступные аниме");
                      
                    var url1 = $"https://animechan.vercel.app/api/available/anime";
                    var request1 = WebRequest.Create(url1);

                    var response1 = request1.GetResponse();
                    var httpStatusCode1 = (response1 as HttpWebResponse).StatusCode;

                    if (httpStatusCode1 != HttpStatusCode.OK)
                    {
                        Console.WriteLine(httpStatusCode1);
                        return;
                    }

                    using (var streamReader = new StreamReader(response1.GetResponseStream()))
                    {
                        string result = streamReader.ReadToEnd();
                        Console.WriteLine(result);
                        bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Все доступные аниме: {result}");
                    }
                    
                    return;
                }

                var url = $"https://animechan.vercel.app/api/quotes/anime?title={Word.ToLower()}";
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {Word}");
                var request = WebRequest.Create(url);

                var response = request.GetResponse();
                var httpStatusCode = (response as HttpWebResponse).StatusCode;

                if (httpStatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine(httpStatusCode);
                    return;
                }

                try
                {
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

                catch
                {
                    Console.WriteLine("Это слово некоректно, или не имеет синонимов");
                    Console.WriteLine("Если не уверенны в правильности написания аниме, введите команду /help");
                }           

            };

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}
