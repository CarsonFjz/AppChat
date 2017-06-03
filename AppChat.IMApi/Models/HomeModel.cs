using AppChat.IMApi.Validators.Home;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppChat.IMApi.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    [Validator(typeof(AddUserBindingModelValidator))]
    public class AddUserBindingModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}