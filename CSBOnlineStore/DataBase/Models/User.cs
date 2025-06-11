using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Column("first_name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column("second_name")]
        [MaxLength(50)]
        public string SecondName { get; set; }

        [Column("phone")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Column("username")]
        [MaxLength(20)]
        public string Username { get; set; }

        [Column("password")]
        [MaxLength(250)]
        public string Password { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
