
using System.ComponentModel.Design;

namespace Inventory_Management_System
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

        public Item(int id, string name, double price, int quantity)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }
        public string getItemData()
        {
            string data = $"ID: {id},Item Name: {name}, Price: {price}, Quantity: {quantity}";
            return data;
        }

        public class Inventory
        {
            private List<Item> items = new List<Item>();

            public bool IsIDPresent(int id)
            {
                return items.Exists(item => item.id == id);
            }

            public void AddItem(Item item)
            {
                if (IsIDPresent(item.id))
                {
                    Console.WriteLine("ID already exists. Please Try Different ID");
                }
                else
                {
                    items.Add(item);
                    Console.WriteLine("Item added Successfully.");
                }
            }

            public void DisplayItems()
            {
                if (items.Count == 0)
                {
                    Console.WriteLine("Inventoty is Empty");
                }
                else
                {
                    Console.WriteLine("Items Available Here: ");
                    foreach (var item in items)
                    {
                        Console.WriteLine(item.getItemData());
                    }
                }
            }
            public Item FindItemByID(int id)
            {
                return items.Find(item => item.id == id);
            }

            public void UpdateItem(int id, string name, double price, int quantity)
            {
                Item item = FindItemByID(id);
                if (item == null)
                {
                    Console.WriteLine("Item not Found.");
                }
                else
                {
                    item.name = name;
                    item.price = price;
                    item.quantity = quantity;
                    Console.WriteLine("Item updated Successfully");
                }

            }

            public void RemoveItem(int id)
            {
                Item item = FindItemByID(id);
                if (item != null)
                {
                    items.Remove(item);
                    Console.WriteLine("Item removed Successfully");
                }
                else
                {
                    Console.WriteLine("Item not Found");
                }
            }
        }

        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("========Inventory Management System========");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Show All Items");
                Console.WriteLine("3. Find Item By ID");
                Console.WriteLine("4. Update Item By ID");
                Console.WriteLine("5. Remove Item");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                Console.WriteLine("\n============================================");
            
            int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddNewItem(inventory);
                        break;
                    case 2:
                        inventory.DisplayItems();
                        break;
                    case 3:
                        FindItemById(inventory);
                        break;
                    case 4:
                        UpdateItem(inventory);
                        break;
                    case 5:
                        RemoveItem(inventory);
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            
            
            }
        }

         static void AddNewItem(Inventory inventory)
        {
            Console.Write("Enter Item ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Item Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Item Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Item item = new Item(id, name, price, quantity);
            inventory.AddItem(item);
        }

         static void FindItemById(Inventory inventory)
        {
            Console.WriteLine("Enter Item ID to find: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Item item = inventory.FindItemByID(id);
            if (item != null) 
            {
                Console.WriteLine(item.getItemData());
            }
            else
            {
                Console.WriteLine("Item not found. Try a valid ID");
            }
        }

          static void UpdateItem(Inventory inventory)
        {
            Console.WriteLine("Enter Item ID to Update Item Details");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New Price: ");
            double newPrice= Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter New Quantity: ");
            int newQuantity =Convert.ToInt32(Console.ReadLine());
            
            inventory.UpdateItem(id, name, newPrice, newQuantity);
        }

          static void RemoveItem(Inventory inventory)
        {
            Console.WriteLine("Enter Item Id to Remove: ");
            int id = Convert.ToInt32(Console.ReadLine());
            inventory.RemoveItem(id);
        }
    }
}