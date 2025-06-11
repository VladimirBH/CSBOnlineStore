using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("Favorite")]
    public class Favorite : BaseEntity
    {
        [Column("user_id")]
        public int UserId { get; set; }
        
        [Column("product_id")]
        public int ProductId { get;set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
