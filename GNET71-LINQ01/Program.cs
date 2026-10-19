using LINQ.DataSources;

namespace GNET71_LINQ01
{
    class Program
    {
        static void Main()
        {
            var ProductList = Source.ProductList;
            var CustomerList = Source.CustomerList;

            // 1. Get all products from Seafood
            var result1 = ProductList
                .Where(p => p.Category == "Seafood")
                .Select(p => new { p.ProductName, p.UnitPrice });

            foreach (var p in result1)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");


            // 2. Get only product names
            var result2 = ProductList
                .Select(p => p.ProductName);

            foreach (var name in result2)
                Console.WriteLine(name);


            // 3. Sort products by UnitPrice ascending
            var result3 = ProductList
                .OrderBy(p => p.UnitPrice)
                .Select(p => new { p.ProductName, p.UnitPrice });

            foreach (var p in result3)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");


            // 4. Products with UnitPrice between 10 and 30
            var result4 = ProductList
                .Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);

            foreach (var p in result4)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");


            // 5. In-stock products in Spices & Condiments
            var result5 = ProductList
                .Where(p => p.UnitsInStock > 0 &&
                            p.Category == "Spices & Condiments");

            foreach (var p in result5)
                Console.WriteLine($"{p.ProductName} - {p.UnitsInStock}");


            // 6. Anonymous type with StockStatus
            var result6 = ProductList
                .Select(p => new
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice,
                    StockStatus = p.UnitsInStock > 0
                        ? "Available"
                        : "Out of Stock"
                });

            foreach (var p in result6)
                Console.WriteLine(
                    $"{p.Name} - {p.Price} - {p.StockStatus}");


            // 7. Product name with 1-based position
            var result7 = ProductList
                .Select((p, index) => new
                {
                    Position = index + 1,
                    Name = p.ProductName
                });

            foreach (var p in result7)
                Console.WriteLine($"{p.Position}. {p.Name}");


            // 8. Sort by Category ascending, then UnitPrice descending
            var result8 = ProductList
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);

            foreach (var p in result8)
                Console.WriteLine(
                    $"{p.Category} - {p.ProductName} - {p.UnitPrice}");


            // 9. Beverages sorted by UnitsInStock descending
            var result9 = ProductList
                .Where(p => p.Category == "Beverages")
                .OrderByDescending(p => p.UnitsInStock)
                .Select(p => new
                {
                    p.ProductName,
                    p.UnitsInStock
                });

            foreach (var p in result9)
                Console.WriteLine(
                    $"{p.ProductName} - Stock: {p.UnitsInStock}");


            // 10. Orders from 1997 or later using Query Syntax
            var result10 =
                from customer in CustomerList
                from order in customer.Orders
                where order.OrderDate >= new DateTime(1997, 1, 1)
                select new
                {
                    customer.CustomerID,
                    order.OrderDate
                };

            foreach (var order in result10)
                Console.WriteLine(
                    $"{order.CustomerID} - {order.OrderDate:d}");


            // 11. Position number alongside ProductName
            var result11 = ProductList
                .Select((p, index) => new
                {
                    Position = index + 1,
                    ProductName = p.ProductName
                });

            foreach (var p in result11)
                Console.WriteLine($"{p.Position} - {p.ProductName}");


            // 12. Sort by word length, then case-insensitive alphabetical order
            string[] Arr =
            {
            "aPPLE", "AbAcUs", "bRaNcH",
            "BlUeBeRrY", "ClOvEr", "cHeRry"
        };

            var result12 = Arr
                .OrderBy(word => word.Length)
                .ThenBy(word => word, StringComparer.OrdinalIgnoreCase);

            foreach (var word in result12)
                Console.WriteLine(word);


            // 13. Filter words whose second letter is 'i', then reverse
            var result13 = Arr
                .Where(word => word.Length > 1 &&
                    char.ToLowerInvariant(word[1]) == 'i')
                .Reverse()
                .ToList();

            foreach (var word in result13)
                Console.WriteLine(word);
        }
    }
}
