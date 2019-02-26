import { AuthenticationContext, adalFetch, withAdalLogin } from 'react-adal';

export const adalConfig = {
  tenant: 'microsoft.onmicrosoft.com',
  clientId: '29792077-f3fb-4c9b-8292-306fafbc135b',
  endpoints: {
    api: 'https://microsoft.onmicrosoft.com/29792077-f3fb-4c9b-8292-306fafbc135b',
  },
  postLogoutRedirectUri: window.location.origin,
  cacheLocation: 'sessionStorage'
};

export const authContext = new AuthenticationContext(adalConfig);

export const adalApiFetch = (fetch, url, options) =>
  adalFetch(authContext, adalConfig.endpoints.api, fetch, url, options);

export const withAdalLoginApi = withAdalLogin(authContext, adalConfig.endpoints.api);

export const getToken = () => {
 return authContext.getCachedToken(authContext.config.clientId);
};