using ShoeInventory.Models;

namespace ShoeInventory.Services
{
   
    public class InventoryService
    {
    
        private List<Product> _products;
        private List<Category> _categories;
        private List<Supplier> _suppliers;
        private List<TransactionRecord> _transactions;

        private int _nextProductId = 1;
        private int _nextCategoryId = 1;
        private int _nextSupplierId = 1;
        private int _nextTransactionId = 1;

      
        public int LowStockThreshold { get; set; } = 5;

      
        public InventoryService()
        {
            _products = new List<Product>();
            _categories = new List<Category>();
            _suppliers = new List<Supplier>();
            _transactions = new List<TransactionRecord>();
        }

        // ==================== CATEGORY METHODS ====================
        // Handles adding and retrieving shoe categories

        public void AddCategory(string name, string description = "")
        {
         
            if (_categories.Any(c => c.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Category '{name}' already exists.");

            var category = new Category(_nextCategoryId++, name, description);
            _categories.Add(category);
        }

        public List<Category> GetAllCategories() => new List<Category>(_categories);

        public Category? GetCategoryById(int id) => _categories.FirstOrDefault(c => c.Id == id);

        // ==================== SUPPLIER METHODS ====================
        // Handles adding and retrieving shoe suppliers

        public void AddSupplier(string name, string contactPerson, string phone, string email)
        {
            if (_suppliers.Any(s => s.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Supplier '{name}' already exists.");

            var supplier = new Supplier(_nextSupplierId++, name, contactPerson, phone, email);
            _suppliers.Add(supplier);
        }

        public List<Supplier> GetAllSuppliers() => new List<Supplier>(_suppliers);

        public Supplier? GetSupplierById(int id) => _suppliers.FirstOrDefault(s => s.Id == id);

        // ==================== PRODUCT METHODS ====================

        public void AddProduct(string name, string brand, string size, string color,
                               decimal price, int quantity, int categoryId, int supplierId)
        {
            var category = GetCategoryById(categoryId)
                ?? throw new InvalidOperationException("Invalid category ID.");
            var supplier = GetSupplierById(supplierId)
                ?? throw new InvalidOperationException("Invalid supplier ID.");

            var product = new Product(_nextProductId++, name, brand, size, color,
                                      price, quantity, category, supplier);
            _products.Add(product);
        }

        public List<Product> GetAllProducts() => new List<Product>(_products);

        public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

       
        public List<Product> SearchProducts(string query)
        {
            query = query.Trim().ToLower();
            return _products.Where(p =>
                p.Name.ToLower().Contains(query) ||
                p.Brand.ToLower().Contains(query) ||
                p.Id.ToString() == query
            ).ToList();
        }

        public void UpdateProduct(int id, string? name, string? brand, string? size,
                                  string? color, decimal? price, int? categoryId, int? supplierId)
        {
            var product = GetProductById(id)
                ?? throw new InvalidOperationException($"Product with ID {id} not found.");

            if (!string.IsNullOrWhiteSpace(name)) product.Name = name;
            if (!string.IsNullOrWhiteSpace(brand)) product.Brand = brand;
            if (!string.IsNullOrWhiteSpace(size)) product.Size = size;
            if (!string.IsNullOrWhiteSpace(color)) product.Color = color;
            if (price.HasValue) product.Price = price.Value;

            if (categoryId.HasValue)
            {
                var cat = GetCategoryById(categoryId.Value)
                    ?? throw new InvalidOperationException("Invalid category ID.");
                product.Category = cat;
            }

            if (supplierId.HasValue)
            {
                var sup = GetSupplierById(supplierId.Value)
                    ?? throw new InvalidOperationException("Invalid supplier ID.");
                product.Supplier = sup;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id)
                ?? throw new InvalidOperationException($"Product with ID {id} not found.");
            _products.Remove(product);
        }

        // ==================== STOCK METHODS ====================

        public void RestockProduct(int productId, int quantity, string performedBy)
        {
            if (quantity <= 0)
                throw new ArgumentException("Restock quantity must be greater than zero.");

            var product = GetProductById(productId)
                ?? throw new InvalidOperationException($"Product with ID {productId} not found.");

            int before = product.Quantity;
            product.Quantity += quantity;
            int after = product.Quantity;

         
            var record = new TransactionRecord(
                _nextTransactionId++, product.Id, product.Name,
                "Restock", quantity, before, after, performedBy);
            _transactions.Add(record);
        }

        public void DeductStock(int productId, int quantity, string performedBy)
        {
            if (quantity <= 0)
                throw new ArgumentException("Deduct quantity must be greater than zero.");

            var product = GetProductById(productId)
                ?? throw new InvalidOperationException($"Product with ID {productId} not found.");

            if (product.Quantity < quantity)
                throw new InvalidOperationException(
                    $"Insufficient stock. Available: {product.Quantity}, Requested: {quantity}");

            int before = product.Quantity;
            product.Quantity -= quantity;
            int after = product.Quantity;

           
            var record = new TransactionRecord(
                _nextTransactionId++, product.Id, product.Name,
                "Deduct", quantity, before, after, performedBy);
            _transactions.Add(record);
        }

        // ==================== REPORTS ====================

        public List<TransactionRecord> GetAllTransactions() => new List<TransactionRecord>(_transactions);

        public List<Product> GetLowStockProducts()
            => _products.Where(p => p.Quantity < LowStockThreshold).ToList();

        public decimal ComputeTotalInventoryValue()
            => _products.Sum(p => p.TotalValue);

        // ==================== SAMPLE DATA ====================

       
        public void SeedSampleData()
        {
            // Seed Categories
            AddCategory("Sneakers", "Casual and athletic footwear");
            AddCategory("Boots", "Ankle and long boots");
            AddCategory("Sandals", "Open-toe summer footwear");
            AddCategory("Formal", "Dress shoes and oxfords");
            AddCategory("Kids", "Children's footwear");

            // Seed Suppliers
            AddSupplier("Nike Philippines", "Juan Cruz", "09171234567", "juan@nikeph.com");
            AddSupplier("Adidas Distrib. PH", "Maria Santos", "09281234567", "maria@adidaph.com");
            AddSupplier("Local Crafts Co.", "Pedro Reyes", "09391234567", "pedro@localcrafts.ph");

            // Seed Products
            AddProduct("Air Max 90", "Nike", "10", "White/Red", 5999.00m, 20, 1, 1);
            AddProduct("Ultraboost 22", "Adidas", "9", "Black", 7499.00m, 15, 1, 2);
            AddProduct("Chelsea Boot", "Clarks", "8", "Brown", 4599.00m, 3, 2, 3);
            AddProduct("Havaianas Classic", "Havaianas", "7", "Blue", 799.00m, 40, 3, 3);
            AddProduct("Oxford Brogue", "Rusty Lopez", "9", "Black", 3200.00m, 2, 4, 3);
            AddProduct("Pepito Kids Sneaker", "Tiongs", "5", "Pink", 650.00m, 8, 5, 3);
        }
    }
}
