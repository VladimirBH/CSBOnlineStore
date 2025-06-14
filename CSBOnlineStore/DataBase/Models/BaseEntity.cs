using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id {get; set; }

        [Column("created_at")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}
