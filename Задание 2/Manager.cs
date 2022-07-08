using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Task2
{
    public class Manager : Consultant
    {
   
        #region Конструкторы 
        public Manager(string surname, string name, string middleName, string telephoneNumber, string pasport)
            :base(surname, name, middleName, telephoneNumber, pasport)
        {
            
        }
        #endregion

        #region Методы
        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        public static void UserInput()
        {
            Console.Write("Ведите Фамилию: ");
            string usurname = Console.ReadLine();
            Console.Write("Ведите Имя: ");
            string uname = Console.ReadLine();
            Console.Write("Ведите Отчество: ");
            string umiddleName = Console.ReadLine();
            Console.Write("Ведите Номер телефона: ");
            string utelephoneNumber = Console.ReadLine();
            Console.Write("Ведите паспорт: ");
            string upasport = Console.ReadLine();
            Consultant client = new Consultant(usurname, uname, umiddleName, utelephoneNumber, upasport);

            var consultants = ReadFromXml();
            consultants.Add(client);
            AddToXmlFromList(consultants);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Клиент добавлен!");
            Console.ResetColor();
        }

        /// <summary>
        /// Меню Менеджера
        /// </summary>
        public static new void ShowMenu()
        {
            Console.WriteLine("1 - Добавить данные");
            Console.WriteLine("2 - Вывод информации");
            Console.WriteLine("3 - Изменить номер телефона");
            Console.WriteLine("4 - Выход");

            bool exit = true;
            while (exit)
            {
                Header();
                switch (Console.ReadLine())
                {
                    case "1": Manager.UserInput(); break;
                    case "2": Manager.ShowInformationAboutAll(); break;
                    case "3": Manager.ChangeTelephoneNumber(); break;
                    case "4": exit = false; break;
                }
            }
        }
        #endregion
    }
}

