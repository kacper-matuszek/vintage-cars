import Cookie from 'universal-cookie';
import CookieDictionary from '../settings/cookieSettings/CookieDictionary';

export const getAuthorizationToken = () => new Cookie().get(CookieDictionary.Token);