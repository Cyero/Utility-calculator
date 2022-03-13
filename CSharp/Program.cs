using System.Globalization;
using UtilityCalculator.Modules;

namespace UtilityCalculator
{
    class UtilityCalculator
    {
        private static double Calculate(double _now, double _past, double _tarif)
        {
            if ((_now - _past) < 0)
            {
                return 0;
            }
            else
            {
                decimal _result = (Convert.ToDecimal(_now) - Convert.ToDecimal(_past)) * Convert.ToDecimal(_tarif);
                return Convert.ToDouble(_result);
            }            
        }

        static void Main()
        {
            UserDialog Dialog = new();
            SqlDb Sql = new();
            Sql._table = Dialog.GetPhoneNumber();
            Sql.CheckDb();
            string _date = Convert.ToString(DateTime.Now, CultureInfo.CurrentCulture);
            Console.WriteLine(" Прошлые показания за водоснабжение : ");
            double _waterStart = Dialog.SetFormat(Sql.GetValue("W_end"));
            double _waterEnd = Dialog.AddUserData();
            Console.WriteLine(" Текущий тариф за водоснабжение : ");
            double _waterTariff = Dialog.ValueChecker(Sql.GetValue("W_tariff"));
            double _waterSum = Calculate(_waterEnd, _waterStart, _waterTariff);
            Console.WriteLine(" Прошлые показания за элетроснабжение : ");
            double _energyStart = Dialog.SetFormat(Sql.GetValue("E_end"));
            double _energyEnd = Dialog.AddUserData();
            Console.WriteLine(" Текущий тариф за элетроснабжение : ");
            double _energyTariff = Dialog.ValueChecker(Sql.GetValue("E_tariff"));
            double _energySum = Calculate(_energyEnd, _energyStart, _energyTariff);
            Sql.SetValues(_date, _waterStart, _waterEnd, _waterTariff, _waterSum, _energyStart, _energyEnd, _energyTariff, _energySum);
            Dialog.ProgramFinisher(_waterSum, _energySum);
        }
    }
}