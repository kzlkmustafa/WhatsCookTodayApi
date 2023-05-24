using Microsoft.AspNetCore.Identity;

namespace WhatsCookTodayApi.MyModels
{
    public class MyUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AIPrompt> AIPrompts { get; set; }
        public List<MyPrompt> MyPrompts { get; set; }

    }
}
