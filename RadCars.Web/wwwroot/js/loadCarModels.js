import { get } from './fetchApiRequester.js';

const makeSelectList = document.getElementById('carMakes');
const modelSelectList = document.getElementById('carModels');

makeSelectList.addEventListener('change', loadModelsByMakeId);

async function loadModelsByMakeId() {
    const models = await get(`/api/car/models/${this.value}`);
    modelSelectList.innerHTML = '';

    if (models.length !== 0) {
        modelSelectList.disabled = false;

        models.forEach(m => {
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