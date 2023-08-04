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
    public const string ListingReactivated =
        "Обявата отново беше активирана и вече е публична.";

    public const string ListingWasUpdatedSuccessfully = "Обявата беше редактирана. Изберете снимка за обложка.";
    public const string UploadedImagesAreInvalid = "Можете да качвате само снимки с размер до 10 MB!";

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

    public const string UnsuccessfulRegistration =
        "Получи се грешка при опита за регистрация, моля опитайте отново по-късно.";

    public const string AuctionDeactivated = "Търгът беше деактивиран.";
    public const string CannotDeactivateAuction = "Не можете да деактивирате търг, който вече е започнал или има наддавания!";
    public const string AuctionReactivated = "Търгът беше активиран отново. Моля, опреснете данните за неговото провеждане!";
    public const string AuctionIsAlreadyInCurrentUserFavorites = "Търгът вече е бил добавен в любими!";
    public const string AuctionRemovedFromFavorites = "Търгът беше премахнат от любими.";
    public const string CannotEditActiveAuctionWithBids = "Нямате правомощията, за да редактирате търгове, които имат наддавания!";
    public const string AuctionDoesNotExistError = "Търгът не същестува.";
    public const string AuctionHasAlreadyStarted = "Нямате правомощията да променяте търга след неговото начало или край.";
    public const string ErrorCreatingTheAuction =
        "Неуспешно създаване на търг. Моля, опитайте отново по-късно или се свържете с администратор.";
    public const string ErrorEditingTheAuction =
        "Неуспешно редактиране на търг. Моля, опитайте отново по-късно или се свържете с администратор.";
    public const string AuctionWasUpdatedSuccessfully = "Търгът беше обновен успешно. Изберете снимка за обложката.";
    public const string AuctionCreateSuccessfully = "Търгът беше създаден успешно.";
}