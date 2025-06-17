namespace CSBOnlineStore.Classes
{
    public class ProductFilterDto
    {
        public int CategoryId { get; set; }
        public List<AttributeFilter> Filters { get; set; } = [];
    }
}
