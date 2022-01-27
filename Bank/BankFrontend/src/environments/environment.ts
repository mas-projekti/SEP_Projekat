// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,

  BANK_PORT: 44355,

  AUTH_API: 'https://localhost:7286/api/auth/',
  USER_API: 'https://localhost:7286/api/users/',
  CAMERA_API: 'https://localhost:7286/api/cameras/',

  // Set the Bank env variables, and change index.html file for assets and title
  BANK_NAME: 'Erste',
  // BANK_NAME: 'Raiffeisen',
  BANK_LOGO_URL: 'https://www.14oktobar.rs/wp-content/uploads/2018/08/erste-bank-logo-png-transparent.png',
  // BANK_LOGO_URL: 'https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/Raiffeisen_Bank.svg/1200px-Raiffeisen_Bank.svg.png',
  BANK_LOGO_SIZE: {
    X: 500,
    Y: 200
  },
};

export function LOGIN_API() {
  return 'https://localhost:' + environment.BANK_PORT + '/api/clients/login';
};

export function REGISTER_API() {
  return 'https://localhost:' + environment.BANK_PORT + '/api/clients';
};

export function PAY_API() {
  return 'https://localhost:' + environment.BANK_PORT + '/api/payments/pay';
};

export function PAYMENT_CARD_API() {
  return 'https://localhost:' + environment.BANK_PORT + '/api/payment-cards/user/';
};

export function AUTHENTICATION_HEADER() {
  return {headers: {'Authorization': 'Bearer ' + localStorage.getItem("jwt")}};
};
export function AUTHENTICATION_OPTION() {
  return {'Authorization': 'Bearer ' + localStorage.getItem("jwt")};
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
