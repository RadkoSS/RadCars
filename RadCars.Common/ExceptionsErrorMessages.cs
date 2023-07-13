namespace RadCars.Common;

public static class ExceptionsErrorMessages
{
    public const string ListingDoesNotExistError = "Обявата не същестува.";

    public const string ImageDoesNotExistError = "Изображението не същестува.";
    public const string ImageDeleteUnsuccessful = "Неуспешно изтриване на изображение!";
    public const string ImageUploadUnsuccessful = "Качването на изображението не беше успешно! Опитайте отново!";
    public const string ImagesUploadUnsuccessful = "Качването на изображенията не беше успешно! Опитайте отново!";

    public const string InvalidImageForThumbnailProvided = "Изображението, което избрахте за обложка на обявата не съществува!";

    public const string InvalidDataProvidedError = "Данните са невалидни!";

    public const string InvalidDataSubmitted = "Изпращате невалидни данни, качете снимките отново и поправете грешните полета.";
    public const string ErrorCreatingTheListing = "Получи се грешка, качете снимките отново и опитайте пак.";

    public const string ListingIsAlreadyInCurrentUserFavorites = "Обявата вече е била добавена в любимите Ви обяви!";
    public const string ListingIsNotInCurrentUserFavorites = "Обявата вече не присъства в любимите Ви обяви!";
}