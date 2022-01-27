import axios from 'axios'; 
import { handleResponse, handleError } from './response'; 

const BASE_URL = 'https://localhost:44313'; 

const getTransactionById = (transactionId) =>{    
    const config = {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('psp-token')}` }
    };
  return axios 
    .get(`${BASE_URL}/payment-service/transactions/${transactionId}`,  config) 
    .then(handleResponse) 
    .catch(handleError); 
};

const payWithBank = (transactionId) =>{    
  const config = {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('psp-token')}` }
  };
return axios 
  .post(`${BASE_URL}/payment-service/transactions/bank-payment/${transactionId}`, {}, config) 
  .then(handleResponse) 
  .catch(handleError); 
};

export const apiTransactionsProvider = { 
    getTransactionById,
    payWithBank
  };