using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class MyUsers : IdentityUser
    {
        [StringLength(50)]
        public string FullName { get; set; }
        public List<AIPrompt> AIPrompts { get; set; }
        public List<MyPrompt> MyPrompts { get; set; }

    }
}
