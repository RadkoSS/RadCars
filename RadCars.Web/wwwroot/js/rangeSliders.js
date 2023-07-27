const priceRange = document.getElementById('priceRange');
const priceRangeValue = document.getElementById('priceRangeValue');

if (priceRange && priceRangeValue) {
    if (Number(priceRange.value) > 0) {
        priceRangeValue.innerText = `до ${Number(priceRange.value).toLocaleString('eu-BG')} лв.`;
    } else {
        priceRangeValue.innerText = `Без значение`;
    }

    priceRange.addEventListener('input',
        (event) => {
            const number = Number(event.target.value);

            if (number <= 0) {
                priceRangeValue.innerText = `Без значение`;
                return;
            }

            priceRangeValue.innerHTML = `до ${number.toLocaleString('eu-BG')} лв.`;
        });
}

const mileageRange = document.getElementById('mileageRange');
const mileageRangeValue = document.getElementById('mileageRangeValue');

if (Number(mileageRange.value) > 0) {
    mileageRangeValue.innerHTML = `до ${Number(mileageRange.value).toLocaleString('eu-BG')} km`;
} else {
    mileageRangeValue.innerHTML = `Без значение`;
}

mileageRange.addEventListener('input',
    (event) => {
        const number = Number(event.target.value);

        if (number <= 0) {
            mileageRangeValue.innerHTML = `Без значение`;
            return;
        }

        mileageRangeValue.innerHTML = `до ${number.toLocaleString('eu-BG')} km`;
    });