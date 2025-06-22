using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Classes
{
    public class ProductFull
    {
        public Product Product { get; set; }
        public List<Spetification> Spetifications { get; set; } = new();
    }
}
