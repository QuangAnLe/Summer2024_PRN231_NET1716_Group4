using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MilkTeaServices.IServices;
using MilkTeaStore.Admin;
using MilkTeaStore.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MilkTeaStore.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _config;

        public AuthController(IUserServices userServices, IConfiguration config)
        {
            _userServices = userServices;
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginVM userLogin)
        {
            try
            {
                if (userLogin.Email == AdminConfig.GetAdminEmail() && userLogin.Password == AdminConfig.GetAdminPassword())
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email, "admin@gmail.com"),
                        new Claim(ClaimTypes.Role, "Admin")

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var _token = new JwtSecurityTokenHandler().WriteToken(token);

                    HttpContext.Response.Cookies.Append("AdminCookie", _token, new CookieOptions
                    {
                        HttpOnly = true, // Chỉ có thể được đọc bằng cách sử dụng HTTP (không sử dụng JavaScript)
                        Secure = true,   // Chỉ sử dụng khi kết nối an toàn (HTTPS)
                        SameSite = SameSiteMode.None, // Hoặc SameSiteMode.Strict, tùy thuộc vào yêu cầu của ứng dụng
                        Expires = DateTime.UtcNow.AddMinutes(1) // Thời gian hết hạn của cookie
                    });

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }

                var checkLogin = _userServices.CheckLogin(userLogin.Email, userLogin.Password);
                if (checkLogin != null)
                {
                    if (checkLogin.Status != true)
                    {
                        return BadRequest("You are not allowed access into system");
                    }

                    var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, checkLogin.UserID.ToString()),
                        new Claim(ClaimTypes.Email, checkLogin.Email),
                        new Claim(ClaimTypes.Role, checkLogin.RoleID.ToString())
                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var _token = new JwtSecurityTokenHandler().WriteToken(token);

                    HttpContext.Response.Cookies.Append("UserCookie", _token, new CookieOptions
                    {
                        HttpOnly = true, // Chỉ có thể được đọc bằng cách sử dụng HTTP (không sử dụng JavaScript)
                        Secure = true,   // Chỉ sử dụng khi kết nối an toàn (HTTPS)
                        SameSite = SameSiteMode.None, // Hoặc SameSiteMode.Strict, tùy thuộc vào yêu cầu của ứng dụng
                        Expires = DateTime.UtcNow.AddMinutes(1) // Thời gian hết hạn của cookie
                    });

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }

                return BadRequest("Incorect User Name or Password Please Try Again");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
