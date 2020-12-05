import axios from 'axios';

// eslint-disable-next-line import/no-anonymous-default-export
export default (token) => {
    if (token) {
         axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
        delete axios.defaults.headers.common['Authorization'];
    }
}