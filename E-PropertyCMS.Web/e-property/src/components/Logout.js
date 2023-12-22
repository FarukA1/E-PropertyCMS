import React from 'react';
import Auth0Service from './app-services/auth0-service';
const Logout =() => {
    const { logout } = new Auth0Service();
    return logout();
}

export default Logout;