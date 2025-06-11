using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("parent_id")]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category? Parent { get; set; }
    }
}
