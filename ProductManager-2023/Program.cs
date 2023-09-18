using static System.Console;
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
                var choice = ReadLine();

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
                        WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        private static void ClearScreenWithColors(ConsoleColor textColor)
        {
            Clear();
            ForegroundColor = textColor;
        }

        private static void ShowMainMenu()
        {
            Clear();
            ClearScreenWithColors(ConsoleColor.DarkYellow);

            WriteLine("\n********** Product Manager ***************************");
            WriteLine("\n1. Lägg till ny produkt");
            WriteLine("2. Sök efter produkt");
            WriteLine("3. Radera produkt");
            WriteLine("4. Avsluta programmet");
            WriteLine("\nVälj ett alternativ genom att ange siffran:");
        }

        static void AddProduct()
        {
            Clear();
            WriteLine("Lägg till ny produkt");

            WriteLine("\nNamn:");
            var name = ReadLine();

            WriteLine("SKU:");
            var sku = ReadLine();

            WriteLine("Beskrivning:");
            var description = ReadLine();

            WriteLine("Bild (URL):");
            var imageUrl = ReadLine();

            WriteLine("Pris:");
            decimal price;
            while (!decimal.TryParse(ReadLine(), out price) || price < 0)
            {
                WriteLine("Ogiltigt pris. Ange ett giltigt pris:");
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
                WriteLine("\nProdukt sparad!");
                Thread.Sleep(2000);
                DbContext.Products.Add(product);
                DbContext.SaveChanges();


            }
            catch (Exception e)
            {
                WriteLine("Ett fel inträffade när produkten skulle sparas. Detaljer: " + e.Message);
            }
        }

        private static void SearchProduct()
        {
            Clear();

            WriteLine("Ange SKU eller produktens namn för att söka:");
            string searchTerm = ReadLine() ?? string.Empty;

            // Söker baserat på SKU eller produktens namn
            var product = DbContext.Products.FirstOrDefault(p => p.SKU == searchTerm || (p.Name != null && p.Name.Contains(searchTerm)));

            if (product != null)
            {
                WriteLine($"\nNamn: {product.Name}");
                WriteLine($"SKU: {product.SKU}");
                WriteLine($"Beskrivning: {product.Description}");
                WriteLine($"Bild (URL): {product.ImageUrl}");
                WriteLine($"Pris: {product.Price} SEK");
                ReadKey();
            }
            else
            {
                WriteLine("\nIngen produkt med den SKU:n eller namnet hittades.");
                WriteLine("\nTryck på en tangent för att återgå till huvudmenyn.");
                ReadKey();
                Thread.Sleep(3000);
            }
        }

        private static void DeleteProduct()
        {
            Clear();
            WriteLine("Ange SKU eller produktens namn du vill ta bort:");
            var searchTerm = ReadLine() ?? string.Empty;

            var productToDelete = DbContext.Products.FirstOrDefault(p => p.SKU == searchTerm || p.Name != null && p.Name.Contains(searchTerm));

            if (productToDelete != null)
            {
                WriteLine($"Produkt hittades: {productToDelete.Name} (SKU: {productToDelete.SKU}). Vill du verkligen ta bort den? (J/N)");
                var confirmation = ReadLine() ?? string.Empty.ToUpper();

                if (confirmation == "J")
                {
                    Clear();
                    ShowProgressBar();
                    WriteLine("\n\nProdukten raderat!");
                    Thread.Sleep(3000);
                    DbContext.Products.Remove(productToDelete);
                    DbContext.SaveChanges();
                }
                else
                {
                    WriteLine("Produkten togs inte bort.");
                }
            }
            else
            {
                WriteLine("Ingen produkt med den SKU:n eller namnet hittades.");
            }
        }

        private static void ShowProgressBar(int delayMilliseconds = 25)
        {
            WriteLine("Tar bort produkt...");
            for (int i = 0; i <= 100; i++)
            {
                Write($"\r[{new string('=', i)}{new string(' ', 100 - i)}] {i}%");
                System.Threading.Thread.Sleep(delayMilliseconds);
            }
        }

        private static void ExitProgram()
        {
            WriteLine("Tack för att du använde Product Manager!");
            WriteLine("Programmet avslutas nu...");
            System.Threading.Thread.Sleep(2000);
        }

    }
     
}


