using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("CategorySpetification")]
    public class CategorySpetification : BaseEntity
    {
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("spetification_id")]
        public int SpetificationId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("SpetificationId")]
        public virtual Spetification Spetification { get; set; }
    }
}
