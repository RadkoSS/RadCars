import { get } from './fetchApiRequester.js'

const makeSelectList = document.getElementById('carMakes');
const modelSelectList = document.getElementById('carModels');

makeSelectList.addEventListener('change', loadModelsByMakeId);

async function loadModelsByMakeId() {
    const models = await get(`/api/car/models/${this.value}`);

    console.log(models);
}