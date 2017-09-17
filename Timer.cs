using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot.Types.Enums;
using Timer = System.Timers.Timer;

namespace Kazitsky_Bot
{
    public static class WakeUp
    {


        public static void SetTimer()
        {
            AlarmClock clock = new AlarmClock(DateTime.Today.AddDays(1).AddHours(7).AddMinutes(30));
            clock.Alarm += (sender, e) =>
            {
                Task.Run(WakeUppy);
                Console.WriteLine("invoked!");
            };
           
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
            SetTimer();
        }
        public class AlarmClock
        {
            public AlarmClock(DateTime alarmTime)
            {
                this.alarmTime = alarmTime;

                timer = new System.Timers.Timer();
                timer.Elapsed += timer_Elapsed;
                timer.Interval = 1000;
                timer.Start();

                enabled = true;
            }

            void  timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                if(enabled && DateTime.Now > alarmTime)
                {
   
                    enabled = false;
                    OnAlarm();
                    timer.Stop();
                }
            }

            protected virtual void OnAlarm()
            {
                if(alarmEvent != null)
                    alarmEvent(this, EventArgs.Empty);
            }


            public event EventHandler Alarm
            {
                add { alarmEvent += value; }
                remove { alarmEvent -= value; }
            }

            private EventHandler alarmEvent;
            private Timer timer;
            private DateTime alarmTime;
            private bool enabled;
        }
    }
}