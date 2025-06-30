using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WSInformatica.Models;
using WSInformatica.Models.Common;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;
using BCrypt.Net;
using Azure.Core;
using WSInformatica.DTOs.UserDTO;

namespace WSInformatica.Services
{
    public class UserService : IUserService 
    {                                       
        private readonly AppSettings _appSettings;
        private readonly InfoContext _context;

        public UserService(IOptions<AppSettings> appSettings, InfoContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public UserResponse Auth(AuthRequest model)
        {
            var efectivo = _context.Efectivo
                         .Include(e => e.User)
                         .FirstOrDefault(e => e.Legajo == model.Legajo);

            if (efectivo == null || efectivo.User == null)
            {
                throw new KeyNotFoundException("Usuario o Contraseña erronea.");
            }

            var usuario = efectivo.User;

            if (!BCrypt.Net.BCrypt.Verify(model.Password, usuario.Password))
            {
                throw new KeyNotFoundException("Usuario o Contraseña erronea.");
            }

            var userResponse = new UserResponse
            {
                Legajo = efectivo.Legajo,
                EsAdmin = usuario.EsAdmin,
                Token = GetToken(usuario, efectivo.Legajo)
            };

            return userResponse;
        }


        private string GetToken(User usuario, int legajo) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim("IdEfectivo", usuario.IdEfectivo.ToString()), 
                    new Claim("Legajo", legajo.ToString()), 
                    new Claim(ClaimTypes.Role, usuario.EsAdmin ? "Admin" : "User") 
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,   
                Audience = _appSettings.Audience

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void CreateUser(CreateUserRequest request)
        {
            var efectivo = _context.Efectivo
                                   .Include(e => e.User) 
                                   .FirstOrDefault(e => e.Legajo == request.Legajo);

            if (efectivo == null)
            {
                throw new Exception("El efectivo con el legajo especificado no existe.");
            }

            if (efectivo.User != null)
            {
                throw new Exception("Este efectivo ya tiene un usuario asociado.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                IdEfectivo = efectivo.Id,
                Password = hashedPassword,
                EsAdmin = request.EsAdmin
            };

            _context.User.Add(newUser);
            _context.SaveChanges();
        }

        public void ChangePassword(ChangePasswordRequest request)
        {
            var user = _context.User.FirstOrDefault(u => u.Id == request.IdUsuario);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }
           
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                throw new Exception("La contraseña actual es incorrecta.");
            }

            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                        
            user.Password = newHashedPassword;
            _context.User.Update(user); 
            _context.SaveChanges();           
        }

        public User UpdateUser(int userId, string? newPassword, bool? newIsAdmin)
        {
            var userToUpdate = _context.User.Find(userId);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            if (newIsAdmin.HasValue)
            {
                userToUpdate.EsAdmin = newIsAdmin.Value;
            }

            _context.SaveChanges();
            return userToUpdate;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _context.User
                .Include(u => u.Efectivo) 
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                   
                    Legajo = u.Efectivo != null ? u.Efectivo.Legajo.ToString() : null,
                    Nombre = u.Efectivo != null ? u.Efectivo.Nombre : "N/A",
                    Apellido = u.Efectivo != null ? u.Efectivo.Apellido : "N/A",
                    IsAdmin = u.EsAdmin 
                })
                .ToList();
        }
    }
}
