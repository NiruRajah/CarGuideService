import axios from 'axios';





export const apiLogin = (request_data) => {
    return  axios.post(`https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/user/login?username=${request_data.username}&password=${request_data.password}`);
}


export const apiSignUp = async (request_data) => {
    return axios.post("https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/user", request_data);
}

export const fetchProfile = (username) => {
    return axios.get(`https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/user/${username}`);
}

export const getAllUsers = async () => {
    const users = await axios.get('https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/user/all')
    // console.log(users.data)
    return users;

}


// export const generate_Token = () => {

// }