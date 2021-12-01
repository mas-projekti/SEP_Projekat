import axios from 'axios'; 
import { handleResponse, handleError } from './response'; 

const BASE_URL = 'https://localhost:44313'; 


const createOrder = (orderDto) =>{    
    const config = {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('psp-token')}` }
    };
  return axios 
    .post(`${BASE_URL}/paypal/orders`, orderDto, config) 
    .then(handleResponse) 
    .catch(handleError); 
};

const capturePaypalOrder = (orderID) =>{
    const config = {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('psp-token')}` }
    };    

  return axios 
    .post(`${BASE_URL}/paypal/orders/${orderID}/capture`, {}, config) 
    .then(handleResponse) 
    .catch(handleError); 
};

export const apiPaypalProvider = { 
    capturePaypalOrder,
    createOrder
  };