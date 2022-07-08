using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Авто создание xml
            //Consultant.AutoCreation();

            bool exit = true;

            ShowMenu();

            while (exit)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nВведите команду: ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1": Consultant.UserInput(); break;
                    case "2": Consultant.ShowInformationAboutAll(); break;
                    case "3": Consultant.ChangeTelephoneNumber(); break;
                    case "4": exit = false; break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("1 - Добавить данные");
            Console.WriteLine("2 - Вывод информации");
            Console.WriteLine("3 - Изменить номер телефона");
            Console.WriteLine("4 - Выход");
        }
    }
}