using Microsoft.Data.Sqlite;

namespace UtilityCalculator.Modules;

class SqlDb
{
    public string? _table;
    public string? _valueFromDb;
    private string? _date;
    private double _waterStart;
    private double _waterEnd;
    private double _waterTarif;
    private double _waterSumm;
    private double _energyStart;
    private double _energyEnd;
    private double _energyTarif;
    private double _energySumm;
    
    public void CheckDb()
    {
        ConnectToDb();
    }
    public double GetValue(string _value)
    {
        _valueFromDb = _value;
        double _dbValue = GetDbValue();
        return _dbValue;
    }
    public void SetValues(string _date, double _waterStart, double _waterEnd, double _waterTarif, double _waterSumm, 
                            double _energyStart, double _energyEnd, double _energyTarif, double _energySumm)
    {
        this._date = _date;
        this._waterStart = _waterStart;
        this._waterEnd = _waterEnd;
        this._waterTarif = _waterTarif;
        this._waterSumm = _waterSumm;
        this._energyStart = _energyStart;
        this._energyEnd = _energyEnd;
        this._energyTarif = _energyTarif;
        this._energySumm = _energySumm;
        SaveToDb();
    }
    private void SaveToDb()
    {
        using var connection = new SqliteConnection("Data Source=Clients.db");
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText =
        $@" 
            INSERT INTO [{_table}] VALUES(
            '{_date}', 
            '{_waterStart}', 
            '{_waterEnd}', 
            '{_waterTarif}', 
            '{_waterSumm}', 
            '{_energyStart}', 
            '{_energyEnd}', 
            '{_energyTarif}', 
            '{_energySumm}');
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
    private double GetDbValue()
    {
        using var connection = new SqliteConnection("Data Source=Clients.db");
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = $"SELECT {_valueFromDb} FROM [{_table}] ORDER BY Date DESC";
        using var reader = command.ExecuteReader();
        reader.Read();
        try
        {
            var _data = reader.GetString(0);
            double _val = Convert.ToDouble(_data);
            return _val;
        }
        catch (Exception)
        {
            double _userData = 0;
            return _userData;
        }
    }

    private void ConnectToDb()
    {
        using var connection = new SqliteConnection("Data Source=Clients.db");
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText =
        $@"  
            CREATE TABLE IF NOT EXISTS [{_table}] (
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