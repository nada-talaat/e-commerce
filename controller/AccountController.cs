using Ecommerce1.DTO;
using Ecommerce1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce1.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //the usermanager injection:3CHAN A2DR A3ML SAVE FEL DB
        private UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;

        }




        //reg action:
        [HttpPost("Registration")] // api/account/registration
        public async Task<IActionResult> Registration(RegUserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PhoneNumber=userDTO.Phone,
                Age=userDTO.Age
            };

            IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                return Ok("account added");
            }
            else
            {
                return BadRequest(result.Errors.FirstOrDefault());
            }

        }



        //login action :
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginUserDTO UserDTO)
        {
            //check on the username:
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(UserDTO.UserName);
                if (user != null)
                {
                    //check on the pass:
                    bool found = await userManager.CheckPasswordAsync(user, UserDTO.Password);
                    if (found)
                    {
                        //create the token:
                        //1)create the claims:list of claims
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        //2)get roles: 
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }

                        //3)signin cr.: algorithm,key
                        //key:
                        SecurityKey seckey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:seckey"]));

                        SigningCredentials signcr = new SigningCredentials(seckey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken mytoken = new JwtSecurityToken
                       (
                           issuer: config["jwt:validissuer"],
                           audience: config["jwt:validaud"],
                           claims: claims,
                           expires: DateTime.Now.AddHours(1),
                           signingCredentials: signcr

                       );

                        return Ok
                            (
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expiration = mytoken.ValidTo
                            }

                            );




                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }


    }
}
