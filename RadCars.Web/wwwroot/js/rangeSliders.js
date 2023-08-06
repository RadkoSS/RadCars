const priceRange = document.getElementById('priceRange');
const priceRangeValue = document.getElementById('priceRangeValue');

const kilometersNumberFormatter = Intl.NumberFormat("en", {
    style: "unit",
    unit: "kilometer"
});

if (priceRange && priceRangeValue) {
    const numberFormatter = new Intl.NumberFormat("bg-BG", { style: "currency", currency: "BGN" });

    if (Number(priceRange.value) > 0) {
        priceRangeValue.innerText = `до ${numberFormatter.format(Number(priceRange.value))}`;
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

            priceRangeValue.innerHTML = `до ${numberFormatter.format(number)}`;
        });
}

const mileageRange = document.getElementById('mileageRange');
const mileageRangeValue = document.getElementById('mileageRangeValue');

if (Number(mileageRange.value) > 0) {
    mileageRangeValue.innerHTML = `до ${kilometersNumberFormatter.format(Number(mileageRange.value))}`;
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

        mileageRangeValue.innerHTML = `до ${kilometersNumberFormatter.format(number)}`;
    });