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
    public class Consultant
    {
        #region Поля
        private string surname;

        private string name;

        private string middleName;

        private string telephoneNumber;

        private string pasport;

        private const string path = "Databook.xml";
        #endregion

        #region Свойства
        public string Surname { get { return surname; } }
        public string Name { get { return name; } }
        public string MiddleName { get { return middleName; } }
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { telephoneNumber = string.IsNullOrEmpty(value) ? "111111" : value; }
        }
        public string Pasport
        {
            get
            {
                if (!string.IsNullOrEmpty(pasport))
                    return "********";
                else
                    return "Паспорт не указан";
            }
        }
        #endregion

        #region Конструкторы 
        public Consultant(string surname, string name, string middleName, string telephoneNumber, string pasport)
        {
            this.surname = surname;
            this.name = name;
            this.middleName = middleName;
            this.TelephoneNumber = telephoneNumber;
            this.pasport = pasport;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Запись в xml
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="consultants">Данные</param>
        protected static void AddToXmlFromList(List<Consultant> consultants)
        {
            XElement persons = new XElement("Persons");

            foreach (Consultant consultant in consultants)
            {
                XElement person = new XElement("Person");
                XElement surname = new XElement("surname", consultant.Surname);
                XElement name = new XElement("name", consultant.Name);
                XElement middleName = new XElement("middleName", consultant.MiddleName);
                XElement telephoneNumber = new XElement("telephoneNumber", consultant.TelephoneNumber);
                XElement pasport = new XElement("pasport", consultant.pasport);
                person.Add(surname, name, middleName, telephoneNumber, pasport);
                persons.Add(person);
            }
            persons.Save(path);
        }

        /// <summary>
        /// Чтение из xml
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="consultants">Данные</param>
        /// <returns>consultants</returns>
        public static List<Consultant> ReadFromXml()
        {
            List<Consultant> consultants = new List<Consultant>();
            string xsurname = "", xname = "", xmiddleName = "", xtelephoneNumber = "", xpasport = "";
            if(!File.Exists(path)) AutoCreation();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            // получим корневой элемент
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    // обходим все дочерние узлы элемента
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "surname") xsurname = childnode.InnerText;
                        if (childnode.Name == "name") xname = childnode.InnerText;
                        if (childnode.Name == "surname") xmiddleName = childnode.InnerText;
                        if (childnode.Name == "telephoneNumber") xtelephoneNumber = childnode.InnerText;
                        if (childnode.Name == "pasport") xpasport = childnode.InnerText;

                    }
                    Consultant client = new Consultant(xsurname, xname, xmiddleName, xtelephoneNumber, xpasport);
                    consultants.Add(client);
                }
            }
            return consultants;
        }

        /// <summary>
        /// Меню Консультанта
        /// </summary>
        public static void ShowMenu()
        {
            Console.WriteLine("1 - Вывод информации");
            Console.WriteLine("2 - Изменить номер телефона");
            Console.WriteLine("3 - Выход");

            bool exit = true;
            while (exit)
            {
                Header();
                switch (Console.ReadLine())
                {
                    case "1": Consultant.ShowInformationAboutAll(); break;
                    case "2": Consultant.ChangeTelephoneNumber(); break;
                    case "3": exit = false; break;
                }
            }
        }

        /// <summary>
        /// Вывести данные о конкретном человеке
        /// </summary>
        private void ShowInformation()
        {
            Console.WriteLine(Surname);
            Console.WriteLine(Name);
            Console.WriteLine(MiddleName);
            Console.WriteLine(TelephoneNumber);
            Console.WriteLine(Pasport);
        }

        /// <summary>
        /// Запрос ввода команды
        /// </summary>
        public static void Header()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nВведите команду: ");
            Console.ResetColor();
        }

        /// <summary>
        /// Вывести всю информацию на экран
        /// </summary>
        public static void ShowInformationAboutAll()
        {
            var consultants = ReadFromXml();
            int i = 0;

            foreach (Consultant consultant in consultants)
            {
                i++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("_____________________________");
                Console.WriteLine("№" + i);
                Console.ResetColor();
                consultant.ShowInformation(); 
            }
        }

        /// <summary>
        /// Автосоздание данных
        /// </summary>        
        /// <returns>consultants</returns>
        public static void AutoCreation()
        {
            List<Consultant> consultants = new List<Consultant>();

            for (int i = 1; i < 10; i++)
            {
                string asurname = "Ахвердов " + i;
                string aname = "Андрей " + i;
                string amiddleName = "Александрович " + i;
                string atelephoneNumber = "8918766937" + i;
                string apasport = "0708 10050" + i;

                Consultant client = new Consultant(asurname, aname, amiddleName, atelephoneNumber, apasport);

                consultants.Add(client);
            }
            AddToXmlFromList(consultants);
        }

        /// <summary>
        /// Изменить номер телефона
        /// </summary>
        /// <param name="consultants">Список клиентов</param>
        public static void ChangeTelephoneNumber()
        {
            var consultants = ReadFromXml();

            Console.Write("У какого клиента следует изменить номер телефона?: ");
            int clientNumber = int.Parse(Console.ReadLine());

            int i = 0;            
            foreach (Consultant consultant in consultants)
            {
                i++;
                if (i == clientNumber)
                {
                    consultant.ShowInformation();
                    
                    Console.Write("Введите новый номер телефона: ");
                   
                    consultant.telephoneNumber = Console.ReadLine();                    

                    break;
                }                
            }
            AddToXmlFromList(consultants);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Номер телефона изменен!");
            Console.ResetColor();
        }
        #endregion
    }
}