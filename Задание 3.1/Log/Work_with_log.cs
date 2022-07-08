using System.Xml;
using System.Xml.Linq;
using Задание_3._1.Models;

namespace Задание_3._1.Log
{
    public class Work_with_log
    {
        private static readonly string _logFilePath = Environment.CurrentDirectory + @"\Changes_log.xml";

        /// <summary>
        /// Запись в xml
        /// </summary>
        /// <param name="logs">Записи в лог</param>
        public static void AddToLogXmlFromList(List<Employee> logs)
        {
            XElement changes = new XElement("Changes");

            foreach (var log in logs)
            {
                XElement change = new XElement("Change");
                XElement dateTimeChange = new XElement("dateTimeChange", log.DateTimeChange);
                XElement dataChanged = new XElement("dataChanged", log.DataChanged);
                XElement typeOfChanges = new XElement("typeOfChanges", log.TypeOfChanges);
                XElement changer = new XElement("changer", log.Changer);
                
                change.Add(dateTimeChange, dataChanged, typeOfChanges, changer);
                changes.Add(change);
            }

            changes.Save(_logFilePath);
        }

        /// <summary>
        /// Чтение из xml
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="consultants">Данные</param>
        /// <returns>consultants</returns>
        public static List<Employee> ReadFromLogXml()
        {
            var log = new List<Employee>();
            string xdateTimeChange = "", xdataChanged = "", xtypeOfChanges = "", xchanger = "";

            if (!File.Exists(_logFilePath)) AutoLogCreation();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_logFilePath);

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
                        if (childnode.Name == "dateTimeChange") xdateTimeChange = childnode.InnerText;
                        if (childnode.Name == "dataChanged") xdataChanged = childnode.InnerText;
                        if (childnode.Name == "typeOfChanges") xtypeOfChanges = childnode.InnerText;
                        if (childnode.Name == "changer") xchanger = childnode.InnerText;                        
                    }

                    log.Add(new Employee
                    {
                        DateTimeChange = xdateTimeChange,
                        DataChanged = xdataChanged,
                        TypeOfChanges = xtypeOfChanges,
                        Changer = xchanger,
                    });
                }
            }

            return log;
        }

        /// <summary>
        /// Автосоздание данных
        /// </summary>        
        /// <returns>consultants</returns>
        private static void AutoLogCreation()
        {
            var employees = new List<Employee>();

            for (int i = 1; i < 10; i++)
            {
                string testDateTimeChange = DateTime.Now.ToString();
                string testDataChanged = "Поле " + i;
                string testTypeOfChanges = "Изменение";
                string testChanger = "Андрей " + i;

                var employee = new Employee
                {
                    DateTimeChange = testDateTimeChange,
                    DataChanged = testDataChanged,
                    TypeOfChanges = testTypeOfChanges,
                    Changer = testChanger,
                };

                employees.Add(employee);
            }

            AddToLogXmlFromList(employees);
        }

        public static void ShowChanges()
        {
            var log = ReadFromLogXml();
            var counter = 0;

            foreach (Employee noteOfLog in log)
            {
                counter++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("_____________________________");
                Console.WriteLine("№" + counter);
                Console.ResetColor();
                noteOfLog.ShowLog();
            }
        }
    }
}

