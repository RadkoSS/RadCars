namespace RadCars.Common;

using ByteSizeLib;

public static class GeneralApplicationConstants
{
    public const int ReleaseYear = 2023;

    public const long ImageMaximumSizeInBytes = ByteSize.BytesInMegaByte * 10;

    public const int DefaultPage = 1;
    public const int EntitiesPerPage = 6;

    public const string UserNamePattern = @"^[a-z0-9]([._]?[a-z0-9]){2,49}$";
    public const string AdminRoleName = "Administrator";
    public const string DevelopmentAdminEmail = "radi7275@gmail.com";

    public const string SendGridSenderEmail = "radcars.bg@gmail.com";
    public const string SendGridSenderName = "RadCars";
}