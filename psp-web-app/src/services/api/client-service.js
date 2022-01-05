import axios from 'axios'; 
import { handleResponse, handleError } from './response'; 

const BASE_URL = 'https://localhost:44313'; 

const createNewClient = (clientData) =>{    
  return axios 
    .post(`${BASE_URL}/payment-service/clients`, clientData, {}) 
    .then(handleResponse) 
    .catch(handleError); 
};

export const apiClientsProvider = { 
    createNewClient,
  };