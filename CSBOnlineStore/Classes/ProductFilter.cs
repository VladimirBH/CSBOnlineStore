namespace CSBOnlineStore.Classes
{
    public class ProductFilter
    {
        public int CategoryId { get; set; }
        public List<AttributeFilter> Filters { get; set; } = [];
    }
}
