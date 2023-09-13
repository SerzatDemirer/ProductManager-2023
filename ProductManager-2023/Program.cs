using System;

namespace ProductManager_2023
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        SearchProduct();
                        break;
                    case "3":
                        DeleteProduct();
                        break;
                    case "4":
                        ExitProgram();
                        return;  // Avslutar programmet
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("********** Product Manager **********");
            Console.WriteLine("1. Lägg till ny produkt");
            Console.WriteLine("2. Sök efter produkt");
            Console.WriteLine("3. Radera produkt");
            Console.WriteLine("4. Avsluta programmet");
            Console.WriteLine("Välj ett alternativ genom att ange siffran:");
        }

        private static void AddProduct()
        {
            // Här lägger du till logiken för att lägga till en produkt
        }

        private static void SearchProduct()
        {
            // Här lägger du till logiken för att söka efter en produkt
        }

        private static void DeleteProduct()
        {
            // Här lägger du till logiken för att radera en produkt
        }

        private static void ExitProgram()
        {
            Console.WriteLine("Tack för att du använde Product Manager!");
            Console.WriteLine("Programmet avslutas nu...");
            System.Threading.Thread.Sleep(2000);
        }
    }
}


