using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI
{
    public class TokenAuthenticationService : IAuthenticationService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IOptions<TokenManagement> _tokenManagement;

        public TokenAuthenticationService(IUserManagementService userManagementService, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = userManagementService;
            _tokenManagement = tokenManagement;
        }
        public bool isAuthenticated(TokenRequest tokenRequest, out string token)
        {
            token = null;
            if (!_userManagementService.isValidUser(tokenRequest.Username, tokenRequest.Password))
            {
                return false;
            }

            var claims = new[] //claim(talep), byte cinsinden alınacaktır.
            {
                new Claim(ClaimTypes.Name,tokenRequest.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenManagement.Value.Secret));
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256); //credentials(güven belgesi)
            /*  * appsettings.Development.json dosyasındaki Secret değeri ASCII kodları halinde bytelara ayrılır
                * ve simetrik şifreleme yöntemi için hazırlanır. Elde edilen byte verisi SHA256 ile şifrelenir. */

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _tokenManagement.Value.Issuer,
                _tokenManagement.Value.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.Value.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return true;
        }
    }
}
