﻿namespace RadCars.Common;

public static class EntityValidationConstants
{
    public static class ListingConstants
    {
        public const int TitleMinimumLength = 5;
        public const int TitleMaximumLength = 80;

        public const int DescriptionMinimumLength = 10;
        public const int DescriptionMaximumLength = 1500;

        public const int MileageMinimum = 0;

        public const string PriceMinimum = "1";
        public const string PriceMaximum = "99999999";

        public const int EngineModelMinimumLength = 3;
        public const int EngineModelMaximumLength = 20;

        public const int VinNumberMinimumLength = 11;
        public const int VinNumberMaximumLength = 17;

        public const int YearMinimumValue = 1910;
    }

    public static class AuctionConstants
    {
        public const string PriceMinimum = "1";
        public const string PriceMaximum = "99999999";

        public const int MinimumMinutesToAuctionStart = 5; //ToDo: Fix this! For testing this will be set to 5 minutes. Should be 30 minutes
        public const double MinimumHoursToAuctionEnd = 0.2; //ToDo: Fix this! For testing this will be set to 0.2 hours (12 minutes). Should be 24 hours

        public const int MaximumDaysOfAuctioning = 14;

        public const int JobIdMaximumLength = 38;
    }

    public static class FeatureCategoryConstants
    {
        public const int NameMinimumLength = 3;
        public const int NameMaximumLength = 50;
    }

    public static class FeatureConstants
    {
        public const int NameMinimumLength = 1;
        public const int NameMaximumLength = 50;
    }

    public static class MakeConstants
    {
        public const int NameMinimumLength = 1;
        public const int NameMaximumLength = 50;
    }

    public static class ModelConstants
    {
        public const int NameMinimumLength = 1;
        public const int NameMaximumLength = 70;
    }

    public static class CountryConstants
    {
        public const int NameMinimumLength = 3;
        public const int NameMaximumLength = 56;
    }

    public static class CityConstants
    {
        public const int NameMinimumLength = 1;
        public const int NameMaximumLength = 85;
    }

    public static class CarEngineTypeConstants
    {
        public const int NameMaximumLength = 15;
    }

    public static class ApplicationUser
    {
        public const int UserNameMinimumLength = 3;
        public const int UserNameMaxLength = 50;

        public const int FirstNameMinimumLength = 2;
        public const int FirstNameMaximumLength = 50;

        public const int LastNameMinimumLength = 2;
        public const int LastNameMaximumLength = 50;

        public const int PhoneNumberMinimumLength = 10;
        public const int PhoneNumberMaximumLength = 13;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const int EmailMinimumLength = 5;
        public const int EmailMaximumLength = 320;
    }
}
