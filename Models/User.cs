namespace ShoeInventory.Models
{
   
    public class User
    {
       
        private int _id;
        private string _username;
        private string _password; 
        private string _fullName;
        private string _role;

   
        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username cannot be empty.");
                _username = value.Trim().ToLower();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                    throw new ArgumentException("Password must be at least 4 characters.");
                _password = value;
            }
        }

        public string FullName
        {
            get => _fullName;
            set => _fullName = value?.Trim() ?? string.Empty;
        }

        public string Role
        {
            get => _role;
            set => _role = string.IsNullOrWhiteSpace(value) ? "Staff" : value.Trim();
        }

   
        public User(int id, string username, string password, string fullName, string role = "Staff")
        {
            Id = id;
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
        }

       
        public bool ValidateCredentials(string username, string password)
        {
            return _username == username.Trim().ToLower() && _password == password;
        }

        public override string ToString()
        {
            return $"[{Id}] {FullName} (@{Username}) | Role: {Role}";
        }
    }
}
