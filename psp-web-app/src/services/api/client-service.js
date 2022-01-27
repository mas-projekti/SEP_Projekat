import axios from 'axios'; 
import { handleResponse} from './response'; 

const BASE_URL = 'https://localhost:44313'; 

const createNewClient = (clientData) =>{    
  return axios 
    .post(`${BASE_URL}/payment-service/clients`, clientData, {}) 
    .then(handleResponse) 
};

const notifyClientTransactionFinished = (transactionId) =>{    
  return axios 
    .post(`${BASE_URL}/payment-service/clients/notify/${transactionId}`, {}, {}) 
    .then(handleResponse) 
};

const getClientById = (clientId) =>{    
  return axios 
    .get(`${BASE_URL}/payment-service/clients/${clientId}/info`) 
    .then(handleResponse)
  };

const getClientByClientId = (clientId) =>{    
  const config = {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('client-token')}` }
  };
return axios 
  .get(`${BASE_URL}/payment-service/clients/${clientId}`,  config) 
  .then(handleResponse)
};

const updateClient = (id, client) =>{    
  const config = {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('client-token')}` }
  };
return axios 
  .put(`${BASE_URL}/payment-service/clients/${id}`, client,  config) 
  .then(handleResponse) 
};

export const apiClientsProvider = { 
    createNewClient,
    getClientByClientId,
    updateClient,
    notifyClientTransactionFinished,
    getClientById
  };