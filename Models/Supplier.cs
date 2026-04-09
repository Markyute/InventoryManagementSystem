namespace ShoeInventory.Models
{

    public class Supplier
    {
  
        private int _id;
        private string _name;
        private string _contactPerson;
        private string _phone;
        private string _email;

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
                    throw new ArgumentException("Supplier name cannot be empty.");
                _name = value.Trim();
            }
        }

        public string ContactPerson
        {
            get => _contactPerson;
            set => _contactPerson = value?.Trim() ?? string.Empty;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value?.Trim() ?? string.Empty;
        }

        public string Email
        {
            get => _email;
            set => _email = value?.Trim() ?? string.Empty;
        }

   
        public Supplier(int id, string name, string contactPerson = "", string phone = "", string email = "")
        {
            Id = id;
            Name = name;
            ContactPerson = contactPerson;
            Phone = phone;
            Email = email;
        }

       
        public override string ToString()
        {
            return $"[{Id}] {Name} | Contact: {ContactPerson} | Phone: {Phone} | Email: {Email}";
        }
    }
}
