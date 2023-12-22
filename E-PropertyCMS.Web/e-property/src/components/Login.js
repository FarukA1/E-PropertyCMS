import React from 'react';
import Auth0Service from './app-services/auth0-service';
const Login =() => {
    const { login } = new Auth0Service();
    return login();
}

export default Login;