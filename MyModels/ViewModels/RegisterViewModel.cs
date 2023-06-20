using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="İsim soyisim zorunludur")]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email Adresi zorunludur")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage ="Şifre Zorunludur")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Şifre en az 6, en fazla 20 karakter olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Girmiş olduğunuz şifreler birbiri ile uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
