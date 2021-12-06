import { handleResponse, handleError } from './response'; 
var ClientOAuth2 = require('client-oauth2')


const BASE_URL = 'https://localhost:44389'; 

const getPSPToken = (client_id, client_secret, scopes, grant_type='client_credentials') => { 
    var authRequest = new ClientOAuth2({
        clientId: client_id,
        clientSecret: client_secret,
        accessTokenUri: `${BASE_URL}/connect/token`,
        scopes: scopes
     });
    
     return authRequest.credentials.getToken()
     .then(handleResponse) 
     .catch(handleError); 
}; 


export const apiIdentityProvider = { 
    getPSPToken
  };