const domain = 'https://localhost:7180';

async function requester(method, endPoints, data, token) {
    const options = {
        method,
        headers: {}
    }

    options.headers['Content-Type'] = 'application/json';

    if (token) {
        options.headers['X-CSRF-VERIFICATION-TOKEN'] = token;
    }

    if (data) {
        options.body = JSON.stringify(data);
    }

    try {
        const request = await fetch(domain + endPoints, options);

        if (!request.ok) {
            const error = await request.json();
            throw new Error(error.message);
        }

        if (request.status === 204) {
            return request;
        } else {
            const contentType = request.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return await request.json();
            } else {
                return await request.text();
            }
        }

    } catch (error) {
        throw error;
    }
}

const get = requester.bind(null, 'get');
const post = requester.bind(null, 'post');
const put = requester.bind(null, 'put');
const patch = requester.bind(null, 'patch');
const deleteReq = requester.bind(null, 'delete');

export {
    get,
    post,
    put,
    patch,
    deleteReq as delete
}