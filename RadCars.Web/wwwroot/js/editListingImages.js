import { post } from './webApiRequester.js';

const form = document.getElementById('createListingForm');
const undoButton = document.getElementById('undo-btn');
const imagesInput = document.getElementById('images');
imagesInput.setAttribute('data-val', 'false');
const listingId = document.getElementById('listingId').value;

//ToDo: implement JWT token send to the web API for security reasons!
//const jwtToken = '';

const uploadedImagesCount = await post('/api/listing/uploadedImages/count', listingId);

document.querySelectorAll('.delete-button').forEach(button => {
    button.addEventListener('click', async (e) => {
        let target = e.target;
        if (target.tagName.toLowerCase() === 'i') {
            target = e.target.parentNode;
        }

        const imageId = target.id.toLowerCase();
        
        const swalResult = await Swal.fire({
            title: 'Сигурни ли сте, че искате да изтриете снимката?',
            text: "След редактирането на обявата снимката ще бъде изтрита!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Да',
            cancelButtonText: 'Отказ'
        });

        if (swalResult.isConfirmed) {
            //Remove image from the grid.
            target.parentElement.remove();

            // Add the image ID to a hidden input
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'DeletedImages';
            input.value = imageId;
            input.classList.add('deleted-img');
            form.appendChild(input);

            const deleteImagesCount = document.querySelectorAll('.deleted-img').length;

            if (uploadedImagesCount <= deleteImagesCount) {
                imagesInput.setAttribute('data-val', 'true');
                imagesInput.required = true;
            }
        }
    });
});

undoButton.addEventListener('click', async (e) => {
    e.preventDefault();

    const swalResult = await Swal.fire({
        title: 'Сигурни ли сте, че искате да върнете промените?',
        text: "Всичките промени ще бъдат премахнати.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Да',
        cancelButtonText: 'Не'
    });

    if (swalResult.isConfirmed) {
        window.location.replace(e.target.href);
    }
});