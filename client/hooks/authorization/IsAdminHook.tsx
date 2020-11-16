import Cookie from 'universal-cookie';
import Role from '../../core/models/authorization/Roles';
import CookieDictionary from '../../core/models/settings/cookieSettings/CookieDictionary';
import isEmpty from '../../core/models/utils/StringExtension';

const useIsAdmin = (): () => boolean => {

    const isAdmin = () => {
        const roles: string = new Cookie().get(CookieDictionary.Roles);
        if(isEmpty(roles))
            return false;
        const splittedRoles = roles.split(",");
        return splittedRoles.some(r => r === Role.Administrator);
    }

    return isAdmin
}

export default useIsAdmin;