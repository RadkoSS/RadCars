namespace RadCars.Common;

public static class EntityValidationConstants
{
    public static class ListingConstants
    {
        public const int TitleMinimumLength = 5;
        public const int TitleMaximumLength = 80;

        public const int DescriptionMinimumLength = 10;
        public const int DescriptionMaximumLength = 1500;

        public const int MileageMinimum = 0;

        public const int EngineTypeMinimum = 1;
        public const int EngineTypeMaximum = 5;

        public const int EngineModelMinimumLength = 2;
        public const int EngineModelMaximumLength = 10;

        public const int VinNumberMinimumLength = 11;
        public const int VinNumberMaximumLength = 17;

        public const int YearMinimumValue = 1910;
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
}