using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Parts_Store.HR;
using Vehicle_Parts_Store.Items;

namespace Vehicle_Parts_Store
{
    public class Menu
    {
        bool exit = false;
        int specifiedId, secondsCounter;
        string currentTime;

        Repository<Employee> employeeRepository;
        Repository<Customer> customerRepository;
        Repository<Order> orderRepository;
        Category<EnginePart> enginePartCategory;
        Category<BodyPart> bodyPartCategory;
        Category<ElectricalPart> electricalPartCategory;

        public Menu()
        {
            employeeRepository = new Repository<Employee>();
            customerRepository = new Repository<Customer>();
            orderRepository = new Repository<Order>();
            employeeRepository.Add(new Employee("Bartosz", "Rosiek", "123456789", "Boss" ,22, 3200));
            employeeRepository.Add(new Employee("Kamil", "Amrah", "987654321", "Worker" ,48, 1700));
            customerRepository.Add(new Customer("Simon", "Barakuda", "982134321", true));
            customerRepository.Add(new Customer("Kristof", "Ronaldinio", "123134321", false));

            enginePartCategory = new Category<EnginePart>();
            bodyPartCategory = new Category<BodyPart>();
            electricalPartCategory = new Category<ElectricalPart>();
            enginePartCategory.Add(new EnginePart("Piston", "PistonMaster", "Car", 4, 320, "1.2", "Piston for small engine cars"));
            enginePartCategory.Add(new EnginePart("Piston", "PistonOmega", "Car", 8, 1452, "4.2", "Piston for big engine cars"));
            bodyPartCategory.Add(new BodyPart("Door", "BMW", "Car", 2, 320, "Black", true));
            bodyPartCategory.Add(new BodyPart("Hood", "Mazda", "Car", 1, 180, "Red", false));
            electricalPartCategory.Add(new ElectricalPart("Computer", "MAN", "Truck", 1, 4200, false, "Medium"));
            electricalPartCategory.Add(new ElectricalPart("Wires", "Alfa Romeo", "Car", 3, 160, true, "Small"));

            StartClock();
            ShowMenu();
        }

        public async void StartClock()
        {
            while (true)
            {
                currentTime = DateTime.Now.ToString("HH:mm:ss");
                await Task.Delay(1000);
                secondsCounter++;
            }
        }

        public void ShowSpecifiedID<T>()
        {
            Console.Write($"{typeof(T).Name} ID > ");
            
            try
            {
                specifiedId = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid ID.\n");
                ShowMenu();
            }

            Console.Clear();

            if(typeof(T) == typeof(Employee))
                if (specifiedId <= employeeRepository.repository.Last().ID && specifiedId >= employeeRepository.repository.First().ID)
                    ShowSpecifiedEmployeeMenu(specifiedId);
                else
                {
                    Console.WriteLine("ID hasn't been found.\n");
                    ShowEmployeesMenu();
                }

            if(typeof(T) == typeof(Customer))
            {
                if (specifiedId <= customerRepository.repository.Last().ID && specifiedId >= customerRepository.repository.First().ID)
                    ShowSpecifiedCustomerMenu(specifiedId);
                else
                {
                    Console.WriteLine("ID hasn't been found.\n");
                    ShowCustomersMenu();
                }
            }

            if (typeof(T) == typeof(EnginePart))
            {
                if (specifiedId <= enginePartCategory.repository.Last().ID && specifiedId >= enginePartCategory.repository.First().ID)
                    ShowSpecifiedWarehouseMenu<EnginePart>(specifiedId);
                else
                {
                    Console.WriteLine("ID hasn't been found.\n");
                    ShowWarehouseMenu();
                }
            }

            if (typeof(T) == typeof(BodyPart))
            {
                if (specifiedId <= bodyPartCategory.repository.Last().ID && specifiedId >= bodyPartCategory.repository.First().ID)
                    ShowSpecifiedWarehouseMenu<BodyPart>(specifiedId);
                else
                {
                    Console.WriteLine("ID hasn't been found.\n");
                    ShowWarehouseMenu();
                }
            }

            if (typeof(T) == typeof(ElectricalPart))
            {
                if (specifiedId <= electricalPartCategory.repository.Last().ID && specifiedId >= electricalPartCategory.repository.First().ID)
                    ShowSpecifiedWarehouseMenu<ElectricalPart>(specifiedId);
                else
                {
                    Console.WriteLine("ID hasn't been found.\n");
                    ShowWarehouseMenu();
                }
            }

            if (typeof(T) == typeof(Order))
            {
                if (specifiedId <= orderRepository.repository.Last().ID && specifiedId >= orderRepository.repository.First().ID)
                    ShowSpecifiedOrderMenu(specifiedId);
                else
                {
                    Console.WriteLine("ID hasn't been found.\n");
                    ShowOrderMenu();
                }
            }
        }

        public void ShowMenu()
        {
            while (!exit)
            {

                Console.Write($"""
                              Current time: {currentTime}
                              0. Exit
                              1. Employees manager
                              2. Customers manager
                              3. Orders manager
                              4. Warehouse manager
                              > 
                              """);
                string input = Console.ReadLine();
                Console.WriteLine();

                switch(input)
                {
                    case "0":
                        exit = true;
                        Console.WriteLine($"You have used this program for {secondsCounter} seconds.");
                        break;
                    case "1":
                        Console.Clear();
                        ShowEmployeesMenu();
                        break;
                    case "2":
                        Console.Clear();
                        ShowCustomersMenu();
                        break;
                    case "3":
                        Console.Clear();
                        ShowOrderMenu();
                        break;
                    case "4":
                        Console.Clear();
                        ShowWarehouseMenu();
                        break;
                    default: 
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }
        #region EmployeesManager
         public void ShowEmployeesMenu()
        {
            Console.Write("""
                          0. Go back to main menu.
                          1. Create new employee.
                          2. Show all employees.
                          3. Show employee (using employee ID).
                          4. Employees statistics.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "0":
                    Console.Clear();
                    ShowMenu();
                    break;
                case "1":
                    Console.Clear();
                    CreateEmployee();
                    ShowEmployeesMenu();
                    break;
                case "2":
                    Console.Clear();
                    employeeRepository.ShowAll();
                    ShowEmployeesMenu();
                    break;
                case "3":
                    Console.Clear();
                    ShowSpecifiedID<Employee>();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Employees hired: " + employeeRepository.repository.Count + "\n");
                    Console.WriteLine("Boss: ");
                    //look for position boss
                    int bossIndex = employeeRepository.repository.FindIndex(emp => emp.Position == "Boss");
                    if(bossIndex >= 0)
                        employeeRepository.Show(employeeRepository.repository[bossIndex]);
                    else
                        Console.WriteLine("There is not a boss in the company!");
                    Console.WriteLine();
                    ShowEmployeesMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }

        public void ShowSpecifiedEmployeeMenu(int id)
        {
            int index = employeeRepository.repository.FindIndex(emp => emp.ID == id);
            employeeRepository.Show(employeeRepository.repository[index]);
            Console.WriteLine();

            Console.Write("""
                          0. Go back to employees menu.
                          1. Edit employee.
                          2. Remove employee.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "0":
                    Console.Clear();
                    ShowEmployeesMenu();
                    break;
                case "1":
                    employeeRepository.Edit(employeeRepository.repository[index]);
                    ShowEmployeesMenu();
                    break;
                case "2":
                    employeeRepository.Remove(employeeRepository.repository[index]);
                    Console.Clear();
                    Console.WriteLine("Succesfully removed an employee with ID: " + employeeRepository.repository[index].ID + "\n");
                    ShowEmployeesMenu();
                    break;
                default:
                    Console.WriteLine("Invaild input.");
                    break;
            }
        }

        public void CreateEmployee()
        {
            string firstName, lastName, phone, position, input;
            int age, salary;

            Console.Write("First name > ");
            firstName= Console.ReadLine();
            Console.Write("Last name > ");
            lastName= Console.ReadLine();
            Console.Write("Phone number > ");
            phone= Console.ReadLine();
            Console.Write("Position > ");
            position = Console.ReadLine();

            try
            {
                Console.Write("Age > ");
                input = Console.ReadLine();
                age = int.Parse(input);
                Console.Write("Salary > ");
                input = Console.ReadLine();
                salary = int.Parse(input);
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error. Age/Salary needs to be a natural number." + "\n");
                return;
            }

            // check if employee has been created correctly
            try
            {
                employeeRepository.Add(new Employee(firstName, lastName, phone, position, age, salary));
            }catch (Exception ex)
            {
                Console.WriteLine("Error while adding a new employee." + "\n");
                return;
            }
            Console.WriteLine("\n" + "Succesfully created a new employee." + "\n");
        }
        #endregion
        #region CustomersManager
        public void ShowCustomersMenu()
        {
            Console.Write("""
                          0. Go back to main menu.
                          1. Create new customer.
                          2. Show all customers.
                          3. Show customer (using customer ID).
                          4. Customers statistics.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch(input)
            {
                case "0":
                    Console.Clear();
                    ShowMenu();
                    break;
                case "1":
                    Console.Clear();
                    CreateCustomer();
                    ShowCustomersMenu();
                    break;
                case "2":
                    Console.Clear();
                    customerRepository.ShowAll();
                    ShowCustomersMenu();
                    break;
                case "3":
                    Console.Clear();
                    ShowSpecifiedID<Customer>();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Customers: " + customerRepository.repository.Count + "\n");
                    Console.WriteLine("Most money spent: ");
                    Customer objectWithHighestValue = customerRepository.repository.MaxBy(customer => customer.MoneySpent);
                    customerRepository.Show(objectWithHighestValue);
                    Console.WriteLine();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }

        public void ShowSpecifiedCustomerMenu(int id)
        {
            int index = customerRepository.repository.FindIndex(customer => customer.ID == id);
            customerRepository.Show(customerRepository.repository[index]);
            Console.WriteLine();

            Console.Write("""
                          0. Go back to customers menu.
                          1. Edit customer.
                          2. Remove customer.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "0":
                    Console.Clear();
                    ShowCustomersMenu();
                    break;
                case "1":
                    customerRepository.Edit(customerRepository.repository[index]);
                    ShowCustomersMenu();
                    break;
                case "2":
                    customerRepository.Remove(customerRepository.repository[index]);
                    Console.Clear();
                    Console.WriteLine("Succesfully removed a customer with ID: " + customerRepository.repository[index].ID + "\n");
                    ShowCustomersMenu();
                    break;
                default:
                    Console.WriteLine("Invaild input.");
                    break;
            }
        }
        public void CreateCustomer()
        {
            string firstName, lastName, phone, isCompany;

            Console.Write("First name > ");
            firstName = Console.ReadLine();
            Console.Write("Last name > ");
            lastName = Console.ReadLine();
            Console.Write("Phone number > ");
            phone = Console.ReadLine();
            Console.Write("Is company (yes/no) > ");
            isCompany = Console.ReadLine();

            // check if customer has been created correctly
            try
            {
                customerRepository.Add(new Customer(firstName, lastName, phone, isCompany.Equals("yes") ? true : false));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding a new customer." + "\n");
                return;
            }
            Console.WriteLine("\n" + "Succesfully created a new customer." + "\n");
        }
        #endregion
        #region WarehouseManager
        public void ShowWarehouseMenu()
        {
            Console.Write("""
                          0. Go back to main menu.
                          1. Create new item.
                          2. Show all items (ascending by price).
                          3. Show all items (descending by price).
                          4. Show item (using item ID).
                          5. Items statistics.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch(input)
            {
                case "0":
                    Console.Clear();
                    ShowMenu();
                    break;
                case "1":
                    Console.Clear();
                    Console.Write("Select item type (engine, body, electrical) > ");
                    string type = Console.ReadLine();
                    switch (type)
                    {
                        case "engine":
                            CreateItem<EnginePart>();
                            break;
                        case "body":
                            CreateItem<BodyPart>();
                            break;
                        case "electrical":
                            CreateItem<ElectricalPart>();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input.\n");
                            break;
                    }
                    Console.Clear();
                    ShowWarehouseMenu();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("ENGINE PARTS: ");
                    enginePartCategory.ShowAscendingByPrice();
                    Console.WriteLine("BODY PARTS: ");
                    bodyPartCategory.ShowAscendingByPrice();
                    Console.WriteLine("ELECTRICAL PARTS: ");
                    electricalPartCategory.ShowAscendingByPrice();
                    ShowWarehouseMenu();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("ENGINE PARTS: ");
                    enginePartCategory.ShowDescendingByPrice();
                    Console.WriteLine("BODY PARTS: ");
                    bodyPartCategory.ShowDescendingByPrice();
                    Console.WriteLine("ELECTRICAL PARTS: ");
                    electricalPartCategory.ShowDescendingByPrice();
                    ShowWarehouseMenu();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Choose part type (engine, body, electrical) > ");
                    string partType = Console.ReadLine();
                    if (partType.Equals("engine"))
                        ShowSpecifiedID<EnginePart>();
                    else if (partType.Equals("body"))
                        ShowSpecifiedID<BodyPart>();
                    else if (partType.Equals("electrical"))
                        ShowSpecifiedID<ElectricalPart>();
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input\n");
                        ShowWarehouseMenu();
                    }
                        break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Most expensive engine part: ");
                    EnginePart objectWithHighestValue = enginePartCategory.repository.MaxBy(x => x.Price);
                    enginePartCategory.Show(objectWithHighestValue);
                    Console.WriteLine("\nMost expensive body part: ");
                    BodyPart bodyPartWithHighestValue = bodyPartCategory.repository.MaxBy(x => x.Price);
                    bodyPartCategory.Show(bodyPartWithHighestValue);
                    Console.WriteLine("\nMost expensive electrical part: ");
                    ElectricalPart electricalPartWithHighestValue = electricalPartCategory.repository.MaxBy(x => x.Price);
                    electricalPartCategory.Show(electricalPartWithHighestValue);
                    ShowWarehouseMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }

        public void CreateItem<T>()
        {
            string name, brand, vehicleType, input;
            int quantity, price;

            Console.Write("Name > ");
            name = Console.ReadLine();
            Console.Write("Brand > ");
            brand = Console.ReadLine();
            Console.Write("Vehicle type (None, Car, Truck, Motorcycle) > ");
            vehicleType = Console.ReadLine();

            try
            {
                Console.Write("Quantity > ");
                input = Console.ReadLine();
                quantity = int.Parse(input);
                Console.Write("Price > ");
                input = Console.ReadLine();
                price = int.Parse(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error. Quantity/Price needs to be a natural number." + "\n");
                return;
            }

            if (typeof(T) == typeof(EnginePart))
            {
                string size, desc;
                Console.Write("Engine size (in liters) > ");
                size = Console.ReadLine();
                Console.Write("Description > ");
                desc = Console.ReadLine();
                enginePartCategory.Add(new EnginePart(name, brand, vehicleType, quantity, price, size, desc));
            }

            if (typeof(T) == typeof(BodyPart))
            {
                string color, wbolts;
                Console.Write("Color > ");
                color = Console.ReadLine();
                Console.Write("With bolts (yes/no) > ");
                wbolts = Console.ReadLine();
                bodyPartCategory.Add(new BodyPart(name, brand, vehicleType, quantity, price, color, wbolts.Equals("yes") ? true : false));
            }

            if (typeof(T) == typeof(ElectricalPart))
            {
                string ishighvoltage, size;
                Console.Write("Is high voltage (yes/no) > ");
                ishighvoltage = Console.ReadLine();
                Console.Write("Size (small, medium, large) > ");
                size = Console.ReadLine();
                electricalPartCategory.Add(new ElectricalPart(name, brand, vehicleType, quantity, price, ishighvoltage.Equals("yes") ? true : false, size));
            }

        }
        public void ShowSpecifiedWarehouseMenu<T>(int id)
        {
            int index = 0;
            string type;
            if(typeof(T) == typeof(EnginePart))
            {
                index = enginePartCategory.repository.FindIndex(emp => emp.ID == id);
                enginePartCategory.Show(enginePartCategory.repository[index]);
                type = "engine";
            }
            else if (typeof(T) == typeof(BodyPart))
            {
                index = bodyPartCategory.repository.FindIndex(emp => emp.ID == id);
                bodyPartCategory.Show(bodyPartCategory.repository[index]);
                type = "body";
            }
            else if (typeof(T) == typeof(ElectricalPart))
            {
                index = electricalPartCategory.repository.FindIndex(emp => emp.ID == id);
                electricalPartCategory.Show(electricalPartCategory.repository[index]);
                type = "electrical";
            }
            else
                type= "unknown";

            Console.WriteLine();

            Console.Write("""
                          0. Go back to warehouse menu.
                          1. Edit item.
                          2. Remove item.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "0":
                    Console.Clear();
                    ShowWarehouseMenu();
                    break;
                case "1":
                    if (type.Equals("engine"))
                        enginePartCategory.Edit(enginePartCategory.repository[index]);
                    if (type.Equals("body"))
                        bodyPartCategory.Edit(bodyPartCategory.repository[index]);
                    if (type.Equals("electrical"))
                        electricalPartCategory.Edit(electricalPartCategory.repository[index]);
                    ShowWarehouseMenu();
                    break;
                case "2":
                    if (type.Equals("engine"))
                        enginePartCategory.Remove(enginePartCategory.repository[index]);
                    if (type.Equals("body"))
                        bodyPartCategory.Remove(bodyPartCategory.repository[index]);
                    if (type.Equals("electrical"))
                        electricalPartCategory.Remove(electricalPartCategory.repository[index]);
                    Console.Clear();
                    Console.WriteLine("Succesfully removed a part\n");
                    ShowWarehouseMenu();
                    break;
                default:
                    Console.WriteLine("Invaild input.");
                    break;
            }
        }
        #endregion
        #region OrdersManager
        public void ShowOrderMenu()
        {
            Console.Write("""
                          0. Go back to main menu.
                          1. Create new order.
                          2. Show all orders.
                          3. Show order (using order ID).
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch(input)
            {
                case "0":
                    Console.Clear();
                    ShowMenu();
                    break;
                case "1":
                    CreateOrder();
                    break;
                case "2":
                    Console.Clear();
                    orderRepository.ShowAll();
                    ShowOrderMenu();
                    break;
                case "3":
                    Console.Clear();
                    ShowSpecifiedID<Order>();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input.");
                    break;

            }
        }

        public void CreateOrder()
        {
            int employeeID, customerID;
            string input;
            bool stop = false;

            Console.Clear();
            try
            {
                Console.Write("Employee ID > ");
                input = Console.ReadLine();
                employeeID = int.Parse(input);
            }
            catch (Exception e) { Console.WriteLine("Invalid input."); return; }

            Console.Write("Do you want to create new customer? (yes/no) > ");
            bool newCustomer = Console.ReadLine().Equals("yes") ? true : false;

            if (newCustomer)
            {
                CreateCustomer();
                customerID = customerRepository.repository.LastOrDefault()?.ID ?? 0;
            }
            else
            {
                try
                {
                    Console.Write("Customer ID > ");
                    input = Console.ReadLine();
                    customerID = int.Parse(input);
                }
                catch (Exception e) { Console.WriteLine("Invalid input."); return; }
            }

            Dictionary<Part, int> order = new Dictionary<Part, int>();

            while (true)
            {
                int partID_int, quantity_int;
                string partID, quantity;
                Part part;

                Console.Write("Part ID (type 'quit' to stop adding items to order) > ");
                partID = Console.ReadLine();
                if (partID.Equals("quit"))
                    break;
                else
                {
                    Console.Write("Quantity > ");
                    quantity = Console.ReadLine();

                    try
                    {
                        partID_int = int.Parse(partID);
                        quantity_int = int.Parse(quantity);
                    }
                    catch (Exception e) { Console.WriteLine("Error"); return; }
                }

                if (enginePartCategory.repository.Any(item => item.ID == partID_int))
                    part = enginePartCategory.repository.FirstOrDefault(item => item.ID == partID_int);
                else if (electricalPartCategory.repository.Any(item => item.ID == partID_int))
                    part = electricalPartCategory.repository.FirstOrDefault(item => item.ID == partID_int);
                else if (bodyPartCategory.repository.Any(item => item.ID == partID_int))
                    part = bodyPartCategory.repository.FirstOrDefault(item => item.ID == partID_int);
                else
                {
                    Console.WriteLine("No item with that ID");
                    return;
                }

                order.Add(part, quantity_int);
            }

            Order orderFinal = new(employeeID, customerID, order);

            //invoke events
            orderFinal.OnOrder(order);

            orderRepository.Add(orderFinal);

            //fill other fields or properties
            Employee employee = employeeRepository.repository.FirstOrDefault(x => x.ID == employeeID);
            Customer customer = customerRepository.repository.FirstOrDefault(x => x.ID == customerID);

            if (employee != null)
            {
                employee.CompletedOrdersIDs.Add(orderRepository.repository.LastOrDefault().ID);
            }

            if (customer != null)
            {
                customer.OrdersMadeIDs.Add(orderRepository.repository.LastOrDefault().ID);
                customer.MoneySpent += orderRepository.repository.LastOrDefault().OrderValue;

                Console.Clear();
                Console.WriteLine("Succesfully created new order.\n");
                orderRepository.Show(orderRepository.repository[orderRepository.repository.Count - 1]);
            }
        }

        public void ShowSpecifiedOrderMenu(int id)
        {
            int index = orderRepository.repository.FindIndex(order => order.ID == id);
            orderRepository.Show(orderRepository.repository[index]);
            Console.WriteLine();

            Console.Write("""
                          0. Go back to orders menu.
                          1. Remove order.
                          > 
                          """);

            string input = Console.ReadLine();
            Console.WriteLine();

            switch(input)
            {
                case "0":
                    Console.Clear();
                    ShowOrderMenu();
                    break;
                case "1":
                    Console.Clear();
                    Console.WriteLine("Succesfully removed an order.\n");
                    orderRepository.Remove(orderRepository.repository[index]);
                    ShowOrderMenu();
                    break;
            }
        }
        #endregion
    }
}
