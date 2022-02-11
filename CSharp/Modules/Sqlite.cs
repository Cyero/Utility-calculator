using Microsoft.Data.Sqlite;

namespace UtilityCalculator.Modules;

public static class SqlDb
{

    public static void SaveToDb(string? table, string date, double water_start, double water_end, double water_tarif, double water_summ,
                    double energy_start, double energy_end, double energy_tarif, double energy_summ)
    {
        using var connection = new SqliteConnection("Data Source=Clients.db");
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText =
        $@" 
            INSERT INTO [{table}] VALUES(
            '{date}', 
            '{water_start}', 
            '{water_end}', 
            '{water_tarif}', 
            '{water_summ}', 
            '{energy_start}', 
            '{energy_end}', 
            '{energy_tarif}', 
            '{energy_summ}');
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
    public static double LoadFromDb(string value, string? table)
    {
        using var connection = new SqliteConnection("Data Source=Clients.db");
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = $"SELECT {value} FROM [{table}] ORDER BY Date DESC";
        using var reader = command.ExecuteReader();
        reader.Read();
        try
        {
            var data = reader.GetString(0);
            double val = Convert.ToDouble(data);
            return val;
        }
        catch (Exception)
        {
            double user_data = 0;
            return user_data;
        }
    }

    public static void ConnectToDb(string? table)
    {
        using var connection = new SqliteConnection("Data Source=Clients.db");
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText =
        $@"  
            CREATE TABLE IF NOT EXISTS [{table}] (
                Date Дата проведения расчетов,
                W_start Начальные показания за водоснабжение,
                W_end Конечные показания за водоснабжение,
                W_tariff Тариф за водоснабжение на дату расчета,
                W_summ Итоговая сумма оплаты за водоснабжение,
                E_start Начальные показания за электроснабжение,
                E_end Конечные показания за электроснабжение,
                E_tariff Тариф за электроснабжение на дату расчета,
                E_summ Итоговая сумма оплаты за электроснабжение 
        );";
        command.ExecuteNonQuery();
    }
}