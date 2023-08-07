namespace RadCars.Services.Messaging.Templates;

using System.Security.Cryptography;

public static class EmailTemplates
{
    public static class ListingTemplates
    {
        public const string PriceChangeEmailTemplate = @"
            <!DOCTYPE html>
            <html lang='bg'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Промяна в цената на обява в Любими</title>
                </head>
                <body>
                        <div style='max-width: 600px; margin: 0 auto; font-family: Arial, sans-serif;'>
                            <h2 style='color: #2c3e50;'>Промяна в цената на обява</h2>
                            <p>Здравейте, {10}</p>
                            <p>Имаме новина за Вас! Цената на една от обявите, които сте добавили към любимите си, е променена.</p>
                        <div style='border: 1px solid #eaeaea; padding: 15px; margin: 15px 0;'>
                        <img src='{1}' alt='Thumbnail' style='width: 100%; max-height: 250px; object-fit: cover;'>
                        <h3>{0}</h3>
                        <ul>
                             <li>Марка: {2}</li>
                             <li>Модел: {3}</li>
                             <li>Година на производство: {4} г.</li>
                             <li>Населено място: {5}</li>
                             <li>Двигател: {6}</li>
                             <li>Пробег: {7} км.</li>
                             <li>Стара цена: <span style='text-decoration: line-through; color: rgb(0, 0, 0);'>{8}</span></li>
                             <li style='font-weight: bold; color: #e74c3c;'>Нова цена: {9}</li>
                        </ul>
                        </div>
                            <p>Поздрави,<br>Екипът на RadCars</p>
                        </div>
                </body>
            </html>";
    }

    public static class AuctionTemplates
    {
        public const string AuctionEndedEmailToCreator = @"
            <!DOCTYPE html>
            <html lang='bg'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Приключил търг</title>
            </head>
            <body>
                <div style='max-width: 600px; margin: 0 auto; font-family: Arial, sans-serif;'>
                    <h2 style='color: #2c3e50;'>Търгът приключи</h2>
                    <p>Здравейте, {0}</p>
                    <p>Вашият търг зa {1} {2} приключи.</p>
                    <div style='border: 1px solid #eaeaea; padding: 15px; margin: 15px 0;'>
                        <img src='{3}' alt='Thumbnail' style='width: 100%; max-height: 250px; object-fit: cover;'>
                        <h3>{4}</h3>
                        <p>{5}</p>
                    </div>
                    <p>Поздрави,<br>Екипът на RadCars</p>
                </div>
            </body>
            </html>"
        ;

        public const string WinnerAnnounce = @"{0} спечели търга с наддаване от {1} в/ъв {2}. Можете да се свържете с победителя по имейл: {3} или телефон: {4}. При отказ от сделка с купувач или проблеми със системата, винаги можете да се обърнете към администрацията на RadCars. Пожелаваме Ви приятен ден и успешна сделка!";

        public const string NoWinnerAnnounce = "За съжаление Вашият търг приключи без наддавания и не можахме да излъчим победител! Можете да го пуснете отново през Моите -> Търгове -> Приключили или да се свържете с администрацията на RadCars. Ние с удовоствие ще Ви помогнем с всякакви въпроси и проблеми. Пожелаваме Ви приятен ден!";
    }
}