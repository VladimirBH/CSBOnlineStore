using System.ComponentModel;

namespace CSBOnlineStore.DataBase.Models
{
    public enum Status
    {
        [Description("Оплачено")]
        Paid,
        [Description("В обработке")]
        Processing,
        [Description("Доставляется")]
        Delivering,
        [Description("Получено")]
        Received
    }
}
