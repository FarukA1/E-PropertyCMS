import React from 'react';
import { useAuth0 } from "@auth0/auth0-react";
import Auth0Service from './components/app-services/auth0-service';
import Login from './components/Login';

const AuthenticateRoute = ({ component: Component, isAuthenticated, path }) => {
    debugger;
    if(path == "startSession") {
        return <Component path="{path}" />;
    }
    if (isAuthenticated === true) {
      return <Component path="{path}" />;
    } else {
      // Redirect to the login page or handle authentication flow
      return <Login />
    }
  };

export default AuthenticateRoute;