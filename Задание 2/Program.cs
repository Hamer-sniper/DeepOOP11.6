using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Авто создание xml
            //Consultant.AutoCreation();

            bool ex = true;
            while (ex)
            {
                Console.WriteLine("1 - Работать как \"Консультант\"");
                Console.WriteLine("2 - Работать как \"Менеджер\"");
                Console.WriteLine("3 - Выйти");
                Consultant.Header();

                switch (Console.ReadLine())
                {
                    case "1": Consultant.ShowMenu(); break;
                    case "2": Manager.ShowMenu(); break;
                    case "3": ex = false; break;
                }
            }
        }       
    }
}