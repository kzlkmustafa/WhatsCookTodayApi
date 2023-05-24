using Microsoft.AspNetCore.Identity;
using WhatsCookTodayApi.Data;
using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureIdentity(this IServiceCollection serviceCollection)
        {
            var builder = serviceCollection.AddIdentity<MyUsers, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        }
    }
}
