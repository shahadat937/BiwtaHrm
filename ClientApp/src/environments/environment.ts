// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'http://localhost:25971/api/hrm',
  securityUrl: 'http://localhost:25971/api',
  fileUrl: 'http://localhost:25971/Content/',
  signalREndpoint: 'http://localhost:25971/notification',
  imageUrl: 'http://localhost:25971/assets/images/',
  companyTitle :"Bangladesh Inland Water Transport Authority",
  companyAddress : "141-143, Motijheel Commerial Area, Dhaka-1000",
  officerFormName: "Annual Confidential Record Of Officer",
  staffFormName: "Annual Confidential Record Of Staff",
  officerFormId: 1,
  staffFormId: 2,
  officerGradeType: 2,
  staffGradeType: 3


};

// export const environment = {
//   production: false,
//   apiUrl: 'http://118.179.158.165:8089/api/hrm',
//   securityUrl: 'http://118.179.158.165:8089/api',
//   fileUrl: 'http://118.179.158.165:8089/Content/',
//   imageUrl: 'http://118.179.158.165:8089/assets/images/',
// };

// export const environment = {
//   production: false,
//   apiUrl: 'http://114.134.95.238:9091/api/hrm',
//   securityUrl: 'http://114.134.95.238:9091/api',
//   fileUrl: 'http://114.134.95.238:9091/Content/',
//   imageUrl: 'http://114.134.95.238:9091/assets/images/',
// };
// export const environment = {
//   production: false,
//   apiUrl: 'http://114.134.95.238:8099/api/hrm',
//   securityUrl: 'http://114.134.95.238:8099/api',
//   fileUrl: 'http://114.134.95.238:8099/Content/',
//   imageUrl: 'http://114.134.95.238:8099/assets/images/',
// };
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
