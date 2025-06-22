using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Classes
{
    public class OrderCreate
    {
        public List<int> ProductIds { get; set; } = new();
        public int? UserId { get; set; }
        public PaymentType paymentType { get; set; }
        public string Address { get; set; }
    }
}
