using CSBOnlineStore.DataBase;

namespace CSBOnlineStore.Services
{
    public class OrderService
    {
        private static TokenService _tokenService;
        private static CSBContext _context;
        private readonly IConfiguration _configuration;
        public OrderService(CSBContext context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

    }
}
