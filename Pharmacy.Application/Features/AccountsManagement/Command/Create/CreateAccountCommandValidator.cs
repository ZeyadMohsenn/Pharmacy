using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Create;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator(
        UserManager<ApplicationUser>  userManager
    )
    {
        RuleFor(x => x.FullName)
          .NotEmpty().WithMessage(Messages.FullNameIsRequired);

        RuleFor(x => x.Password)
          .Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage(Messages.PasswordIsRequired)
          .MinimumLength(8).WithMessage(Messages.InvalidPassword)
          .Matches(@"[A-Z]").WithMessage(Messages.InvalidPassword)
          .Matches(@"\d").WithMessage(Messages.InvalidPassword)
          .Matches(@"[^\w\d\s:]").WithMessage(Messages.InvalidPassword);

        RuleFor(p => p.UserName)
          .NotEmpty().WithMessage(Messages.UserNameIsRequired)
          .MaximumLength(20).WithMessage(Messages.UserNameIsTooLong)
          .MustAsync(async (userName, cancellation) =>
          {
              var user = await userManager.FindByNameAsync(userName);
              return user == null;
          }).WithMessage(Messages.UserNameAlreadyExists);

        RuleFor(p => p.Email)
          .EmailAddress().WithMessage(Messages.InvalidEmail)
          .MaximumLength(25).WithMessage(Messages.EmailIsTooLong)
          .MustAsync(async (email, cancellation) =>
          {
              return await userManager.FindByEmailAsync(email) == null;
          }).WithMessage(Messages.EmailAlreadyExists);

        RuleFor(p => p.PhoneNumber)
          .MaximumLength(20).WithMessage(Messages.PhoneNumberIsTooLong);

        RuleFor(p => p.RoleId)
          .NotEmpty().WithMessage(Messages.RoleIdIsRequired);

        RuleFor(p => p.DateOfBirth)
          .LessThan(DateTime.Now).WithMessage(Messages.InvalidDateOfBirth);

    }
}
