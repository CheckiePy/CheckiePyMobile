namespace CheckiePyMobile.Services
{
    public class NetworkService
    {
        public static NetworkService Instance { get; } = new NetworkService();

        public string Token { get; set; }

        protected NetworkService()
        {

        }
    }
}
