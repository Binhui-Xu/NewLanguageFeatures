using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace NewLanguageFeatures
{
    public delegate bool KeyValueFilter<K, V>(K key, V value);

    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int customerId { get; private set; }
        public Customer(int ID)
        {
            customerId = ID;
        }

        public override string ToString()
        {
            return Name + "\t" + City + "\t" + customerId;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    //Exercise4
    public static class Extensions
    {
        public static bool Compare(this Customer c1, Customer c2)
        {
            if (c1.customerId == c2.customerId && c1.Name == c2.Name && c1.City == c2.City)
            {
                return true;

            }
            return false;
        }
        public static List<T> Append<T>(this List<T> a,List<T> b)
        {
            var newList = new List<T>(a);
            newList.AddRange(b);
            return newList;
        }
        //Exercise5 - optional
        public static List<K> FilterBy<K, V>(this Dictionary<K, V> items,
            KeyValueFilter<K, V> filter)
        {
            var result = new List<K>();
            foreach (KeyValuePair<K, V> element in items)
            {
                if (filter(element.Key, element.Value))
                    result.Add(element.Key);
            }
            return result;
        }
    }
    //Exercise7
    public class Store
    {
        public string Name { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return Name + "\t" + City;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Customer customer = new Customer(1);
            //customer.Name = "Maria Anders";
            //customer.City = "Berlin";
            //Console.WriteLine(customer);
            //Point point = new Point { X = 3, Y = 99 };
            //Customer customer2 = new Customer(2) { Name = "John", City = "London" };
            //Console.WriteLine(customer2);

            //List<Point> Square = new List<Point>
            //{
            //    new Point{ X=0,Y=5},
            //    new Point{ X=5,Y=5},
            //    new Point{ X=5,Y=0},
            //    new Point{ X=0,Y=0 }
            //};
            //List<Customer> customers = CreateCustomer();
            //Console.WriteLine("Customers:\n");
            //foreach (Customer c in customers)
            //    Console.WriteLine(c);

            //var customers = CreateCustomer();
            //var addedCustomer = new List<Customer>
            //{
            //    new Customer(9) { Name = "Diego Roel", City = "Madrid" },
            //    new Customer(10) { Name = "Diego Roel", City = "Madrid" }
            //};
            //var updateedCustomors = customers.Append(addedCustomer);
            //var newCustomor = new Customer(10) { Name = "Diego Roel", City = "Madrid" };
            //foreach (var c in updateedCustomors)
            //{
            //    if (newCustomor.Compare(c))
            //    {
            //        Console.WriteLine("The new customer was already in the list");
            //        return;
            //    }
            //}
            //Console.WriteLine("The new customer was not in the list");

            //var customers = CreateCustomer();
            //foreach (var c in FindCustomorByCity(customers,"London"))
            //{
            //    Console.WriteLine(c);
            //}

            //var customers = CreateCustomer();
            //var customerDictionary=new Dictionary<Customer,string>();
            //foreach (var c in customers)
            //{
            //    customerDictionary.Add(c, c.Name.Split(' ')[1]);
            //}
            //var matches = customerDictionary.FilterBy(
            //    (customer, lastName) => lastName.StartsWith("A"));
            //Console.WriteLine("Number of Matches: {0}",matches.Count);

            //Exercise6
            //Expression<Func<int, bool>> filter = n => (n * 3) < 5;
            //BinaryExpression lt = (BinaryExpression)filter.Body;
            //BinaryExpression mult = (BinaryExpression)lt.Left;
            //ParameterExpression en = (ParameterExpression)mult.Left;
            //ConstantExpression three = (ConstantExpression)mult.Right;
            //ConstantExpression five = (ConstantExpression)lt.Right;
            //Console.WriteLine("{0} ({1} {2} {3} {4})",lt.NodeType,mult.NodeType,en.Name,
            //    three.Value,five.Value);

            //Func<int, int> addOne = n => n + 1;
            //Console.WriteLine("Result:{0}", addOne(5));
            //Expression<Func<int, int>> addOneExpression = n => n + 1;
            //var addOneFunc = addOneExpression.Compile();
            //Console.WriteLine("Result:{0}",addOneFunc(5));

            Query();


        }
        //Exercise2
        static List<Customer> CreateCustomer()
        {
            return new List<Customer>
            {
                new Customer(1){ Name="Maria Anders",City="Berlin"},
                new Customer(2){ Name="Laurence Lebihan",City="Marseille"},
                new Customer(3){ Name="Elizabeth Brown",City="London"},
                new Customer(4){ Name="Ann Devon",City="London"},
                new Customer(5){ Name="Paolo Accorti",City="Torino"},
                new Customer(6){ Name="Fran Wilson",City="Portland"},
                new Customer(7){ Name="Simon Crowther",City="London"},
                new Customer(8){ Name="Liz Nixon",City="Portland"}
            };
        }
        //Exercise3
        static void TestVar()
        {
            var i = 42;
            var s = "...this is only a test...";
            var a = new[] { 1, 2, 3 };
            var complex = new SortedDictionary<string, List<DateTime>>();
        }
        //Exercise5
        public static List<Customer> FindCustomorByCity(List<Customer> customers,string city)
        {
            //return customers.FindAll(delegate (Customer c) { return c.City == city; });
            return customers.FindAll(c => c.City == city);
        }
        //Exercise7
        static List<Store> CreateStore()
        {
            return new List<Store>
            {
                new Store{ Name="Jim's Hardware",City="Berlin"},
                new Store{ Name="John's Books",City="London"},
                new Store{ Name="Lisa's Flowers",City="Torino"},
                new Store{ Name="Dana's Hadrware",City="London"},
                new Store{ Name="Tim's Pets",City="Portland"},
                new Store{ Name="Scott's Bookds",City="London"},
                new Store{ Name="Paula's",City="London"}
            };
        }

        static void Query()
        {
            var stores = CreateStore();
            foreach (var s in stores.Where(s => s.City == "London"))
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("----------------------------");
            var stores2 = CreateStore();
            IEnumerable<Store> results = from s in stores2
                                         where s.City == "London"
                                         select s;
            foreach (var s in results)
                Console.WriteLine(s);
            Console.WriteLine("----------------------------");
            var numLondon = stores.Count(s => s.City == "London");
            Console.WriteLine("There are {0} stores in London", numLondon);
            Console.WriteLine("----------------------------");
            foreach (var c in CreateCustomer())
            {
                var customerStores = new
                {
                    CustomerId = c.customerId,
                    City = c.City,
                    CustomerName = c.Name,
                    Stores = from s in CreateStore()
                             where s.City == c.City
                             select s
                };
                Console.WriteLine("{0}\t{1}",
                    customerStores.City, customerStores.CustomerName);
                foreach (var store in customerStores.Stores)
                {
                    Console.WriteLine("\t<{0}>", store.Name);
                }
            }
            Console.WriteLine("----------------------------");
            var results1 = from c in CreateCustomer()
                          select new
                          {
                              c.customerId,
                              c.City,
                              CustomerName = c.Name,
                              Stores = CreateStore().Where(s => s.City == c.City)
                          };
            foreach (var result in results1)
            {
                Console.WriteLine("{0}/{1}", result.City, result.CustomerName);
                foreach (var store in result.Stores)
                {
                    Console.WriteLine("\t<{0}>", store.Name);
                }
            }
            Console.WriteLine("----------------------------");
            var results2 = from c in CreateCustomer()
                          join s in CreateStore() on c.City equals s.City
                          select new
                          {
                              CustomerName = c.Name,
                              StoreName = s.Name,
                              c.City,
                          };
            foreach (var r in results2)
                Console.WriteLine("{0}\t{1}\t{2}",
                    r.City, r.CustomerName, r.StoreName);
            Console.WriteLine("----------------------------");
            var results3 = from c in CreateCustomer()
                          join s in CreateStore() on c.City equals s.City
                          group s by c.Name into g
                          select new { CustomerName = g.Key, Count = g.Count() };
            foreach (var r in results3)
                Console.WriteLine("{0}\t{1}", r.CustomerName, r.Count);
            Console.WriteLine("----------------------------");
            var results4 = from c in CreateCustomer()
                          join s in CreateStore() on c.City equals s.City
                          group s by c.Name into g
                          let count = g.Count()
                          orderby count ascending
                          select new { CustomerName = g.Key, Count = count };
            foreach (var r in results4)
            {
                Console.WriteLine("{0}\t{1}",r.CustomerName,r.Count);
            }
        }
    }
}
