namespace WebAPI
{
    public interface IUserManagementService
    {
        bool isValidUser(string username, string password); //kullanıcı adı ve şifre kontrolü yapılan interface
    }
}
