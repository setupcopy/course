using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseDtos;
using FluentValidation;

namespace CourseUtility.DataValidator
{
    public class LoginRequestValidator:AbstractValidator<LoginDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty().Length(6, 32).WithMessage("length of email is wrong");
            RuleFor(u => u.Email).EmailAddress().WithMessage("format of email is wrong");
            RuleFor(u => u.Password).NotEmpty().Length(6, 32).WithMessage("length of password is wrong");
        }
    }
}
