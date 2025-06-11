using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        [Column("name")]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
