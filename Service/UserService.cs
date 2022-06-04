using Microsoft.Extensions.Options;
using BCrypt.Net;
using TestApis.Authorization;
using TestApis.Datos;
using TestApis.Helpers;
using TestApis.Models;

namespace TestApis.Service
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private IJwtUtils jwtUtils;
        private readonly AppSettings appsettings;

        public UserService(ApplicationDbContext context,IJwtUtils jwtUtils,IOptions<AppSettings> appSettings)
        {
            this._context = context;
            this.jwtUtils = jwtUtils;
            this.appsettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            User? user = this._context.users.FirstOrDefault(x=>x.Username==model.Username); //busca en db el username
            //!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash)
            if (user == null || !model.Password.Equals(user.PasswordHash))
                throw new AppException("username o password incorrecto");

            string jwtToken = this.jwtUtils.GenerateJwtToken(user); //genera el token

            return new AuthenticateResponse(user,jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return this._context.users;  
        }

        public User GetById(int id)
        {
            User? user = this._context.users.Find(id);
            if (user == null)
                throw new KeyNotFoundException("user not found");
            return user;
        }
    }
}
