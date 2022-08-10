using Framework.Models;

namespace Framework.Services
{
    public static class UserCreator
    {
        private const string DefaultFirstName = "Ivan";
        private const string DefaultLastName = "Romanov";

        public static User CreateDefaultUser()
        {
            return new User(DefaultFirstName, DefaultLastName);
        }
    }
}