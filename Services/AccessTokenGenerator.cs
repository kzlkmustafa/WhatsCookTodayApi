using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WhatsCookTodayApi.Data;
using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services
{
    public class AccessTokenGenerator
    {
        public DatabaseContext _databaseContext { get; set; }
        public IConfiguration _config { get; set; }
        public MyUsers _myUsers { get; set; }

        public AccessTokenGenerator(DatabaseContext databaseContext, IConfiguration config, MyUsers myUsers)
        {
            _databaseContext = databaseContext;
            _config = config;
            _myUsers = myUsers;
        }

        public MyUserTokens GetTokens()
        {
            MyUserTokens userTokens = null;
            TokenInfo tokenInfo= null;

            if (_databaseContext.MyUserTokens.Count(x => x.UserId == _myUsers.Id) > 0)
            {
                userTokens = _databaseContext.MyUserTokens.FirstOrDefault(x => x.UserId == _myUsers.Id);
                if (userTokens.ExpireDate <= DateTime.Now)
                {
                    tokenInfo = GenerateToken();
                    userTokens.ExpireDate = tokenInfo.ExpireDate;
                    userTokens.Value = tokenInfo.Token;
                    _databaseContext.MyUserTokens.Update(userTokens);

                }
            }
            else
            {
                tokenInfo = GenerateToken();
                userTokens = new MyUserTokens();

                userTokens.UserId = _myUsers.Id;
                userTokens.LoginProvider = "SystemAPI";
                userTokens.Name = _myUsers.FullName;
                userTokens.ExpireDate = tokenInfo.ExpireDate;
                userTokens.Value = tokenInfo.Token;

                _databaseContext.MyUserTokens.Add(userTokens);
            }
            _databaseContext.SaveChanges();
            return userTokens;
        }
        public async Task<bool> DeleteToken()
        {
            bool ret = true;

            try
            {
                if(_databaseContext.MyUserTokens.Count(x => x.UserId == _myUsers.Id)> 0)
                {
                    MyUserTokens userTokens = _databaseContext.MyUserTokens.FirstOrDefault(x => x.UserId == _myUsers.Id);

                    _databaseContext.MyUserTokens.Remove(userTokens);
                }
                await _databaseContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                ret = false;
            }
            return ret;
        } 
        private TokenInfo GenerateToken()
        {
            DateTime expireDate = DateTime.Now.AddSeconds(5000);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Application:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config["Application:Audience"],
                Issuer = _config["Application;Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, _myUsers.Id),
                    new Claim(ClaimTypes.Name, _myUsers.FullName),
                    new Claim(ClaimTypes.Email, _myUsers.Email)
                }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            TokenInfo tokenInfo = new TokenInfo();

            tokenInfo.Token = tokenString;
            tokenInfo.ExpireDate = expireDate;

            return tokenInfo;
        }
    }
}
