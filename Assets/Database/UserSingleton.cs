namespace Database
{
    public sealed class UserSingleton
    {
        private static UserSingleton instance = null;
        public string Email { get; set; }
        public string Password { get; set; }

        private UserSingleton() { }

        public static UserSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSingleton();
                }
                return instance;
            }
        }
    }
}
