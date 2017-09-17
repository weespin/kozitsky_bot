using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Kazitsky_Bot
{
    internal class Program

    {
        private static string token;
        public const long GruppaId = -228945165;
        public static List<Day> Days = new List<Day>();
        public static List<Uchenik> Users = new List<Uchenik>();
        public static TelegramBotClient Bot;

        public static void Load()
        {
            var failed = false;
            if (Directory.Exists("./config"))
            {
                if (File.Exists("./config/user.json"))
                    Users = JsonConvert.DeserializeObject<List<Uchenik>>(File.ReadAllText("./config/user.json"));
                else
                    throw new Exception("No user file");
                if (File.Exists("./config/weeks.json"))
                    Days = JsonConvert.DeserializeObject<List<Day>>(File.ReadAllText("./config/weeks.json"));
                else
                    throw new Exception("No weeks file");
                if (File.Exists("./config/token.txt"))
                    token = File.ReadAllText("./config/token.txt");
                else
                    throw new Exception("No token file");
            }
        }

        private static async Task MainAsync(string[] args)
        {
            Bot = new TelegramBotClient(token);
        }

        public static void Main(string[] args)
        {
            Load();
            MainAsync(args).Wait();
            Bot.OnMessage += BotOnMessageReceived;
            Bot.StartReceiving();
            WakeUp.SetTimer();
            Days = JsonConvert.DeserializeObject<List<Day>>(File.ReadAllText("./config/weeks.json"));
            Console.WriteLine("Loaded!");
            while (true)
            {
            }
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Chat.Id != GruppaId)
            {
                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Ти не курсант моєї улюбленої роти.");

                return;
            }
            if (message == null || message.Type != MessageType.TextMessage) return;

            if (message.Text.StartsWith("/naryad")) // read if gay
            {
                var b = message.Text;
                b = b.Remove(0, b.IndexOf(' ') + 1);

                if (b == string.Empty || b == "/naryad")
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id,
                        "Щось ти тупенький, ти не ввiв прiзвище.\nТи хочеш ЗНАТИ чи СДАТИ?");
                    return;
                }
                var needed = Users.FirstOrDefault(n => n.Fio.ToLower().Contains(b.ToLower()));
                if (needed == null)
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Я не знайшов цього курсанта");
                    return;
                }
                if (needed.Naryady.Count == 0)
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "У цього курсанта немае нарядiв.");
                    return;
                }
                var text = $"Наряди курсанта {needed.Fio}\n";
                text = needed.Naryady.Aggregate(text,
                    (current, shit) =>
                        current + $"\n{NaryadResolver.GetNaryadName(shit.ENaryad)} - {shit.Day}-го числа");
                await Bot.SendTextMessageAsync(message.Chat.Id, text);
            }
        }
    }
}