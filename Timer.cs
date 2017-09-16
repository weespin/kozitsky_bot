using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace Kazitsky_Bot
{
    public static class WakeUp
    {
        private static Timer _timer;

        public static void SetUpTimer(TimeSpan alertTime)
        {
            var current = DateTime.Now;
            var timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
                return; //time already passed
            _timer = new Timer(async x => { await WakeUppy(); }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private static async Task WakeUppy()
        {
            var s = string.Empty;

            var day = Program.Days.FirstOrDefault(n => n.DayOfTheWeek == (int) DateTime.Now.DayOfWeek);
            if (day != null)
            {
                s = "Добрий ранок, Панове!\nЯкi сьогоднi пари? 📚:";
                for (var i = 1; i < 4; i++)
                    s = s + $"\n{i}:" + Voids.GetPosition(day, i);
            }
            if(DateTime.Now.DayOfWeek==DayOfWeek.Saturday)
            {
                s = "Сьогоднi вихiдний!\nСьогоднi у тебе гулянка, та не забувай про наступний понедiлок.\n";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                s = "А твій понеділок починається завтра? Чи у листопаді?\n";
            }
            var temp = Voids.GetNaryads(0);
            if (temp != string.Empty)
            {
                s = s + "\nСьогоднi в нарядi:";
                s = s + temp;
            }

            temp = Voids.GetNaryads(1);
            if (temp != string.Empty)
            {
                s = s + "\nЗавтра будуть у нарядi:";
                s = s + temp;
            }
            await Program.Bot.SendTextMessageAsync(Program.GruppaId, s, ParseMode.Markdown);
Thread.Sleep(1000);
            SetUpTimer(new TimeSpan(07, 20, 00));
        }
    }
}