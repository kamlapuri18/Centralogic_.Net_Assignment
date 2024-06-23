using System.Runtime.InteropServices;

class Task
{
    public string Title {  get; set; }
    public string Description { get; set; }
}

namespace ToDo_App
{
    public class Program
    {
        static List<Task> tasks = new List<Task>();
        static void Main(string[] args)
        {
            int choice;
            Console.WriteLine("Welcome To ToDo Application");
            do
            {
                Console.WriteLine("ToDo_App Menu");
                Console.WriteLine("=================");
                Console.WriteLine("Press 1 : Create");
                Console.WriteLine("Press 2 : Display");
                Console.WriteLine("Press 3 : Update");
                Console.WriteLine("Press 4 : Delete");
                Console.WriteLine("Press 5 : Exit");
                Console.WriteLine("===================");
                Console.WriteLine("Enter Your Choice \n");

                choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("--------------------------");

                switch (choice)
                {
                    case 1:
                        Create_Task();
                        break;
                    case 2:
                        Show_Tasks();
                        break;
                    case 3:
                        Update_Task();
                        break;
                    case 4:
                        Delete_Task();
                        break;
                    case 5:
                        Console.WriteLine("Exit Successfully");
                        break;
                    default:
                        Console.WriteLine("Invalid Input, Please Try Again.");
                        break;

                }
            } while (choice != 5) ;
        }

        private static void Create_Task()
        {
            Task newTask = new Task();

            Console.WriteLine("Enter Task Title:-");
            newTask.Title = Console.ReadLine();

            Console.WriteLine("Enter Task Description:-");
            newTask.Description = Console.ReadLine();

            tasks.Add(newTask);
            Console.WriteLine("Your Task is Created Successfully :)");
        }
        private static void Show_Tasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No Task Available");
            }

            Console.WriteLine("Tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Title: {tasks[i].Title} \nDescription: {tasks[i].Description}");

            }

        }    

        private static void Update_Task()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Tasks Not Available \n First Create the Task.");
            }
            else
            {
                Console.WriteLine("Enter Index to Update the task");
                int index = Convert.ToInt32(Console.ReadLine());

                if (index >= 0 && index < tasks.Count)
                {
                    Console.WriteLine("Enter New Title:- ");
                    tasks[index].Title = Console.ReadLine();

                    Console.WriteLine("Enter New Description:- ");
                    tasks[index].Description = Console.ReadLine();

                    Console.WriteLine("Your Title and Description is Updated Successfully!!\n");
                }
                else
                {
                    Console.WriteLine("Sorry,\n Please Enter Valid Index");
                }

            }
        }

        private static void Delete_Task()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Sorry!!\n ToDo List is Empty \n First Please Create the Task.");
            }
            else
            {

                Console.WriteLine("Enter index Of Task To Delete the Task:- ");
                int index = Convert.ToInt32(Console.ReadLine());

                if (index >= 0 && index < tasks.Count)
                {
                    tasks.RemoveAt(index);
                    Console.WriteLine("\n Your Task Deleted Successfully.");
                }
                else
                {
                    Console.WriteLine("Sorry!\n Enter Valid Index");
                }
            }
        }

        }
    }

