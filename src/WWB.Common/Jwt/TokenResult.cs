namespace WWB.Common.Jwt
{
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int Expire { get; set; }
    }
}