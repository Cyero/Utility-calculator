namespace UtilityCalculator.Modules;
class UserDialog
{
    private double _water;
    private double _electrical;
    private void ResultMsg()
    {
        Console.WriteLine(" На текущую дату необходимо оплатить : ");
        Console.WriteLine($" {_water} - за водоснабжение, {_electrical} - за электроэнергию ");
    }
    private static void Pause()
    {
        Console.WriteLine(" Для завершения нажмите любую клавишу . . . ");
        Console.ReadKey();
    }
    private double FormatChecker(double _data)
    {
        if (_data == 0)
        {
            try
            {
                Console.WriteLine(" Данные отсутствуют в БД \nУкажите значение : ");
                double _userData = Convert.ToDouble(Console.ReadLine());
                return _userData;
            }
            catch (FormatException)
            {
                Console.WriteLine(" Используйте числовой формат! ");
                return AddUserData();
            }
        }
        else
        {
            Console.WriteLine(_data);
            return _data;
        }
    }
    private double TariffChecker(double _data)
    {
        Console.WriteLine(" Тарифы указаны верно? \nВведите 'нет' чтобы изменить : ");
        string? _answer = Console.ReadLine();
        if (_answer?.ToLower() == "нет")
        {
            try
            {
                Console.WriteLine(" Укажите правильный тариф : ");
                double _tarif = Convert.ToDouble(Console.ReadLine());
                return _tarif;
            }
            catch (FormatException)
            {
                Console.WriteLine(" Используйте числовой формат! ");
                return AddUserData();

            }

        }
        else
        {
            return _data;
        }
    }
    public double SetFormat(double _data)
    {
        double _fdata = FormatChecker(_data);
        return _fdata;
    }
    public double AddUserData()
    {
        try
        {
            Console.WriteLine(" Укажите текущее значение : ");
            double _data = Convert.ToDouble(Console.ReadLine());
            return _data;
        }
        catch (FormatException)
        {
            Console.WriteLine(" Используйте числовой формат! ");
            return AddUserData();
        }

    }
    public double ValueChecker(double _dataset)
    {
        double _data = TariffChecker(FormatChecker(_dataset));
        return _data;
    }
    public string? GetPhoneNumber()
    {
        Console.WriteLine(" Введите номер телефона : ");
        string? _phone = Console.ReadLine();
        return _phone;
    }
    public void ProgramFinisher(double _water, double _electrical)
    {
        this._water = _water;
        this._electrical = _electrical;
        ResultMsg();
        Pause();
    }
}
