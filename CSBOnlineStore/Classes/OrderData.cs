using System.Diagnostics.Contracts;
using System.Net;

namespace CSBOnlineStore.Classes
{
    public class OrderData
    {
        public Dictionary<int, int> Products { get; set; }
        public int UserId { get; set; }
    }
}
