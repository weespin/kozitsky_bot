namespace Kazitsky_Bot
{
    public static class NaryadResolver
    {
        public static string GetNaryadName(ENaryad naryad)
        {
            var temp = "";
            switch (naryad)
            {
                case ENaryad.Akademiya:
                    temp = "По академii";
                    break;
                case ENaryad.Deshurniy:
                    temp = "Патруль";
                    break;
                case ENaryad.Dnevalny:
                    temp = "Днювальний";
                    break;
                case ENaryad.Ekipash:
                    temp = "Вестибюль экiпажа";
                    break;
                case ENaryad.Gospodar:
                    temp = "Господарськi работи";
                    break;
                case ENaryad.Kambuz:
                    temp = "Камбуз";
                    break;
                case ENaryad.Rota:
                    temp = "Черговий по ротi";
                    break;
                default:
                    temp = "я хз ";
                    break;
            }
            return temp;
        }
    }
}