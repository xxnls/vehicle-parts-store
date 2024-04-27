using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parts_Store.HR
{
    /// <summary>
    /// This class works like container and CRUD operations provider for HR classes.
    /// </summary>
    /// <typeparam name="T">Repository type</typeparam>
    public class Repository<T>
    {
        public List<T> repository { get; set; }

        #region Methods
        public Repository()
        {
            repository = new List<T>();
        }

        public void Add(T item)
        {
            repository.Add(item);
        }

        public void Remove(T item)
        {
            repository.Remove(item);
        }

        public void Edit(T item)
        {
            while (true)
            {
                Console.Clear();
                Show(item);
                Console.WriteLine();
                ShowPropertyNames(item);
                Console.WriteLine();
                Console.Write("Enter field to edit (type 'quit' to leave) > ");
                string input = Console.ReadLine() ?? "quit";

                if (input == "quit")
                {
                    break;
                }

                Console.Write($"Enter value for field {input} > ");

                PropertyInfo property = item.GetType().GetProperty(input);
                Type propertyType = property?.PropertyType;

                //check if property is int
                if (propertyType == typeof(int))
                    property?.SetValue(item, int.Parse(Console.ReadLine()));
                //check if property is DateTime
                if (propertyType == typeof(DateTime))
                    property?.SetValue(item, DateTime.Parse(Console.ReadLine()));
                //check if property is string
                if (propertyType == typeof(string))
                    property?.SetValue(item, Console.ReadLine());
            }
            Console.Clear();
        }

        public void ShowPropertyNames(T item)
        {
            Type type = typeof(T);
            PropertyInfo[] prop = type.GetProperties();

            prop.ToList().ForEach(item => { Console.WriteLine(item.Name); });
        }
        public void ShowAll()
        {
            if (repository.Count != 0)
                foreach (T item in repository)
                {
                    Console.WriteLine(item + "\n");
                }
            else
                Console.WriteLine("Nothing to show.\n");
        }

        public void Show(T item)
        {
            Console.WriteLine(item);
        }
        #endregion

    }
}
