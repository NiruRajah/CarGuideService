import { AUTH_ATTEMPTING,
         AUTH_SUCCESS,
         AUTH_FAILED,
         SIGNUP_SUCCESS,
         SIGNUP_FAILD,
         USER_LOGOUT,
         PROFILE_FETCHED
} from './types';

import {apiLogin, apiSignUp, fetchProfile} from '../api/user';
import setAuthHeader from '../api/setAuthHeader';

const TOKEN_NAME = '12114.gw2.5jfdhghfsl-sahasjkhjbhdskdslkhjrfhkjdslk'; 

export const signIn = (request_data) => {
    return async (dispatch) => {
        dispatch({type: AUTH_ATTEMPTING});
        try {
            const data = await apiLogin(request_data);
            if (data.data === "Logged In Successfully")
            {
                // console.log(request_data.username)
                const token = TOKEN_NAME;
                setAuthHeader(token);
                dispatch(getUserProfile(request_data.username))
                dispatch(success(token));
            }
            else {
                dispatch(error(data.data))
            }
        } catch(e) {
            console.log(e)
        }
    };
};


export const onLogingSignIn = () => {
  return dispatch => {
    try {
      const token = localStorage.getItem(TOKEN_NAME);
      if (token === null || token === 'undefined') {
        dispatch(error('You need to login'));
      } else {
        
        setAuthHeader(token);
        dispatch(success(token));
      }
    } catch(e) {
      // console.log(e);
    }
  }
}

export const getUserProfile = (username) => {
  return async dispatch => {
    try{
       await fetchProfile(username)
             .then(res => {
                dispatch({type: PROFILE_FETCHED, payload: res.data}) 
             }).catch(err => {console.log(err)});
    } catch(e) {
      console.log(e)
    }
  }
}

export const logUserOut = () => {
   localStorage.clear();
   return {type: USER_LOGOUT}
}


const success = (token) => {
  localStorage.setItem('cars_app_token', token);
  return {type: AUTH_SUCCESS, isAuth: true, error: null};
}

const error = (error) => {
  return { type: AUTH_FAILED, payload: error}
}


export const signUp = (request_data )=> {
  return async (dispatch) => {
    try {
      await apiSignUp(request_data);
      dispatch({ type: SIGNUP_SUCCESS });
    } catch (e) {
      dispatch({type: SIGNUP_FAILD, payload: e.response });
    }
  };
};