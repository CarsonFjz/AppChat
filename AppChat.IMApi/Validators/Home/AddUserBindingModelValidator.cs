using AppChat.IMApi.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppChat.IMApi.Validators.Home
{
    public class AddUserBindingModelValidator: BaseFoundationValidator<AddUserBindingModel>
    {
        public AddUserBindingModelValidator()
        {
            Custom(x => 
            {
                if (string.IsNullOrWhiteSpace(x.username) == true)
                {
                    return new ValidationFailure("username", "username Is Empty");
                }
                if (string.IsNullOrWhiteSpace(x.password) == true)
                {
                    return new ValidationFailure("password", "password Is Empty");
                }
                return null;
            });

            RuleFor(x => x.username)
                .Length(3, 9)
                .WithMessage("Characters min 3 and max 9");

            RuleFor(x => x.password)
                .Length(3, 6)
                .WithMessage("Characters min 3 and max 6");
        }
    }
}