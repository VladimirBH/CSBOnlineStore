using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSBOnlineStore.DataBase.Models
{
    public enum PaymentType
    {
        [Description("Карта")]
        Card,
        [Description("Система быстрых платежей")]
        FPS
    }
}
