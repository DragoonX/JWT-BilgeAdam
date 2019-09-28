namespace WebAPI
{
    public interface IAuthenticationService
    {
        bool isAuthenticated(TokenRequest tokenRequest, out string token);
        //yetkilendirme işlemi boolean dönmektedir. çıkış değeri string dönecektir.
    }
}
