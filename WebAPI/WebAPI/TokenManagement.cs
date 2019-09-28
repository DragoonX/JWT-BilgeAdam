using Newtonsoft.Json;

namespace WebAPI
{
    // buradaki altsınıf ve özellikleri "appsettings.Develoment.json" dosyasına tanımlanmalıdır.
    [JsonObject("tokenManagement")] //tokenmanagement adında bir altsınıf açıyoruz
    public class TokenManagement
    {
        [JsonProperty("secret")] //altsınıfları özellikleri.
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
