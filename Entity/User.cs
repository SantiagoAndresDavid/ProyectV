namespace Entity
{
    public class User
    {
        public string UserName { get; }
        public string UserEmail { get; }
        public string Password { get; }
        public string Rol { get; }

        public User(string userName, string userEmail, string password, string rol)
        {
            UserName = userName;
            UserEmail = userEmail;
            Password = password;
            Rol = rol;
        }
    }
}