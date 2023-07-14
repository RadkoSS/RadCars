import { get } from './webApiRequester.js';

const makeSelectList = document.getElementById('carMakes');
const modelSelectList = document.getElementById('carModels');

makeSelectList.addEventListener('change', loadModelsByMakeId);

async function loadModelsByMakeId() {
    const response = await get(`/api/car/models/${this.value}`);
    modelSelectList.innerHTML = '';

    if (response.length !== 0 && response.status !== 204) {
        modelSelectList.disabled = false;

        response.forEach(m => {
            const option = document.createElement('option');
            option.setAttribute('value', m.id);
            option.text = m.name;

            modelSelectList.appendChild(option);
        });
    } else {
        const option = document.createElement('option');

        option.text = 'Невалидна марка автомобили!';

        modelSelectList.appendChild(option);

        modelSelectList.disabled = true;
    }
}