using Microsoft.AspNetCore.Identity;
using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services.Abstracts
{
    public interface IUserService : IGenericService<MyUsers>
    {
    }
}
