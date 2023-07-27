import { post } from './webApiRequester.js';

document.addEventListener('DOMContentLoaded', async () => {
    const favoriteButton = document.getElementById('favoriteButton');
    const heartIcon = document.getElementById('heartIcon');
    const favoritesCounter = document.getElementById('favoritesCounter');

    if (!favoriteButton) {
        return;
    }

    const userId = document.getElementById('userId').value;
    const auctionId = document.getElementById('auctionId').value;

    const checkIfListingIsInUserFavoritesUrl = '/api/auction/favorites/exists';
    const checkListingsFavoriteCountUrl = '/api/auction/favorites/count';
    const addFavoriteListingUrl = '/api/auction/favorites/add';
    const unFavoriteListingUrl = '/api/auction/favorites/remove';

    const updateButtonAndReturnListingIsInFavorites = async () => {
        const result = await post(checkIfListingIsInUserFavoritesUrl,
            {
                userId,
                auctionId
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

            const swalResult = await Swal.fire({
                title: 'Сигурни ли сте, че искате да премахнете търга от любими?',
                text: "Няма да бъдете известени за приключването му!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                cancelButtonText: 'Отказ',
                confirmButtonText: 'Да.'
            });

            if (swalResult.isConfirmed) {
                await unFavoriteListing(url);

                toastr.warning('Търгът беше премахнат от любими.');
            }
        } else {
            await favoriteListing(url);

            toastr.options.onclick = function () {
                window.location.href = favoriteButton.href;
            }

            toastr.success('Търгът беше добавен в любими. Натиснете тук, за да видите всичките си любими търгове.');
        }

        await updateFavoritesCounter();
    });

    async function favoriteListing(callbackUrl) {
        await post(callbackUrl,
            {
                userId,
                auctionId
            });

        await updateButtonAndReturnListingIsInFavorites();
    }

    async function unFavoriteListing(callbackUrl) {
        await post(callbackUrl,
            {
                userId,
                auctionId
            });

        await updateButtonAndReturnListingIsInFavorites();
    }

    async function updateFavoritesCounter() {
        const count = await post(checkListingsFavoriteCountUrl, { auctionId });

        if (count === 1) {
            favoritesCounter.innerText = `Добавен ${count} път в "Любими"`;
        } else {
            favoritesCounter.innerText = `Добавен ${count} пъти в "Любими"`;
        }
    }
});