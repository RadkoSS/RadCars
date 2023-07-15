namespace RadCars.Common;

public static class ExceptionsAndNotificationsMessages
{
    public const string ListingDoesNotExistError = "Обявата не същестува.";

    public const string ImageDoesNotExistError = "Изображението не същестува.";
    public const string ImageDeleteUnsuccessful = "Неуспешно изтриване на изображение!";
    public const string ImageUploadUnsuccessful = "Качването на изображението не беше успешно! Опитайте отново!";
    public const string ImagesUploadUnsuccessful = "Качването на изображенията не беше успешно! Опитайте отново!";

    public const string InvalidImageForThumbnailProvided = "Изображението, което избрахте за обложка на обявата не съществува!";

    public const string InvalidDataProvidedError = "Данните са невалидни!";
    public const string AnErrorOccurred = "Неуспешно действие. Опитайте отново по-късно.";

    public const string ListingCreatedSuccessfully = "Обявата беше създадена успешно!";
    public const string ListingRemovedFromFavorites = "Обявата беше премахната от любими.";

    public const string ListingDeactivated =
        "Обявата беше скрита и деактивирана! Можете да я активирате отново или да я изтриете.";
    public const string ListingReDeactivated =
        "Обявата отново беше активирана и вече е публична.";

    public const string ThumbnailSelectedSuccessfully = "Обложката беше избрана успешно.";

    public const string ImageDeletedSuccessfully = "Снимката беше изтрита успешно!";

    public const string ListingPermanentlyDeleted = "Обявата беше изтрита перманентно.";

    public const string InvalidDataSubmitted = "Изпращате невалидни данни, качете снимките отново и поправете грешните полета.";
    public const string ErrorCreatingTheListing = "Получи се грешка, качете снимките отново и опитайте пак.";

    public const string ListingIsAlreadyInCurrentUserFavorites = "Обявата вече е била добавена в любимите Ви обяви!";
    public const string ListingIsNotInCurrentUserFavorites = "Обявата вече не присъства в любимите Ви обяви!";

    public const string LogoutSuccessful = "Излязохте успешно от своя акаунт.";
    public const string LoginSuccessful = "Влязохте успешно в своя акаунт.";
    public const string RegistrationSuccessful = "Регистрирахте се успешно!";
}