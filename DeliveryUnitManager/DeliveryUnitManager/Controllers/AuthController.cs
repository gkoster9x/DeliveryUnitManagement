using DeliveryUnitManager.Attributes;
using DeliveryUnitManager.Constant;
using DeliveryUnitManager.Models;
using DeliveryUnitManager.Models.UserLogin;
using DeliveryUnitManager.Reponsitory.Models.Users;
using DeliveryUnitManager.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        #region test
        [HttpPost("token")]
        [CustomAuthorize(Role ="admins")]
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
        public ActionResult GetBody([FromHeader] string content)
        {

            return Ok(new TestModel()
            {
                Name=content,
                Number = content.Length,
                Date = DateTime.Now
            });
        }

        #endregion
        [HttpPost]
        public TokenModel GetRequest(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
                {
                    var getUser = _context.Users.SingleOrDefault(x => x.Username == user.UserName && x.Password == user.Password);
                    if (getUser == null)
                        return new TokenModel(false, "No user registered!");
                    else
                    {
                        // symmetric security key
                        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.jwtKey));

                        // signing credentials
                        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                        IEnumerable<Claim> claims = new Claim[] {
                    new Claim("Id", getUser.Id.ToString()),
                    new Claim("username", getUser.Username),
                    new Claim("Fullname", getUser.Fullname),


                };
                        // create token
                        var token = new JwtSecurityToken(
                            issuer: JwtConstant.jwtIssuer,
                            audience: JwtConstant.jwtAudience,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials,
                            claims: claims

                        );

                        // return token
                        return new TokenModel(true, "Successfully", new JwtSecurityTokenHandler().WriteToken(token));
                    }
                }
                else
                {
                    return new TokenModel(false, "username or password is null"
                        );
                }
            }
            catch (Exception ex)
            {
                return new TokenModel(false, ex.Message);
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

                var key = Encoding.ASCII.GetBytes(JwtConstant.jwtKey);
                // symmetric security key
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.jwtKey));

                // signing credentials
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtConstant.jwtIssuer,
                    ValidAudience = JwtConstant.jwtAudience,
                    IssuerSigningKey = symmetricSecurityKey,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return await Task.Run(() => Ok(jwtToken));
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
                return await Task.Run(() => BadRequest());
            }
        }


        [HttpPost("Register")]
        public async Task<TokenModel> Register(RegisterUser user)
        {
            try
            {

                if (_context.Users == null)
                {
                    return new TokenModel(false, "User is null");
                }
                if (_context.Users.Where(u => u.Username == user.Username).Any())
                {
                    return new TokenModel(false, "username is exits");

                }
                if (_context.Users.Where(u => u.Email == user.Email).Any())
                    return new TokenModel(false, "Email is exits");
                var newUser = new Users()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Fullname = user.FullName,
                    UserId = DateTime.Now.Ticks,
                    Code = user.Username.ToUpper(),
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Gender =   user.Gender,
                    PositionId = user.PositionID==0 ? 12 : user.PositionID,
                    DoB = user.DoB,
                    IsActive= true,
                    Created= DateTime.Now,
                    CreateBy="testApi"
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return new TokenModel(true, "Register successfully");
            }
            catch (Exception ex)
            {
                return new TokenModel(false, ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task<TokenModel> Login(User user)
        {
            var TokenModel = GetRequest(user);
            if (TokenModel.result)
                TokenModel.Message= "Login successfully";
            return await Task.Run(() => TokenModel);


        }
    }
}
