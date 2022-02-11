import os


def check_format(data):
    """ Проверяем наличие значений в БД """
    if data == 0:
        print("Данные отсутствуют в базе : ")
        usr_data = input("Укажите значение : \n -> ")
        return usr_data
    else:
        print(data)
        return data


def add_new_data():
    """ Получаем новые значения от пользователя """
    while True:
        try:
            usr_data = int(input("Укажите текущее значение : \n -> "))
            break
        except ValueError:
            print("Используйте числовой формат!")
    return usr_data


def get_user_phone():
    """ Получаем номер телефона пользователя """
    phone = input("Введите ваш номер телефона: \n -> ")
    return phone
            
            
def check_tariff(data):
    """ Проверяем актуальность тарифов """
    answer = input("Тарифы указаны верно? \n Введите 'нет' чтобы изменить : \n -> ")
    if answer.lower() == "нет":
        usr_data = input("Укажите верное значение: \n -> ")
        return usr_data
    else:
        return data
     

def result_msg(water, electrical):
    """ Выводим итоговое сообщение """
    print("На текущую дату необходимо оплатить : ")
    print(" {} - за водоснабжение, {} - за электроэнергию ") .format(water, electrical)
    

def nt_pause():
    """ Ожидаем ввода от пользователя для завершения работы """
    if os.name == "nt":
        print(" Для завершения нажмите любую клавишу . . . ")
        import msvcrt
        msvcrt.getch()
    else:
        pass
