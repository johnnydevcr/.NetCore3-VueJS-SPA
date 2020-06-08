using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.DTOs;
using Model.Identity;
using Service;
using Service.Commons;

namespace Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public IdentityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(ApplicationUserRegisterDTO model) {

            var user = new ApplicationUser
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user,model.Password);

            await _userManager.AddToRoleAsync(user, "Seller");

            if (!result.Succeeded) {
                throw new Exception("Error");
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDTO model)
        {
            // ubicar usuario por su correo
            var user = await _userManager.FindByEmailAsync(model.Email);

            var validate = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (validate.Succeeded)
            {
                return Ok(await GenerateToken(user));
            }

            else {
                return BadRequest("Acceso no valido");
            }
        }

        public async Task<string> GenerateToken(ApplicationUser user) {
            //obtener la llave secreta
            var secretKey = _configuration.GetValue<string>("SecretKey");
            //generar llave
            var key = Encoding.ASCII.GetBytes(secretKey);

            //generar claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            //obtener roles del usuario
            var roles = await _userManager.GetRolesAsync(user);
            
            //agregar roles a la lista de claims
            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //agregar claims al token
                Subject = new ClaimsIdentity(claims),
                //definir cuando expira el token
                Expires = DateTime.UtcNow.AddDays(1),
                //metodo de encriptacion
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //crear token
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            //devuelve la cadena con el token
            return tokenHandler.WriteToken(createdToken);
        }
    }
}
