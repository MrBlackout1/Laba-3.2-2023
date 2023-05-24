using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        string jsonFolderPath = "json_files";

        // Функція для читання JSON-файлу і повернення списку продуктів
        Func<string, Product[]> readJsonFile = (filePath) =>
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Product[]>(json);
        };

        // Делегат типу Predicate<Product> для визначення критеріїв фільтрації
        Predicate<Product> filterCriteria = (product) =>
        {
            // Ваші критерії фільтрації
            return product.Price < 100;
        };

        // Дія для відображення відфільтрованих продуктів
        Action<Product> displayFilteredProduct = (product) =>
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
        };

        // Проходження по всіх JSON-файлах
        for (int i = 1; i <= 10; i++)
        {
            string jsonFilePath = Path.Combine(jsonFolderPath, $"{i}.json");

            if (File.Exists(jsonFilePath))
            {
                Product[] products = readJsonFile(jsonFilePath);

                // Фільтрація та відображення продуктів
                var filteredProducts = products.Where(product => filterCriteria(product));
                foreach (var product in filteredProducts)
                {
                    displayFilteredProduct(product);
                }
            }
        }
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

