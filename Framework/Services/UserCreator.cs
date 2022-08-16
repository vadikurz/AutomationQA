using Framework.Models;

namespace Framework.Services
{
    public static class UserCreator
    {
        public static User CreateDefaultUser()
        {
            return new User(TestConfigurationReader.configurationModel.UserFirstName, TestConfigurationReader.configurationModel.UserLastName);
        }
    }
}