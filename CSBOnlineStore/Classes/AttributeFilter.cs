using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Classes
{
    public class AttributeFilter
    {
        public string Name { get; set; } = null!;
        public DataTypeSpet Type { get; set; }
        public string? Value { get; set; }
        public double? Min { get; set; } 
        public double? Max { get; set; }
    }
}
