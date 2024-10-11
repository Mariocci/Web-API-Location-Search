namespace WebApiLocationSearch.Middleware
{
    public class BasicAuthHelper
    {
        public static (string username, string password) DecodeBase64Credentials(string authHeader)
        {
            string base64Credentials = authHeader.Substring("Basic ".Length).Trim();

            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));

            string[] parts = credentials.Split(':');
            return (parts[0], parts[1]);
        }
    }
}