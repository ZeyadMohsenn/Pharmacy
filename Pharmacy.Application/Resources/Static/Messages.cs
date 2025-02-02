using Pharmacy.Application.Helper;

namespace Pharmacy.Application.Resources.Static;

public class Messages
{
    public static string SomethingWentWrong => CultureHelper.GetResource(nameof(Messages), nameof(SomethingWentWrong));
    public static string IncorrectPassword => CultureHelper.GetResource(nameof(Messages), nameof(IncorrectPassword));
    public static string YourAccountIsDeactivated => CultureHelper.GetResource(nameof(Messages), nameof(YourAccountIsDeactivated));
    public static string UserNotFound => CultureHelper.GetResource(nameof(Messages), nameof(UserNotFound));
    public static string FullNameIsRequired => CultureHelper.GetResource(nameof(Messages), nameof(FullNameIsRequired));
    public static string PasswordIsRequired => CultureHelper.GetResource(nameof(Messages), nameof(PasswordIsRequired));
    public static string InvalidPassword => CultureHelper.GetResource(nameof(Messages), nameof(InvalidPassword));
    public static string UserNameIsRequired => CultureHelper.GetResource(nameof(Messages), nameof(UserNameIsRequired));
    public static string UserNameIsTooLong => CultureHelper.GetResource(nameof(Messages), nameof(UserNameIsTooLong));
    public static string UserNameAlreadyExists => CultureHelper.GetResource(nameof(Messages), nameof(UserNameAlreadyExists));
    public static string InvalidEmail => CultureHelper.GetResource(nameof(Messages), nameof(InvalidEmail));
    public static string EmailIsTooLong => CultureHelper.GetResource(nameof(Messages), nameof(EmailIsTooLong));
    public static string EmailAlreadyExists => CultureHelper.GetResource(nameof(Messages), nameof(EmailAlreadyExists));
    public static string PhoneNumberIsTooLong => CultureHelper.GetResource(nameof(Messages), nameof(PhoneNumberIsTooLong));
    public static string RoleIdIsRequired => CultureHelper.GetResource(nameof(Messages), nameof(RoleIdIsRequired));
    public static string InvalidDateOfBirth => CultureHelper.GetResource(nameof(Messages), nameof(InvalidDateOfBirth));
    public static string RoleNotFound => CultureHelper.GetResource(nameof(Messages), nameof(RoleNotFound));
    public static string AccountCreatedSuccessfully => CultureHelper.GetResource(nameof(Messages), nameof(AccountCreatedSuccessfully));
}
