using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("Spetification")]
    public class Spetification : BaseEntity
    {
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("unit")]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Column("data_type", TypeName = "data_type_spet")]
        public DataTypeSpet Type { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
