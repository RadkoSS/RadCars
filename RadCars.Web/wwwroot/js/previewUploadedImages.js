﻿const input = document.getElementById('images');
const removeImagesBtn = document.getElementById('clearImages');
const carouselContainer = document.querySelector('.carousel-inner');
const carouselWrap = document.getElementById('carouselExampleControls');

input.addEventListener('change', () => {
    carouselContainer.innerHTML = '';

    try {
        for (let i = 0; i < input.files.length; i++) {

            if (!input.files[i].type.startsWith('image/')) {
                Swal.fire({
                    icon: 'error',
                    title: 'Грешка',
                    text: 'Разрешено е качването само на снимки!'
                });
                throw new Error();
            }

            const carouselItem = document.createElement('div');
            if (i === 0) {
                carouselItem.className = 'carousel-item active';
            } else {
                carouselItem.className = 'carousel-item';
            }

            carouselItem.style.height = '40vh';

            const img = document.createElement('img');
            img.src = URL.createObjectURL(input.files[i]);
            img.className = 'd-block w-100 h-100';
            img.style.objectFit = 'cover';
            img.alt = 'Снимката не беше заредена...';

            img.onload = () => {
                URL.revokeObjectURL(this.src);
            }

            carouselItem.appendChild(img);
            carouselContainer.appendChild(carouselItem);
        }

        carouselWrap.classList.remove('visually-hidden');
        removeImagesBtn.classList.remove('visually-hidden');
    } catch (e) {
        input.value = '';
        carouselContainer.innerHTML = '';
    }
});

removeImagesBtn.addEventListener('click', () => {
    input.value = '';
    carouselContainer.innerHTML = '';
    carouselWrap.classList.add('visually-hidden');
    removeImagesBtn.classList.add('visually-hidden');
});