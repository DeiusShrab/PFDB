using System;

namespace PFDALTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu = -1;
            while (menu != 0)
            {
                menu = MainMenu();
                switch (menu)
                {
                    case 1:
                        Test();
                        break;
                    case 2:
                        UpdateBestiary();
                        break;
                }
            }
        }

        private static int MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("1 - Test");
            Console.WriteLine("2 - PFDB Bestiary Update");
            Console.WriteLine("0 - EXIT");
            Console.Write("> ");

            var input = Console.ReadLine();

            int.TryParse(input, out int ret);

            return ret;
        }

        // Menu 1
        private static void Test()
        {
            Console.WriteLine("Test");
        }

        // Menu 2
        private static void UpdateBestiary()
        {
            Console.WriteLine("Not Yet Ready");
            return;
        }
    }
}
