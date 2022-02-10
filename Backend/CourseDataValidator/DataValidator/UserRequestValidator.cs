using CourseUtility.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseUtility.DataValidator
{
    public class UserRequestValidator : AbstractValidator<UserForValdation>
    {
        public UserRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty().Length(6, 32).WithMessage("length of email is wrong");
            RuleFor(u => u.Email).EmailAddress().WithMessage("format of email is wrong");
            RuleFor(u => u.Password).NotEmpty().Length(6, 32).WithMessage("length of password is wrong");
            RuleFor(u => u.PasswordConfirmed).Equal(u => u.Password).NotEmpty().WithMessage("PasswordConfirmed does not match Password");
        }
    }
}
