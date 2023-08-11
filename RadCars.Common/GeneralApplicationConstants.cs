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

    public const string AdminAreaName = "Admin";
    public const string AdminRoleName = "Administrator";
    public const string AdminRoleNormalizedName = "ADMINISTRATOR";

    public const string DevelopmentAdminEmail = "admin77@gmail.com";
    public const string DevelopmentAdminUserName = "tegav_admin";
    public const string DevelopmentAdminPhoneNumber = "+359896969699";
    public const string DevelopmentAdminPassword = "admin69";

    public const string DevelopmentUserEmail = "user77@gmail.com";
    public const string DevelopmentUserPhoneNumber = "+359896969999";
    public const string DevelopmentUserUserName = "tegav_user";
    public const string DevelopmentUserPassword = "user69";

    public const string SendGridSenderEmail = "radcars.bg@gmail.com";
    public const string SendGridSenderName = "RadCars";

    public const string OnlineUsersCookieName = "IsOnline";
    public const int LastActivityBeforeOfflineMinutes = 4;

    public const string UsersCacheKey = "UsersCache";
    public const int UsersCacheDurationMinutes = 2;

    public const string CitiesCacheKey = "Cities";
    public const string CarMakesCacheKey = "CarMakes";

    public const decimal ListingMinimumPriceDecreaseThreshold = 0.96m; //4% off
}