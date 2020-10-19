using Application.Model.Request;
using FluentValidation;
using System;


namespace Application.Validator
{
    public class UserValidator : AbstractValidator<UserReq>
    {
        [Obsolete]
        public UserValidator()
        {
            RuleFor(x => x.EmailAddress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .EmailAddress().WithMessage("{PropertyName} must be valid email adress");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(FluentValidator.BeValidName).WithMessage("{PropertyName} must be only letters");

            RuleFor(x => x.Surname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(FluentValidator.BeValidName).WithMessage("{PropertyName} must be only letters");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(FluentValidator.BeValidUsername).WithMessage("{PropertyName} must be only letters");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .MinimumLength(6).WithMessage("{PropertyName} must be 6 chars at least")
                .MaximumLength(32).WithMessage("{PropertyName} must be 32 chars at the highest")
                .Must(FluentValidator.BeValidPassword).WithMessage("{PropertyName} include lower case , upper case and number");

        }
    }
}
