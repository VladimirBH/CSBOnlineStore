using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("OrderProduct")]
    public class OrderProduct : BaseEntity
    {
        [Column("order_id")]
        public int OrderId{ get; set; }
        
        [Column("unit_product_id")]
        public int UnitProductId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("UnitProductId")]
        public virtual UnitProduct UnitProduct { get; set; }
    }
}
