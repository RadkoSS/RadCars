let currentImageIndex = 0;
const images = Array.from(document.querySelectorAll('[data-bs-img-url]')).map(a => a.getAttribute('data-bs-img-url'));

document.querySelectorAll('[data-bs-toggle="modal"]').forEach((item, index) => {
    item.addEventListener('click', event => {
        // Button that triggered the modal
        const imageUrl = event.currentTarget.getAttribute('data-bs-img-url');
        // Update the modal's content.
        const modalImage = document.querySelector('#modalImage');
        modalImage.src = imageUrl;
        currentImageIndex = index; // Update the current image index
    });
});

document.querySelector('#prevBtn').addEventListener('click', () => {
    currentImageIndex = (currentImageIndex - 1 + images.length) % images.length;
    document.querySelector('#modalImage').src = images[currentImageIndex];
});

document.querySelector('#nextBtn').addEventListener('click', () => {
    currentImageIndex = (currentImageIndex + 1) % images.length;
    document.querySelector('#modalImage').src = images[currentImageIndex];
});