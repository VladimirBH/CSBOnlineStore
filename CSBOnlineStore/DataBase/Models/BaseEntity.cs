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
    }
}
