using static System.Console;
using System.Linq;
using System.Threading;
using System;

namespace ProductManager_2023
{
    class Program
    {
        static FashionDbContext DbContext = new FashionDbContext();

        static void Main(string[] args)
        {

            while (true)
            {
                CursorVisible = false;

                ShowMainMenu();
                CursorVisible = true;
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
                        ExitProgram();
                        return;  // Avslutar program
                    default:
                        WriteLine("Ogiltigt val, försök igen.");
                        Thread.Sleep(2000); // Pausa i 2 sekunder
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
            WriteLine("\n**  1. Lägg till ny produkt");
            WriteLine("**  2. Sök efter produkt");
            WriteLine("**  3. Avsluta programmet");
            WriteLine("\n******************************************************");
            WriteLine("\nVälj ett alternativ genom att ange siffran:");
        }

        static void AddProduct()
        {
            Clear();
            WriteLine("Lägg till ny produkt");
            WriteLine("(Tryck ESC när som helst för att avbryta och återvända till huvudmenyn)\n");

            WriteLine("\nNamn:");
            var name = ReadInput();
            if (string.IsNullOrEmpty(name)) return;

            WriteLine("SKU:");
            var sku = ReadInput();
            if (string.IsNullOrEmpty(sku)) return;

            WriteLine("Beskrivning:");
            var description = ReadInput();
            if (string.IsNullOrEmpty(description)) return;

            WriteLine("Bild (URL):");
            var imageUrl = ReadInput();
            if (string.IsNullOrEmpty(imageUrl)) return;

            WriteLine("Pris:");
            decimal price;
            var priceInput = ReadInput();
            while (!decimal.TryParse(priceInput, out price) || price < 0)
            {
                WriteLine("Ogiltigt pris. Ange ett giltigt pris:");
                priceInput = ReadInput();
                if (string.IsNullOrEmpty(priceInput)) return;
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

        private static string? ReadInput()
        {
            ConsoleKeyInfo keyInfo = ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape) return null;

            return ReadLine();
        }


        private static void SearchProduct()
        {
            Clear();

            WriteLine("Ange SKU eller produktens namn för att söka:");
            string? searchTerm = ReadLine();

            if (string.IsNullOrEmpty(searchTerm))
                return;

            var product = DbContext.Products.FirstOrDefault(p => p.SKU == searchTerm || (p.Name != null && p.Name.Contains(searchTerm)));

            if (product != null)
            {
                DisplayProductDetails(product);
            }
            else
            {
                WriteLine("\nIngen produkt med den SKU:n eller namnet hittades.");
                WriteLine("\nTryck på en tangent för att återgå till huvudmenyn.");
                ReadKey();
                Thread.Sleep(3000);
            }
        }

        private static void DisplayProductDetails(Product product)
        {
            Clear();
            
            WriteLine($"\nNamn: {product.Name}");
            WriteLine($"SKU: {product.SKU}");
            WriteLine($"Beskrivning: {product.Description}");
            WriteLine($"Bild (URL): {product.ImageUrl}");
            WriteLine($"Pris: {product.Price} SEK");

            WriteLine("\n(R)adera eller tryck ESC för att återgå till huvudmenyn.");
            ConsoleKeyInfo keyInfo = ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.R)
            {
                DeleteProduct(product);
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return; // Återvänder till huvudmenyn
            }
            else
            {
                DisplayProductDetails(product); // Om någon annan tangent än R eller ESC trycks, visa detaljerna igen.
            }


        }

        private static void DeleteProduct(Product productToDelete)
        {
            WriteLine($"Produkt hittades: {productToDelete.Name} (SKU: {productToDelete.SKU}). Vill du verkligen ta bort den? (J/N)");
            var confirmation = ReadLine();

            if (confirmation?.ToUpper() == "J")
            {
                Clear();
                ShowProgressBar();

                DbContext.Products.Remove(productToDelete);
                DbContext.SaveChanges();

                WriteLine("\n\nProdukten raderat!");
                Thread.Sleep(3000);
            }
            else
            {
                DisplayProductDetails(productToDelete);
            }
        }

        private static void ShowProgressBar(int delayMilliseconds = 25)
        {
            WriteLine("Tar bort produkt...");
            for (int i = 0; i <= 100; i++)
            {
                Write($"\r[{new string('>', i)}{new string('-', 100 - i)}] {i}%");
                Thread.Sleep(delayMilliseconds);
            }
        }

        private static void ExitProgram()
        {
            WriteLine("Tack för att du använde Product Manager!");
            WriteLine("Programmet avslutas nu...");
            Thread.Sleep(2000);
        }
    }
}
