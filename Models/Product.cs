namespace ShoeInventory.Models
{

    public class Product
    {
     
        private int _id;
        private string _name;
        private string _brand;
        private string _size;
        private string _color;
        private decimal _price;
        private int _quantity;
        private Category _category;
        private Supplier _supplier;

  
        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Product name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Brand cannot be empty.");
                _brand = value.Trim();
            }
        }

        public string Size
        {
            get => _size;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Size cannot be empty.");
                _size = value.Trim();
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Color cannot be empty.");
                _color = value.Trim();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative.");
                _price = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");
                _quantity = value;
            }
        }

        public Category Category
        {
            get => _category;
            set => _category = value ?? throw new ArgumentNullException(nameof(value), "Category cannot be null.");
        }

        public Supplier Supplier
        {
            get => _supplier;
            set => _supplier = value ?? throw new ArgumentNullException(nameof(value), "Supplier cannot be null.");
        }

    
        public decimal TotalValue => _price * _quantity;

 
        public Product(int id, string name, string brand, string size, string color,
                       decimal price, int quantity, Category category, Supplier supplier)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Size = size;
            Color = color;
            Price = price;
            Quantity = quantity;
            Category = category;
            Supplier = supplier;
        }

    
        public override string ToString()
        {
            return $"[{Id}] {Name} | Brand: {Brand} | Size: {Size} | Color: {Color} | " +
                   $"Category: {Category.Name} | Supplier: {Supplier.Name} | " +
                   $"Price: ₱{Price:N2} | Qty: {Quantity}";
        }
    }
}
