namespace UtilityCalculator.Modules;

public static class UserDialog
{
    public static string? GetPhoneNumber()
    {
        Console.WriteLine(" Введите номер телефона ");
        string? phone = Console.ReadLine();
        return phone;
    }

    public static double TariffChecker(double data)
    {
        Console.WriteLine(" Тарифы указаны верно? \nВведите 'нет' чтобы изменить : ");
        string? answer = Console.ReadLine();
        if (answer?.ToLower() == "нет")
        {
            try
            {
                Console.WriteLine(" Укажите правильный тариф ");
                double tarif = Convert.ToDouble(Console.ReadLine());
                return tarif;
            }
            catch (FormatException)
            {
                Console.WriteLine(" Используйте числовой формат! ");
                return AddUserData();

            }

        }
        else
        {
            return data;
        }
    }

    public static void ResultMsg(double water, double electrical)
    {
        Console.WriteLine(" На текущую дату необходимо оплатить : ");
        Console.WriteLine($" {water} - за водоснабжение, {electrical} - за электроэнергию ");
    }
    
    public static double AddUserData()
    {
        try
        {
            Console.WriteLine(" Укажите текущее значение : ");
            double data = Convert.ToDouble(Console.ReadLine());
            return data;
        }
        catch (FormatException)
        {
            Console.WriteLine(" Используйте числовой формат! ");
            return AddUserData();
        }

    }

    public static double FormatChecker(double data)
    {
        if (data == 0)
        {
            try
            {
                Console.WriteLine(" Данные отсутствуют в БД \nУкажите значение : ");
                double userData = Convert.ToDouble(Console.ReadLine());
                return userData;
            }
            catch (FormatException)
            {
                Console.WriteLine(" Используйте числовой формат! ");
                return AddUserData();
            }
        }
        else
        {
            Console.WriteLine(data);
            return data;
        }
    }

    public static void Pause()
    {
        Console.WriteLine(" Для завершения нажмите любую клавишу . . . ");
        Console.ReadKey();
    }
}
