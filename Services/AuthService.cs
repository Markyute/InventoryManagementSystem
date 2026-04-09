using ShoeInventory.Models;

namespace ShoeInventory.Services
{
   
    public class AuthService
    {
    
        private List<User> _users;
        private int _nextUserId = 1;

    
        private User? _currentUser;

        public User? CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;

      
        public AuthService()
        {
            _users = new List<User>();
            SeedDefaultUsers();
        }

    
        private void SeedDefaultUsers()
        {
            _users.Add(new User(_nextUserId++, "admin", "admin123", "System Administrator", "Admin"));
            _users.Add(new User(_nextUserId++, "staff1", "staff123", "John Mark C. Idanan", "Staff"));
        }

    
        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.ValidateCredentials(username, password));
            if (user != null)
            {
                _currentUser = user;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public void RegisterUser(string username, string password, string fullName, string role)
        {
            if (_users.Any(u => u.Username == username.Trim().ToLower()))
                throw new InvalidOperationException($"Username '{username}' is already taken.");
            _users.Add(new User(_nextUserId++, username, password, fullName, role));
        }

        public List<User> GetAllUsers() => new List<User>(_users);
    }
}
