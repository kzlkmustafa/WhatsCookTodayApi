using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsCookTodayApi.Data;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.MyModels.ViewModels;
using WhatsCookTodayApi.Services;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;
        private readonly SignInManager<MyUsers> _signInManager;
        private readonly UserManager<MyUsers> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(DatabaseContext context, IConfiguration config, SignInManager<MyUsers> signInManager, UserManager<MyUsers> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            ResponseViewModel responseViewModel= new ResponseViewModel();

            try
            {
                if(!ModelState.IsValid)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Bilgileriniz eksik, Lütfen tüm alanları doldurunuz.";

                    return BadRequest(responseViewModel);
                }

                MyUsers existUser = await _userManager.FindByNameAsync(model.EmailAddress);
                if (existUser != null)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Kullanıcı Zaten var.";
                    return BadRequest(responseViewModel);
                }
                MyUsers user = new MyUsers();
                user.FullName = model.FullName;
                user.Email = model.EmailAddress.Trim();
                user.UserName = model.EmailAddress.Trim();

                IdentityResult result = await _userManager.CreateAsync(user, model.Password.Trim());

                if (result.Succeeded)
                {
                    bool roleexist = await _roleManager.RoleExistsAsync(_config["Roles:User"]);
                    
                    if (!roleexist)
                    {
                        IdentityRole role = new IdentityRole(_config["Roles:User"]);
                        role.NormalizedName = _config["Roles:User"];

                        _roleManager.CreateAsync(role).Wait();

                    }

                    _userManager.AddToRoleAsync(user, _config["Roles:User"]).Wait();

                    responseViewModel.IsSuccess = true;
                    responseViewModel.Message = "Kullanıcı başarılı şekilde Oluşturuldu";
                }
                else
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault().Description);

                }
                return Ok(responseViewModel);
            }
            catch(Exception ex)
            {

                responseViewModel.IsSuccess = false;
                responseViewModel.Message = ex.Message;

                return BadRequest(responseViewModel);
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                #region Validate

                if (ModelState.IsValid == false)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";
                    return BadRequest(responseViewModel);
                }

                //Kulllanıcı bulunur.
                MyUsers user = await _userManager.FindByNameAsync(model.Email);

                //Kullanıcı var ise;
                if (user == null)
                {
                    return Unauthorized();
                }

                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user,
                                                                                                                   model.Password,
                                                                                                                   false,
                                                                                                                   false);
                //Kullanıcı adı ve şifre kontrolü
                if (signInResult.Succeeded == false)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Kullanıcı adı veya şifre hatalı.";

                    return Unauthorized(responseViewModel);
                }

                #endregion

                MyUsers applicationUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(_context, _config, applicationUser);
                MyUserTokens userTokens = accessTokenGenerator.GetTokens();

                responseViewModel.IsSuccess = true;
                responseViewModel.Message = "Kullanıcı giriş yaptı.";
                responseViewModel.TokenInfo = new TokenInfo
                {
                    Token = userTokens.Value,
                    ExpireDate = userTokens.ExpireDate
                };

                return Ok(responseViewModel);
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = ex.Message;

                return BadRequest(responseViewModel);
            }
        }
    }
}
