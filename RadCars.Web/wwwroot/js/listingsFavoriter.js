import { post } from './webApiRequester.js';

document.addEventListener('DOMContentLoaded', async () => {
    const favoriteButton = document.getElementById('favoriteButton');
    const heartIcon = document.getElementById('heartIcon');
    const favoritesCounter = document.getElementById('favoritesCounter');

    if (!favoriteButton) {
        return;
    }

    const userId = document.getElementById('userId').value;
    const listingId = document.getElementById('listingId').value;

    const checkIfListingIsInUserFavoritesUrl = '/api/listing/favorites/exists';
    const checkListingsFavoriteCountUrl = '/api/listing/favorites/count';
    const addFavoriteListingUrl = '/api/listing/favorites/add';
    const unFavoriteListingUrl = '/api/listing/favorites/remove';
    
    const updateButtonAndReturnListingIsInFavorites = async () => {
        const result = await post(checkIfListingIsInUserFavoritesUrl,
            {
                userId,
                listingId
            });

        if (result === true) {
            favoriteButton.classList.remove('btn-success');
            favoriteButton.classList.add('btn-danger');
            heartIcon.classList.remove('far');
            heartIcon.classList.add('fas');
            heartIcon.innerText = ' Премахни от Любими';
        } else {
            favoriteButton.classList.remove('btn-danger');
            favoriteButton.classList.add('btn-success');
            heartIcon.classList.remove('fas');
            heartIcon.classList.add('far');
            heartIcon.innerText = ' Добави в Любими';
        }

        return result;
    }

    await updateButtonAndReturnListingIsInFavorites();

    favoriteButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let url = addFavoriteListingUrl;

        if (await updateButtonAndReturnListingIsInFavorites() === true) {
            url = unFavoriteListingUrl;

            const swalResult = await  Swal.fire({
                title: 'Сигурни ли сте, че искате да премахнете обявата от любими?',
                text: "Няма да бъдете известявани за промяна в цената!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                cancelButtonText: 'Отказ',
                confirmButtonText: 'Да.'
            });

            if (swalResult.isConfirmed) {
                await unFavoriteListing(url);

                toastr.warning('Обявата бе премахната от любимите ви обяви.');
            }
        } else {
            await favoriteListing(url);

            toastr.options.onclick = function () {
                window.location.href = favoriteButton.href;
            }

            toastr.success('Обявата бе добавена в любими. Натиснете тук, за да видите всички любими.');
        }

        await updateFavoritesCounter();
    });

    async function favoriteListing(callbackUrl) {
        await post(callbackUrl,
            {
                userId,
                listingId
            });

        await updateButtonAndReturnListingIsInFavorites();
    }

    async function unFavoriteListing(callbackUrl) {
        await post(callbackUrl,
            {
                userId,
                listingId
            });

        await updateButtonAndReturnListingIsInFavorites();
    }

    async function updateFavoritesCounter() {
        const count = await post(checkListingsFavoriteCountUrl, { listingId });

        if (count === 1) {
            favoritesCounter.innerText = `Добавена ${count} път в "Любими"`;
        } else {
            favoritesCounter.innerText = `Добавена ${count} пъти в "Любими"`;
        }
    }
});