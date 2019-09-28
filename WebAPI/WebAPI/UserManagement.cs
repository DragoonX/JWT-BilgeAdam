namespace WebAPI
{
    public class UserManagement : IUserManagementService
    {
        public bool isValidUser(string username, string password)
        {
            return true; //iki parametrenin değer alması sonucu true dönülür.
        }
    }
}
