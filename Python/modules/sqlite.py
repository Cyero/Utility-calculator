import sqlite3


def save_data_to_db(table, date, water_start, water_end, water_tarif, water_summ,
                    energy_start, energy_end, energy_tarif, energy_summ):
    """ Определяем структура БД и сохраняем в нее данные """
    cx = sqlite3.connect('clients.db')
    cu = cx.cursor()
    database = (""" 
                CREATE TABLE IF NOT EXISTS '{}' ( 
                "Date" "Дата проведения расчетов", 
                "W_start" "Начальные показания за водоснабжение",
                "W_end" "Конечные показания за водоснабжение",
                "W_tariff" "Тариф за водоснабжение на дату расчета",
                "W_summ" "Итоговая сумма оплаты за водоснабжение", 
                "E_start" "Начальные показания за электроснабжение", 
                "E_end" "Конечные показания за электроснабжение",
                "E_tariff" "Тариф за электроснабжение на дату расчета",
                "E_summ" "Итоговая сумма оплаты за электроснабжение"
                );  
                """) .format(table)
    cu.executescript(database)
    db_insert = ("INSERT INTO '{}' VALUES ('{}', '{}', '{}', '{}', '{}', '{}', '{}', '{}', '{}')") .format(
        table, date, water_start, water_end, water_tarif, water_summ, energy_start, energy_end, energy_tarif,
        energy_summ)
    cu.execute(db_insert)
    cx.commit()
    cx.close()
    
    
def get_data_from_db(param, table):
    """ Получаем показания с БД """
    cx = sqlite3.connect('clients.db')
    cu = cx.cursor()
    req = ("SELECT {} FROM '{}' ORDER BY Date DESC") .format(param, table)
    try:
        cu.execute(req) 
        return cu.fetchone()[0]
    except sqlite3.OperationalError:
        return 0
