import axios from 'axios';

export const getCars = async () => {
    const cars = await axios.get('https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehicle/all');
    return cars;
}

export const apiAddCar = async (request_data) => {
     await axios.post('https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehicle', {request_data})
      .then(res => {
          console.log(res)
          console.log(res.data)
      })
}



/**
 * get all vehiclereview by username
 */
export const apiViewsVicule = async (username) => { 
    return await axios.get(`https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehiclereview/user?username=${username}`)
}

/**
 * post vehiclereview by username
 */

export const apiPostViewsVehicule = async (request_data) => {
    return await axios.post("https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehiclereview", request_data);
}

/**
 * post the best vehicle
 */
export const apiBestVehicle = async (request_data) => {
    return await axios.post("https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehicle/criterias", request_data);
}

/**
 * get maintenance request
 */
export const apiMaintenance = async (request_data) => {
    return await axios.get(`https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/dataapi/maintenance?year=${request_data.year}&make=${request_data.make}&model=${request_data.model}`)
}