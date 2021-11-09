using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Services;
using ApiPreAceleracionAlkemy.ViewModel.AuthView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [Controller]
    [Route(template:"api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;
        public AuthenticationController(UserManager<User> userManager,SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }
        
        [HttpPost]
        [Route("register")]
        //Se utilizo un fromBody ya que con el ViewModel Solicitaba los datos como query //
        public async  Task<IActionResult> RegisterUser([FromBody]RegisterRequestModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if(userExist != null)
            {
                return BadRequest(new { 
                    Status = "Error",
                    Messege = $"El nombre de usuario {model.Username} ya existe."
                });
            }
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Messege = $"La creacion del usuario ha fallado!{string.Join(",", result.Errors.Select(e => e.Description))} "
                    }
                    );
            }
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _mailService.SendEmail(user);
            return Ok(new { 
                Status="Success",
                Messege=$"Se ha creado el usuario {user.UserName} correctamente!"
            });
        }

        [HttpPost]
        [Route(template:"registerAdmin")]
        public async Task<IActionResult> RegistroAdmin([FromBody]RegisterRequestModel model)
        {
            
            var existUser = await _userManager.FindByNameAsync(model.Username);
            if (existUser != null)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Messege = $"El nombre de usuario {model.Username} ya existe."
                });
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Messege = $"La creacion del usuario ha fallado!{string.Join(",", result.Errors.Select(e => e.Description))} "
                    }
                    );
            }

            if(!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            if(!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            await _mailService.SendEmail(user);

            return Ok(new
            {
                Status = "Success",
                Messege = $"Se ha creado el usuario {user.UserName} correctamente!"
            });

        }

        [HttpPost]
        [Route("login")]
        public async  Task<IActionResult> LoginUser([FromBody] LoginViewModel model)
        {
           var result = await _signInManager.PasswordSignInAsync(model.UserName,model.Password,false ,false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.UserName);
                if (currentUser.IsActive)
                {
                    return Ok(await GetToken(currentUser));
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                   new
                   {
                       Status = "Error",
                       Messege = $"El usuario {model.UserName} no esta autorizado"
                   });
        }

        private async Task<LoginResponseViewModel> GetToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var AuthClaims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            AuthClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiKeySeguraParaValidacionDeaccesoALosDatos"));
            var token = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "https://localhost:5000",
                expires: DateTime.Now.AddDays(1),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
