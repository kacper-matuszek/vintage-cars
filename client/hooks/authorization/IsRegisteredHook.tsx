import Cookie from 'universal-cookie';
import Role from '../../core/models/authorization/Roles';
import CookieDictionary from '../../core/models/settings/cookieSettings/CookieDictionary';
import { isStringNullOrEmpty } from '../../core/models/utils/StringExtension';

const useIsRegistered = (): () => boolean => {

    const isRegistered = () => {
        const roles: string = new Cookie().get(CookieDictionary.Roles);
        if(isStringNullOrEmpty(roles))
            return false;
        const splittedRoles = roles.split(",");
        return splittedRoles.some(r => r === Role.Registered);
    }

    return isRegistered;
}
export default useIsRegistered;