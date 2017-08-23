namespace CheckiePyMobile.Models
{
    public class RepositoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsConnected { get; set; }
        public bool IsDisconnected => !IsConnected;
    }
}
