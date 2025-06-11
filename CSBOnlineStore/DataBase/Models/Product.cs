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

        [Column("attributes", TypeName = "jsonb")]
        public string Attributes { get; set; }

    }
}
