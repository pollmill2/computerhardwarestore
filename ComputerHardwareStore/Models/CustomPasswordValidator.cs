using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ComputerHardwareStore.Models
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public int MinLength { get; set; } // минимальная длина
        public int MaxLength { get; set; } // максимальная длина

        public CustomPasswordValidator(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (String.IsNullOrEmpty(password) || password.Length < MinLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Минимальная длина пароля равна {MinLength}"
                });
            }
            else if (password.Length > MaxLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Максимальная длина пароля равна {MaxLength}"
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}