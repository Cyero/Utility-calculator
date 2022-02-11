using System.Globalization;
using UtilityCalculator.Modules;

namespace UtilityCalculator
{
    static class UtilityCalculator
    {
        static double Calculate(double now, double past, double tariff)
        {
            double result = (now - past) * tariff;
            return result;
        }
        static void Main()
        {
            string? userId = UserDialog.GetPhoneNumber();
            SqlDb.ConnectToDb(userId);
            var date = DateTime.Now;
            string nowDate = Convert.ToString(date, CultureInfo.CurrentCulture);
            Console.WriteLine(" Прошлые показания за водоснабжение : ");
            double waterStart = UserDialog.FormatChecker(SqlDb.LoadFromDb("W_end", userId)); 
            double waterEnd = UserDialog.AddUserData();
            Console.WriteLine(" Текущий тариф за водоснабжение : ");
            double waterTariff = UserDialog.TariffChecker(UserDialog.FormatChecker(SqlDb.LoadFromDb("W_tariff", userId)));
            double waterSum = Calculate(waterEnd, waterStart, waterTariff);
            Console.WriteLine(" Прошлые показания за элетроснабжение : ");
            double energyStart = (int)UserDialog.FormatChecker(SqlDb.LoadFromDb("E_end", userId));
            double energyEnd = UserDialog.AddUserData();
            Console.WriteLine(" Текущий тариф за элетроснабжение : ");
            double energyTariff = UserDialog.TariffChecker(UserDialog.FormatChecker(SqlDb.LoadFromDb("E_tariff", userId)));
            double energySum = Calculate(energyEnd, energyStart, energyTariff);
            SqlDb.SaveToDb(userId, nowDate, waterStart, waterEnd, waterTariff, waterSum, energyStart, energyEnd, energyTariff, energySum);
            UserDialog.ResultMsg(waterSum, energySum);
            UserDialog.Pause();
        }
    }
}