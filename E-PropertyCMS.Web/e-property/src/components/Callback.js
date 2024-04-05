import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Auth0Service from './app-services/auth0-service';

const Callback =() => {
    const navigate = useNavigate();

    useEffect(() => {
        const handleAuthentication = async () => {
          const auth0Service = new Auth0Service();
          try {
            // Perform the authentication process
            await auth0Service.handleAuthentication();

            // If authentication is successful, navigate to the "/" path
            navigate("/");
          } catch (error) {
            // Handle errors if needed
            console.error("Authentication error:", error);
          }
        };
    
        handleAuthentication();
      });
    
      return (
        <div>
          <p>Processing authentication...</p>
        </div>
      );
}

export default Callback;