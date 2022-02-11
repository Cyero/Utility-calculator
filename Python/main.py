from datetime import datetime
from decimal import Decimal
from modules import sqlite as sql
from modules import user_dialogs as ud


def calculate(flow, past, tariff):
    """ Рассчитываем сумму к оплате """
    while True:
        try:
            consum = int(flow) - int(past)
            summ = consum * Decimal(tariff)
            break
        except ValueError:
            print(" Произошла ошибка \n Перезапустите приложение ")
    return summ


def main():
    user_id = ud.get_user_phone()
    date = datetime.now().replace(microsecond=0)
    print("Прошлые показания за водоснабжение : ")
    water_start = ud.check_format(sql.get_data_from_db("W_end", user_id))
    water_end = ud.add_new_data()
    print("Текущий тариф за водоснабжение : ")
    water_tarif = ud.check_tariff(ud.check_format(
        sql.get_data_from_db("W_tariff", user_id)))
    water_summ = calculate(water_end, water_start, water_tarif)
    print("Прошлые показания за элетроснабжение : ")
    energy_start = ud.check_format(sql.get_data_from_db("E_end", user_id))
    energy_end = ud.add_new_data()
    print("Текущий тариф за элетроснабжение : ")
    energy_tarif = ud.check_tariff(ud.check_format(
        sql.get_data_from_db("E_tariff", user_id)))
    energy_summ = calculate(energy_end, energy_start, energy_tarif)
    sql.save_data_to_db(user_id, date, water_start, water_end, water_tarif,
                        water_summ, energy_start, energy_end, energy_tarif,
                        energy_summ)
    ud.result_msg(water_summ, energy_summ)
    ud.nt_pause()


if __name__ == '__main__':
    main()
