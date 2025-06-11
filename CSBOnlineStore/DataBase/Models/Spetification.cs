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

        [Column("data_type")]
        [MaxLength(20)]
        public string DataType { get; set; }
    }
}
