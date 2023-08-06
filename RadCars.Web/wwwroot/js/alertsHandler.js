const allActionForms = document.querySelectorAll(".swal-form");

allActionForms.forEach((actionForm) => {
    actionForm.addEventListener("submit", promptWithSweetAlert);
});

async function promptWithSweetAlert(event) {
    event.preventDefault();

    const swalResult = await Swal.fire({
        title: "Сигурни ли сте, че искате да извършите действието?",
        text: "Натиснете Отказ, ако искате да го прекратите.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3c854f',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Отказ',
        confirmButtonText: 'Да, сигурен съм'
    });

    if (swalResult.isConfirmed) {
        event.target.submit();
    }
}