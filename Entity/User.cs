namespace Entity
{
    public class User
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(string userName, string userEmail, string password, string role)
        {
            UserName = userName;
            UserEmail = userEmail;
            Password = password;
            Role = role;
        }

        public User()
        {
        }
    }
}