using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("SpetificationProduct")]
    public class SpetificationProduct : BaseEntity
    {
        [Column("value")]
        [MaxLength(100)]
        public string Value { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("spetification_id")]
        public int SpetificationId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("SpetificationId")]
        public virtual Spetification Spetification { get; set; }
    }
}
