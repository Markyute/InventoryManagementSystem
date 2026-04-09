using ShoeInventory.Models;
using ShoeInventory.Services;

namespace ShoeInventory
{
  
    class Program
    {
       
        private static InventoryService _inventory = new InventoryService();
        private static AuthService _auth = new AuthService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ShowLoginScreen();
        }

        static void ShowLoginScreen()
        {
            while (true)
            {
                ConsoleHelper.ClearAndHeader("SOLE INVENTORY — Shoe Business Management System");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n  Default Accounts:");
                Console.WriteLine("    Admin  → username: admin   | password: admin123");
                Console.WriteLine("    Staff  → username: staff1  | password: staff123");
                Console.ResetColor();
                ConsoleHelper.PrintDivider();

                string username = ConsoleHelper.PromptString("Username");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  > Password: ");
                Console.ResetColor();
                string password = ReadMaskedPassword();

                if (_auth.Login(username, password))
                {
                
                    if (_inventory.GetAllProducts().Count == 0)
                        _inventory.SeedSampleData();

                    ConsoleHelper.PrintSuccess($"Welcome back, {_auth.CurrentUser!.FullName}! ({_auth.CurrentUser.Role})");
                    System.Threading.Thread.Sleep(1000);
                    ShowMainMenu();
                }
                else
                {
                    ConsoleHelper.PrintError("Invalid username or password. Please try again.");
                    ConsoleHelper.PressAnyKey();
                }
            }
        }

       
        static string ReadMaskedPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

   
        static void ShowMainMenu()
        {
            while (true)
            {
                ConsoleHelper.ClearAndHeader("SOLE INVENTORY — Main Menu");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\n  Logged in as: {_auth.CurrentUser!.FullName} [{_auth.CurrentUser.Role}]");
                Console.ResetColor();

                ConsoleHelper.PrintSubHeader("Inventory Menu");
                PrintMenuOption("1", "Add Category");
                PrintMenuOption("2", "Add Supplier");
                PrintMenuOption("3", "Add Product");
                PrintMenuOption("4", "View All Products");
                PrintMenuOption("5", "Search Product");
                PrintMenuOption("6", "Update Product");
                PrintMenuOption("7", "Delete Product");
                PrintMenuOption("8", "Restock Product");
                PrintMenuOption("9", "Deduct Product Stock");
                PrintMenuOption("10", "View Transaction History");
                PrintMenuOption("11", "Show Low Stock Products");
                PrintMenuOption("12", "Compute Total Inventory Value");
                PrintMenuOption("0", "Logout");
                ConsoleHelper.PrintDivider();

                int choice = ConsoleHelper.PromptInt("Enter your choice", 0, 12);

                switch (choice)
                {
                    case 1:  MenuAddCategory(); break;
                    case 2:  MenuAddSupplier(); break;
                    case 3:  MenuAddProduct(); break;
                    case 4:  MenuViewAllProducts(); break;
                    case 5:  MenuSearchProduct(); break;
                    case 6:  MenuUpdateProduct(); break;
                    case 7:  MenuDeleteProduct(); break;
                    case 8:  MenuRestockProduct(); break;
                    case 9:  MenuDeductStock(); break;
                    case 10: MenuViewTransactions(); break;
                    case 11: MenuLowStock(); break;
                    case 12: MenuInventoryValue(); break;
                    case 0:
                        _auth.Logout();
                        ConsoleHelper.PrintSuccess("You have been logged out.");
                        System.Threading.Thread.Sleep(800);
                        ShowLoginScreen();
                        return;
                }
            }
        }

        static void PrintMenuOption(string number, string label)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"    [{number,2}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {label}");
            Console.ResetColor();
        }

        static void MenuAddCategory()
        {
            ConsoleHelper.ClearAndHeader("Add New Category");
            try
            {
                string name = ConsoleHelper.PromptString("Category Name");
                string desc = ConsoleHelper.PromptString("Description (optional)", allowEmpty: true);

                _inventory.AddCategory(name, desc);
                ConsoleHelper.PrintSuccess($"Category '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

      
        static void MenuAddSupplier()
        {
            ConsoleHelper.ClearAndHeader("Add New Supplier");
            try
            {
                string name = ConsoleHelper.PromptString("Supplier Name");
                string contact = ConsoleHelper.PromptString("Contact Person");
                string phone = ConsoleHelper.PromptString("Phone Number");
                string email = ConsoleHelper.PromptString("Email Address");

                _inventory.AddSupplier(name, contact, phone, email);
                ConsoleHelper.PrintSuccess($"Supplier '{name}' added successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

        static void MenuAddProduct()
        {
            ConsoleHelper.ClearAndHeader("Add New Product");
            try
            {
         
                var cats = _inventory.GetAllCategories();
                if (cats.Count == 0)
                {
                    ConsoleHelper.PrintWarning("No categories found. Please add a category first.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                ConsoleHelper.PrintSubHeader("Available Categories");
                cats.ForEach(c => ConsoleHelper.PrintInfo(c.ToString()));


                var sups = _inventory.GetAllSuppliers();
                if (sups.Count == 0)
                {
                    ConsoleHelper.PrintWarning("No suppliers found. Please add a supplier first.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                ConsoleHelper.PrintSubHeader("Available Suppliers");
                sups.ForEach(s => ConsoleHelper.PrintInfo(s.ToString()));
                ConsoleHelper.PrintDivider();

                string name  = ConsoleHelper.PromptString("Product Name");
                string brand = ConsoleHelper.PromptString("Brand");
                string size  = ConsoleHelper.PromptString("Size (e.g., 9, 10, 11)");
                string color = ConsoleHelper.PromptString("Color");
                decimal price = ConsoleHelper.PromptDecimal("Price (₱)", min: 0.01m);
                int qty     = ConsoleHelper.PromptInt("Initial Quantity", min: 0);
                int catId   = ConsoleHelper.PromptInt("Category ID", min: 1);
                int supId   = ConsoleHelper.PromptInt("Supplier ID", min: 1);

                _inventory.AddProduct(name, brand, size, color, price, qty, catId, supId);
                ConsoleHelper.PrintSuccess($"Product '{name}' added successfully!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

     
        static void MenuViewAllProducts()
        {
            ConsoleHelper.ClearAndHeader("All Products");
            var products = _inventory.GetAllProducts();

            if (products.Count == 0)
            {
                ConsoleHelper.PrintWarning("No products in inventory.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            PrintProductTable(products);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  Total Products: {products.Count}");
            Console.ResetColor();
            ConsoleHelper.PressAnyKey();
        }

     
        static void MenuSearchProduct()
        {
            ConsoleHelper.ClearAndHeader("Search Product");
            string query = ConsoleHelper.PromptString("Search by Name, Brand, or ID");

            var results = _inventory.SearchProducts(query);

            if (results.Count == 0)
            {
                ConsoleHelper.PrintWarning($"No products found matching '{query}'.");
            }
            else
            {
                ConsoleHelper.PrintSuccess($"Found {results.Count} result(s):");
                PrintProductTable(results);
            }
            ConsoleHelper.PressAnyKey();
        }

  
        static void MenuUpdateProduct()
        {
            ConsoleHelper.ClearAndHeader("Update Product");
            try
            {
                int id = ConsoleHelper.PromptInt("Enter Product ID to update", min: 1);
                var product = _inventory.GetProductById(id);
                if (product == null)
                {
                    ConsoleHelper.PrintError($"Product with ID {id} not found.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

              
                ConsoleHelper.PrintSubHeader("Current Product Details");
                PrintProductDetail(product);
                ConsoleHelper.PrintDivider();
                ConsoleHelper.PrintWarning("Leave field blank to keep current value.");

                string? name  = ConsoleHelper.PromptString("New Name (or blank)", allowEmpty: true);
                string? brand = ConsoleHelper.PromptString("New Brand (or blank)", allowEmpty: true);
                string? size  = ConsoleHelper.PromptString("New Size (or blank)", allowEmpty: true);
                string? color = ConsoleHelper.PromptString("New Color (or blank)", allowEmpty: true);

                Console.Write("  > New Price (or blank): ");
                string? priceInput = Console.ReadLine();
                decimal? price = decimal.TryParse(priceInput, out decimal p) ? p : (decimal?)null;

            
                var cats = _inventory.GetAllCategories();
                ConsoleHelper.PrintSubHeader("Categories");
                cats.ForEach(c => ConsoleHelper.PrintInfo(c.ToString()));
                Console.Write("  > New Category ID (or blank): ");
                string? catInput = Console.ReadLine();
                int? catId = int.TryParse(catInput, out int cid) ? cid : (int?)null;

                var sups = _inventory.GetAllSuppliers();
                ConsoleHelper.PrintSubHeader("Suppliers");
                sups.ForEach(s => ConsoleHelper.PrintInfo(s.ToString()));
                Console.Write("  > New Supplier ID (or blank): ");
                string? supInput = Console.ReadLine();
                int? supId = int.TryParse(supInput, out int sid) ? sid : (int?)null;

                _inventory.UpdateProduct(id,
                    string.IsNullOrWhiteSpace(name) ? null : name,
                    string.IsNullOrWhiteSpace(brand) ? null : brand,
                    string.IsNullOrWhiteSpace(size) ? null : size,
                    string.IsNullOrWhiteSpace(color) ? null : color,
                    price, catId, supId);

                ConsoleHelper.PrintSuccess("Product updated successfully!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

     
        static void MenuDeleteProduct()
        {
            ConsoleHelper.ClearAndHeader("Delete Product");
            try
            {
                int id = ConsoleHelper.PromptInt("Enter Product ID to delete", min: 1);
                var product = _inventory.GetProductById(id);
                if (product == null)
                {
                    ConsoleHelper.PrintError($"Product with ID {id} not found.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                ConsoleHelper.PrintSubHeader("Product to Delete");
                PrintProductDetail(product);

                if (ConsoleHelper.Confirm("Are you sure you want to DELETE this product?"))
                {
                    _inventory.DeleteProduct(id);
                    ConsoleHelper.PrintSuccess($"Product '{product.Name}' has been deleted.");
                }
                else
                {
                    ConsoleHelper.PrintInfo("Deletion cancelled.");
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

      
        static void MenuRestockProduct()
        {
            ConsoleHelper.ClearAndHeader("Restock Product");
            try
            {
                int id = ConsoleHelper.PromptInt("Enter Product ID to restock", min: 1);
                var product = _inventory.GetProductById(id);
                if (product == null)
                {
                    ConsoleHelper.PrintError($"Product with ID {id} not found.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                ConsoleHelper.PrintSubHeader("Current Stock Info");
                ConsoleHelper.PrintLabel("Product", product.Name);
                ConsoleHelper.PrintLabel("Current Qty", product.Quantity.ToString());

                int qty = ConsoleHelper.PromptInt("Quantity to Restock", min: 1);
                _inventory.RestockProduct(id, qty, _auth.CurrentUser!.Username);
                ConsoleHelper.PrintSuccess($"Restocked {qty} units. New Quantity: {_inventory.GetProductById(id)!.Quantity}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

    
        static void MenuDeductStock()
        {
            ConsoleHelper.ClearAndHeader("Deduct Product Stock");
            try
            {
                int id = ConsoleHelper.PromptInt("Enter Product ID to deduct from", min: 1);
                var product = _inventory.GetProductById(id);
                if (product == null)
                {
                    ConsoleHelper.PrintError($"Product with ID {id} not found.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                ConsoleHelper.PrintSubHeader("Current Stock Info");
                ConsoleHelper.PrintLabel("Product", product.Name);
                ConsoleHelper.PrintLabel("Current Qty", product.Quantity.ToString());

                int qty = ConsoleHelper.PromptInt("Quantity to Deduct", min: 1);
                _inventory.DeductStock(id, qty, _auth.CurrentUser!.Username);
                ConsoleHelper.PrintSuccess($"Deducted {qty} units. New Quantity: {_inventory.GetProductById(id)!.Quantity}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
            ConsoleHelper.PressAnyKey();
        }

      
        static void MenuViewTransactions()
        {
            ConsoleHelper.ClearAndHeader("Transaction History");
            var transactions = _inventory.GetAllTransactions();

            if (transactions.Count == 0)
            {
                ConsoleHelper.PrintWarning("No transactions recorded yet.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            foreach (var tx in transactions)
            {
                string color = tx.Action == "Restock" ? "\u001b[32m" : "\u001b[31m";
                Console.ResetColor();
                ConsoleHelper.PrintInfo(tx.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  Total Transactions: {transactions.Count}");
            Console.ResetColor();
            ConsoleHelper.PressAnyKey();
        }

  
        static void MenuLowStock()
        {
            ConsoleHelper.ClearAndHeader($"Low Stock Products (Qty < {_inventory.LowStockThreshold})");
            var lowStock = _inventory.GetLowStockProducts();

            if (lowStock.Count == 0)
            {
                ConsoleHelper.PrintSuccess("All products are sufficiently stocked.");
            }
            else
            {
                ConsoleHelper.PrintWarning($"{lowStock.Count} product(s) are low on stock:");
                PrintProductTable(lowStock);
            }
            ConsoleHelper.PressAnyKey();
        }

        static void MenuInventoryValue()
        {
            ConsoleHelper.ClearAndHeader("Total Inventory Value");
            var products = _inventory.GetAllProducts();

            if (products.Count == 0)
            {
                ConsoleHelper.PrintWarning("No products in inventory.");
                ConsoleHelper.PressAnyKey();
                return;
            }

        
            ConsoleHelper.PrintSubHeader("Inventory Breakdown");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  {"ID",-5} {"Name",-25} {"Price",10} {"Qty",6} {"Total Value",14}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + new string('─', 64));
            Console.ResetColor();

            foreach (var p in products)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"  {p.Id,-5} {p.Name,-25} {"₱" + p.Price.ToString("N2"),10} {p.Quantity,6} {"₱" + p.TotalValue.ToString("N2"),14}");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + new string('─', 64));
            Console.ForegroundColor = ConsoleColor.Green;
            decimal total = _inventory.ComputeTotalInventoryValue();
            Console.WriteLine($"\n  {"GRAND TOTAL INVENTORY VALUE:",-45} ₱{total:N2}");
            Console.ResetColor();
            ConsoleHelper.PressAnyKey();
        }

  
        static void PrintProductTable(List<Product> products)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  {"ID",-5} {"Name",-20} {"Brand",-12} {"Size",-6} {"Color",-10} {"Price",9} {"Qty",5} {"Category",-12} {"Supplier",-15}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + new string('─', 100));
            Console.ResetColor();

            foreach (var p in products)
            {
           
                Console.ForegroundColor = p.Quantity < _inventory.LowStockThreshold
                    ? ConsoleColor.Yellow : ConsoleColor.White;

                Console.WriteLine($"  {p.Id,-5} {Truncate(p.Name, 20),-20} {Truncate(p.Brand, 12),-12} " +
                                  $"{p.Size,-6} {Truncate(p.Color, 10),-10} {"₱" + p.Price.ToString("N2"),9} " +
                                  $"{p.Quantity,5} {Truncate(p.Category.Name, 12),-12} {Truncate(p.Supplier.Name, 15),-15}");
            }
            Console.ResetColor();
        }

        static void PrintProductDetail(Product p)
        {
            ConsoleHelper.PrintLabel("ID", p.Id.ToString());
            ConsoleHelper.PrintLabel("Name", p.Name);
            ConsoleHelper.PrintLabel("Brand", p.Brand);
            ConsoleHelper.PrintLabel("Size", p.Size);
            ConsoleHelper.PrintLabel("Color", p.Color);
            ConsoleHelper.PrintLabel("Price", $"₱{p.Price:N2}");
            ConsoleHelper.PrintLabel("Quantity", p.Quantity.ToString());
            ConsoleHelper.PrintLabel("Category", p.Category.Name);
            ConsoleHelper.PrintLabel("Supplier", p.Supplier.Name);
            ConsoleHelper.PrintLabel("Total Value", $"₱{p.TotalValue:N2}");
        }

    
        static string Truncate(string value, int maxLength)
        {
            if (value.Length <= maxLength) return value;
            return value[..(maxLength - 2)] + "..";
        }
    }
}
