using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("UnitProduct")]
    public class UnitProduct : BaseEntity
    {
        [Column("deleted_at")]
        [DataType(DataType.DateTime)]
        public DateTime DeletedAt { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
