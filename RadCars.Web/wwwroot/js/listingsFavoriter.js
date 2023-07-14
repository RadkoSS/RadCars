﻿import { post } from './webApiRequester.js';

document.addEventListener('DOMContentLoaded', async () => {
    const favoriteButton = document.getElementById('favoriteButton');
    const heartIcon = document.getElementById('heartIcon');
    const favoritesCounter = document.getElementById('favoritesCounter');

    if (!favoriteButton) {
        return;
    }
    
    const userId = document.getElementById('userId').value;
    const listingId = document.getElementById('listingId').value;
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const checkIfListingIsInUserFavoritesUrl = '/api/listing/favorites/exists';
    const checkListingsFavoriteCountUrl = '/api/listing/favorites/count';
    const addFavoriteListingUrl = '/api/listing/favorites/add';
    const unFavoriteListingUrl = '/api/listing/favorites/remove';
    
    const updateButtonAndReturnListingIsInFavorites = async () => {
        const result = await post(checkIfListingIsInUserFavoritesUrl,
            {
                userId,
                listingId
            }, token);

        if (result === true) {
            favoriteButton.classList.remove('btn-success');
            favoriteButton.classList.add('btn-danger');
            heartIcon.classList.remove('far');
            heartIcon.classList.add('fas');
        } else {
            favoriteButton.classList.remove('btn-danger');
            favoriteButton.classList.add('btn-success');
            heartIcon.classList.remove('fas');
            heartIcon.classList.add('far');
        }

        return result;
    }

    await updateButtonAndReturnListingIsInFavorites();

    favoriteButton.addEventListener('click', async (e) => {
        e.preventDefault();

        let url = addFavoriteListingUrl;

        if (await updateButtonAndReturnListingIsInFavorites() === true) {
            url = unFavoriteListingUrl;
            await unFavoriteListing(url);
        } else {
            await favoriteListing(url);
        }

        await updateFavoritesCounter();
    });

    async function favoriteListing(callbackUrl) {
        await post(callbackUrl,
            {
                userId,
                listingId
            }, token);

        await updateButtonAndReturnListingIsInFavorites();
    }

    async function unFavoriteListing(callbackUrl) {
        await post(callbackUrl,
            {
                userId,
                listingId
            }, token);

        await updateButtonAndReturnListingIsInFavorites();
    }

    async function updateFavoritesCounter() {
        const count = await post(checkListingsFavoriteCountUrl, { listingId }, token);

        if (count === 1) {
            favoritesCounter.innerText = `Добавена ${count} път в "Любими"`;
        } else {
            favoritesCounter.innerText = `Добавена ${count} пъти в "Любими"`;
        }
    }
});