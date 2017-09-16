using System;
using System.Collections.Generic;
using System.Linq;

namespace Kazitsky_Bot
{
    public static class Voids
    {
        public static string GetNaryads(byte offset = 0)
        {
            var naryads = new Dictionary<string, string>();
            foreach (var user in Program.Users)
            {
                var naryad = user.Naryady.FirstOrDefault(n => n.Day == DateTime.Now.Day + offset);
                if (naryad != null)
                    naryads.Add(user.Fio, NaryadResolver.GetNaryadName(naryad.ENaryad));
            }

            return naryads.Aggregate(string.Empty, (current, s) => current + "\n" + $"{s.Key} - {s.Value}");
        }


        public static string GetPosition(Day day, int pos)
        {
            if (DateTime.Now.DayOfYear % 2 == 0)
                switch (pos)
                {
                    case 1:
                        return day.Para1Chet;
                    case 2:
                        return day.Para2Chet;
                    case 3:
                        return day.Para3Chet;
                }
            else
                switch (pos)
                {
                    case 1:
                        return day.Para1Nechet;
                    case 2:
                        return day.Para2Nechet;
                    case 3:
                        return day.Para3Nechet;
                }
            return "";
        }
    }
}