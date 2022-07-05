using DeliveryUnitManager.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly DeliveryUnitDataContext _context;

        public AuthController(DeliveryUnitDataContext context)
        {
            _context = context;
        }

        [HttpPost("token")]
        public async Task<ActionResult> GetToken()
        {
            // security key
            string securityKey = "this_is_super_long_security_key_for_token_validation_project_2018_09_07$smesk.in";

            // symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            // signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // create token
            var token = new JwtSecurityToken(
                issuer: "Anonymous",
                audience: "Guest",
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signingCredentials
            );

            // return token
            return await Task.Run(() => Ok(new JwtSecurityTokenHandler().WriteToken(token)));
        }

        [HttpPost("bodyPara")]
        public ActionResult GetBody([FromBody] string content)
        {
            return Ok(content);
        }

        [HttpPost("request")]
        public async Task<ActionResult> GetRequest(Models.UserLogin.User user)
        {
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                var getUser = _context.Users.SingleOrDefault(x => x.Username == user.UserName && x.Password == user.Password);
                if (user == null)
                    return await Task.Run(() => BadRequest("No user registered!"));
                else
                {
                    // security key
                    string securityKey = "this_is_project_security_key_for_login_but_i_dont_think_anymore";

                    // symmetric security key
                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                    // signing credentials
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                    // create token
                    var token = new JwtSecurityToken(
                        issuer: user.UserName,
                        audience: "login successfully",
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials
                    );

                    // return token
                    return await Task.Run(() => Ok(new JwtSecurityTokenHandler().WriteToken(token)));
                }
            }
            else
            {
                return await Task.Run(() => BadRequest("username or password is null"));
            }
        }
        [HttpPost("response")]
        public async Task<ActionResult> GetResponse([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return await Task.Run(() => BadRequest("token is empty"));
            }
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // security key
                string securityKey = "this_is_project_security_key_for_login_but_i_dont_think_anymore";
                var key = Encoding.ASCII.GetBytes(securityKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Issuer);

                var user = _context.Users.Find(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
            return await Task.Run(() => Ok("Token is success"));
        }

    }
}
