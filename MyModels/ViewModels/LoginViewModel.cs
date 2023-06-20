using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Adresi zorunludur")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
