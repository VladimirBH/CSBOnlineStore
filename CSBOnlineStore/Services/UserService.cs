using CSBOnlineStore.Classes;
using CSBOnlineStore.DataBase;
using CSBOnlineStore.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace CSBOnlineStore.Services
{
    public class UserService
    {
        private static TokenService _tokenService;
        private static CSBContext _context;
        private readonly IConfiguration _configuration;
        public UserService(CSBContext context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public TokenPair AuthenticateUser(Login login) 
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username);
            if (user == null)
            {
                return null;
            }
            var passwordHasher = new PasswordHasher();
            if (passwordHasher.Validate(user.Password, login.Password))
            {
                var tokenPair = new TokenPair
                {
                    AccessToken = _tokenService.BuildAccessToken(_configuration["JWT:Key"], _configuration["JWT:Issuer"], user),
                    RefreshToken = _tokenService.BuildRefreshToken(_configuration["JWT:Key"], _configuration["JWT:Issuer"], user),
                    ExpiredInAccessToken = int.Parse(_configuration["JWT:AccessTokenLifeTime"]),
                    ExpiredInRefreshToken = int.Parse(_configuration["JWT:RefreshTokenLifeTime"]),
                    IdRole = user.RoleId,
                    CreationDateTime = DateTime.Now
                };
                return tokenPair;
            }
            else 
            {
                return null;
            }
        }

        public User RegisterUser(User user) 
        {
            var checkUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (checkUser != null) 
            {
                return null;
            }
            var passwordHasher = new PasswordHasher();
            user.Password = passwordHasher.Hash(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public int GetUserIdFromRefreshToken(string refreshToken)
        {
            if (_tokenService.IsTokenValid(_configuration["JWT:Key"], _configuration["JWT:Issuer"], refreshToken))
                throw new AuthenticationException();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(refreshToken);
            var id = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            if (id == null)
            {
                throw new AuthenticationException();
            }

            return int.Parse(id);

        }

        public int GetUserIdFromAccessToken(string accessToken)
        {
            if (_tokenService.IsTokenValid(_configuration["JWT:Key"], _configuration["JWT:Issuer"], accessToken))
                throw new AuthenticationException();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(accessToken);
            var login = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            if (login == null)
            {
                throw new AuthenticationException();
            }

            return GetByLogin(login).Id;

        }

        public TokenPair RefreshPairTokens(string refreshToken)
        {
            var user = GetById(GetUserIdFromRefreshToken(refreshToken));
            var tokenPair = new TokenPair
            {
                AccessToken = _tokenService.BuildAccessToken(_configuration["JWT:Key"], _configuration["JWT:Issuer"], user),
                RefreshToken = _tokenService.BuildRefreshToken(_configuration["JWT:Key"], _configuration["JWT:Issuer"], user),
                ExpiredInAccessToken = int.Parse(_configuration["JWT:AccessTokenLifeTime"]),
                ExpiredInRefreshToken = int.Parse(_configuration["JWT:RefreshTokenLifeTime"]),
                IdRole = user.RoleId,
                CreationDateTime = DateTime.Now
            };
            return tokenPair;
        }

        public List<User> GetAllWithForeignKey()
        {
            return _context.Users.Include(x => x.Role).ToList();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
        }

        public User GetByLogin(string login)
        {
            return _context.Users.Include(r => r.Role).FirstOrDefault(u => u.Username == login);
        }

        public User GetById(int id)
        {
            return _context.Users.Include(r => r.Role).FirstOrDefault(u => (u.Id == id));
        }

        public User GetCurrentUserInfo(string accessToken)
        {
            return _context.Users.Include(r => r.Role)
                .FirstOrDefault(u => u.Id == GetUserIdFromAccessToken(accessToken));
        }
    }
}
