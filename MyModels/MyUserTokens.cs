using Microsoft.AspNetCore.Identity;

namespace WhatsCookTodayApi.MyModels
{
    public class MyUserTokens: IdentityUserToken<string>
    {
        public DateTime ExpireDate { get; set; }
    }
}
