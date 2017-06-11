using AppChat.Model.Request;
using FluentValidation;

namespace AppChat.IMApi.Validators.Home
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.nickname)
                .NotEmpty()
                .NotNull()
                .Length(3, 9);

            RuleFor(x => x.username)
                .NotEmpty()
                .NotNull()
                .Length(3, 9);

            RuleFor(x => x.password)
                .NotEmpty()
                .NotNull()
                .Length(3, 6);
        }
    }
}