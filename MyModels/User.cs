using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace WhatsCookTodayApi.MyModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        public List<AIPrompt> AIPrompts { get; set; }
        public List<MyPrompt> MyPrompts { get; set; }

    }
}
