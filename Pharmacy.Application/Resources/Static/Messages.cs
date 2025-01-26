using Pharmacy.Application.Helper;

namespace Pharmacy.Application.Resources.Static;

public class Messages
{
    public static string SomethingWentWrong => CultureHelper.GetResource(nameof(Messages), nameof(SomethingWentWrong));
    public static string IncorrectPassword => CultureHelper.GetResource(nameof(Messages), nameof(IncorrectPassword));
    public static string YourAccountIsDeactivated => CultureHelper.GetResource(nameof(Messages), nameof(YourAccountIsDeactivated));
    public static string UserNotFound => CultureHelper.GetResource(nameof(Messages), nameof(UserNotFound));

}
