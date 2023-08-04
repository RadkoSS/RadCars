namespace RadCars.Common;

using ByteSizeLib;

public static class GeneralApplicationConstants
{
    public const int ReleaseYear = 2023;

    public const long ImageMaximumSizeInBytes = ByteSize.BytesInMegaByte * 10;

    public const int DefaultPage = 1;
    public const int EntitiesPerPage = 6;

    public const int CountryIdOfBulgaria = 1;

    public const string UserNamePattern = @"^[a-z0-9]([._]?[a-z0-9]){2,49}$";

    public const string AdminRoleName = "Administrator";
    public const string AdminRoleNormalizedName = "ADMINISTRATOR";

    public const string DevelopmentAdminEmail = "admin77@gmail.com";
    public const string DevelopmentAdminNormalizedEmail = "ADMIN77@GMAIL.COM";
    public const string DevelopmentAdminUserName = "tegav_admin";
    public const string DevelopmentAdminNormalizedUserName = "TEGAV_ADMIN";

    public const string DevelopmentUserEmail = "user77@gmail.com";
    public const string DevelopmentUserNormalizedEmail = "USER77@GMAIL.COM";
    public const string DevelopmentUserUserName = "tegav_user";
    public const string DevelopmentUserNormalizedUserName = "TEGAV_USER";

    public const string SendGridSenderEmail = "radcars.bg@gmail.com";
    public const string SendGridSenderName = "RadCars";
}