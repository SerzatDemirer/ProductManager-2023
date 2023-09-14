using System;
using Microsoft.EntityFrameworkCore;

namespace ProductManager_2023
{
    class Program
    {
        static FashionDbContext DbContext = new FashionDbContext();


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
                        return;  // Avslutar program
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


        static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Lägg till ny produkt");

            Console.WriteLine("\nNamn:");
            var name = Console.ReadLine();

            Console.WriteLine("SKU:");
            var sku = Console.ReadLine();

            Console.WriteLine("Beskrivning:");
            var description = Console.ReadLine();

            Console.WriteLine("Bild (URL):");
            var imageUrl = Console.ReadLine();

            Console.WriteLine("Pris:");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Ogiltigt pris. Ange ett giltigt pris:");
            }

            var product = new Product
            {
                Name = name,
                SKU = sku,
                Description = description,
                ImageUrl = imageUrl,
                Price = price
            };

            try
            {
                DbContext.Products.Add(product);
                DbContext.SaveChanges();
                Console.WriteLine("\nProdukt sparad!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ett fel inträffade när produkten skulle sparas. Detaljer: " + e.Message);
            }
        }


        private static void SearchProduct()
        {
            Console.WriteLine("Ange SKU eller produktens namn för att söka:");
            var searchTerm = Console.ReadLine();

            var product = DbContext.Products.FirstOrDefault(p => p.SKU == searchTerm || p.Name.Contains(searchTerm));

            if (product != null)
            {
                Console.WriteLine($"Namn: {product.Name}");
                Console.WriteLine($"SKU: {product.SKU}");
                Console.WriteLine($"Beskrivning: {product.Description}");
                Console.WriteLine($"Bild (URL): {product.ImageUrl}");
                Console.WriteLine($"Pris: {product.Price} SEK");
            }
            else
            {
                Console.WriteLine("Ingen produkt med den SKU:n eller namnet hittades.");
            }

        }

        private static void DeleteProduct()
        {
        }

        private static void ExitProgram()
        {
            Console.WriteLine("Tack för att du använde Product Manager!");
            Console.WriteLine("Programmet avslutas nu...");
            System.Threading.Thread.Sleep(2000);
        }
    }
}


