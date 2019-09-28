using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    /* Proje oluşturulduğunda miras alınan kısım 'Controller' olarak gelmektedir. Bu kısım 'ControllerBase' olarak değiştirilmelidir. */
    public class AuthenticationController : ControllerBase
    {
        readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost, Route("request")] // POST metodu ile alınacak token 'localhost:5000/api/authentication/request' adresine yönlendirilir.
        public IActionResult RequestToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid) //TokenRequest modeli doğru biçimde getirilemezse model durumu hatalı sonuçlanır.
            {
                return BadRequest(ModelState);
            }

            string token;
            if (_authenticationService.isAuthenticated(tokenRequest,out token))
            {
                return Ok(token); //yetkilendirilmiş iste token iletilir.
            }
            return BadRequest("Invalid Request !!!"); //aksi halde Invalid Request mesajı döner.
        }
    }
}