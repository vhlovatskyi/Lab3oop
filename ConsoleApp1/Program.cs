using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public bool IsNew { get; set; }

        public Item(string name, double price, double discount, int quantity, bool isNew)
        {
            Name = name;
            Price = price;
            Discount = discount;
            Quantity = quantity;
            IsNew = isNew;
        }

        public double GetTotalPrice() => Price * Quantity;

        public double GetDiscountAmount() => GetTotalPrice() * (Discount / 100);
    }

    public class Phone : Item
    {
        public Phone(double price, double discount, int quantity, bool isNew)
            : base("Phone", price, discount, quantity, isNew) { }
    }

    public class Tv : Item
    {
        public Tv(double price, double discount, int quantity, bool isNew)
            : base("Tv", price, discount, quantity, isNew) { }
    }

    public class Laptop : Item
    {
        public Laptop(double price, double discount, int quantity, bool isNew)
            : base("Laptop", price, discount, quantity, isNew) { }
    }

    public class Mouse : Item
    {
        public Mouse(double price, double discount, int quantity, bool isNew)
            : base("Mouse", price, discount, quantity, isNew) { }
    }

    public class Keyboard : Item
    {
        public Keyboard(double price, double discount, int quantity, bool isNew)
            : base("Keyboard", price, discount, quantity, isNew) { }
    }

    public class Karman
    {
        private List<Item> items = new List<Item>();

        public static Karman operator +(Karman karman, Item item)
        {
            karman.items.Add(item);
            return karman;
        }

        public double GetTotalPrice()
        {
            return items.Sum(item => item.GetTotalPrice());
        }

        public double GetTotalDiscount()
        {
            return items.Sum(item => item.GetDiscountAmount());
        }

        public static Karman operator -(Karman karman, double discount)
        {
            foreach (var item in karman.items)
            {
                item.Price -= item.Price * (discount / 100);
            }
            return karman;
        }

        public void ShowItems()
        {
            Console.WriteLine("Items in Karman:");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name}: Price = {item.Price}, Quantity = {item.Quantity}, Discount = {item.Discount}%, IsNew = {item.IsNew}");
            }
        }

        public double GetTotal()
        {
            return GetTotalPrice() - GetTotalDiscount();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Karman karman = new Karman();

            karman += new Phone(1000, 10, 2, true);
            karman += new Tv(2000, 5, 1, false);
            karman += new Laptop(1500, 15, 1, true);
            karman += new Mouse(300, 5, 2, true);
            karman += new Keyboard(500, 5, 1, false);

            karman.ShowItems();
            Console.WriteLine($"Total price: {karman.GetTotalPrice()}");
            Console.WriteLine($"Total discount: {karman.GetTotalDiscount()}");
            Console.WriteLine($"Total price after discount: {karman.GetTotal()}");
        }
    }
}
