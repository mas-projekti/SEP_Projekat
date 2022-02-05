import axios from 'axios'; 
import { handleResponse, handleError } from './response'; 

const BASE_URL = 'https://localhost:44313'; 


const createOrder = (orderDto) =>{    
    const config = {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('psp-token')}` }
    };
  return axios 
    .post(`${BASE_URL}/payment-service/bitcoin`, orderDto, config) 
    .then(handleResponse) 
    .catch(handleError); 
};


export const apiBitcoinProvider = { 
    createOrder
  };