namespace RadCars.Common;

using ByteSizeLib;

public static class GeneralApplicationConstants
{
    public const int ReleaseYear = 2023;

    public const long ImageMaximumSizeInBytes = ByteSize.BytesInMegaByte * 10;

    public const int DefaultPage = 1;
    public const int EntitiesPerPage = 6;

    public const string AdminRoleName = "Administrator";
    public const string DevelopmentAdminEmail = "radko121@mail.bg";
}