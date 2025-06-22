using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBOnlineStore.DataBase.Models
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("article")]
        public string Article { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual List<SpetificationProduct> ProductSpetifications { get; set; } = new();

        public virtual List<Photo> Photos { get; set; } = new();
    }
}
