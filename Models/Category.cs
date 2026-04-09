namespace ShoeInventory.Models
{
  
    public class Category
    {
   
        private int _id;
        private string _name;
        private string _description;

      
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
                    throw new ArgumentException("Category name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string Description
        {
            get => _description;
            set => _description = value?.Trim() ?? string.Empty;
        }

     
        public Category(int id, string name, string description = "")
        {
            Id = id;
            Name = name;
            Description = description;
        }

    
        public override string ToString()
        {
            return $"[{Id}] {Name}" + (string.IsNullOrEmpty(Description) ? "" : $" - {Description}");
        }
    }
}
