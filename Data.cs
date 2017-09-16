using System.Collections.Generic;

namespace Kazitsky_Bot
{
    public class Naryad
    {
        public int Day;
        public ENaryad ENaryad;
    }

    public enum ENaryad
    {
        Dnevalny = 1,
        Rota = 0,
        Kambuz = 2,
        Ekipash = 3,
        Gospodar = 4,
        Akademiya = 5,
        Deshurniy = 6
    }

    public class Day
    {
        public int DayOfTheWeek;
        public string Para1Chet;
        public string Para1Nechet;
        public string Para2Chet;
        public string Para2Nechet;
        public string Para3Chet;
        public string Para3Nechet;
    }


    public class Uchenik
    {
        public string Fio;
        public List<Naryad> Naryady;
    }
}