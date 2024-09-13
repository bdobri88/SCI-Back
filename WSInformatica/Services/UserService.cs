using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WSInformatica.Models;
using WSInformatica.Models.Common;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;
using WSInformatica.Tools;

namespace WSInformatica.Services
{
    public class UserService : IUserService //Uso interface por si en un futuro deciden 
    {                                       //cambiar la forma de autentificarse,por ejem utilizar un servicio de autentificacion
        private readonly AppSettings _appSettings;//en esta variable tenemos el secreto para crear nuestro TOKEN
        private readonly InfoContext _context;

        public UserService(IOptions<AppSettings> appSettings, InfoContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userresponse = new UserResponse();

                string spassword = Encrypt.GetSHA256(model.Password);
                var usuario = _context.User.Where(u => u.Email == model.Email && u.Password == spassword).FirstOrDefault();

                if (usuario == null) return null;

                userresponse.Email = usuario.Email;
                userresponse.Token = GetToken(usuario);
            
            return userresponse;
        }

        private string GetToken(User usuario) //creamos el token y le damos la confiuracion que queremos
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),//tiempo que dura el token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature) //aca se encrypta
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); // se asigana el token con la descripcion que creamos  
            return tokenHandler.WriteToken(token); //devuevlo el token en string
        }
    }
}
