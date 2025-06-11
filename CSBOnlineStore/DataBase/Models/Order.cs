using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        [Column("order_date")]
        public DateOnly OrderDate { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("status")]
        public Status Status { get; set; }

        [Column("payment_type")]
        public PaymentType PaymentType { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
