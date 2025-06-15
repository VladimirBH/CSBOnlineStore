namespace CSBOnlineStore.Classes
{
    public class TokenPair
    {
        public int IdRole { set; get; }
        public string AccessToken { set; get; }
        public string RefreshToken { set; get; }
        public int ExpiredInAccessToken { set; get; }
        public int ExpiredInRefreshToken { set; get; }
        public DateTime CreationDateTime { set; get; }
    }
}
