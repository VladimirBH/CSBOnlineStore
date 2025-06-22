using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;

namespace CSBOnlineStore.Services
{
    public class OrderService
    {
        private static CSBContext _context;
        private readonly IConfiguration _configuration;

        public OrderService(CSBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<Order> GetUserOrder(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public Order GetOrder(int orderId) 
        {
            return _context.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public Order CreateOrder(OrderCreate orderCreate) 
        {
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                Address = orderCreate.Address,
                PaymentType = orderCreate.paymentType,
                Status = Status.Paid
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var prodId in orderCreate.ProductIds) 
            {
                var unitProduct = _context.UnitProducts.FirstOrDefault(up => up.ProductId == prodId && up.DeletedAt == null);
                var orderProduct = new OrderProduct()
                {
                    OrderId = order.Id,
                    UnitProductId = unitProduct.Id
                };
                _context.OrderProducts.Add(orderProduct);
                unitProduct.DeletedAt = DateTime.Now;
                _context.Update(unitProduct);
                _context.SaveChanges();
            }
            return order;
        }
    }
}
