namespace VintageCars.Domain.Settings.Response
{
    public class CaptchaKeyResponse
    {
        public string PublicToken { get; set; }

        public CaptchaKeyResponse(string publicToken)
        {
            PublicToken = publicToken;
        }
    }
}
