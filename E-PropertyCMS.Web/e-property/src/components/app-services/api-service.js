import { AUTH_CONFIG } from "../../config";

const BASE_URL = AUTH_CONFIG.audience;
let token = localStorage.getItem('access_token');

const handleResponse = (response) => {
  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }
  return response.json();
};

const apiService = {
  get: (endpoint,queryParams) => {
       // Convert queryParams to a query string
       const queryString = new URLSearchParams(queryParams).toString();
       
       // Append the query string to the endpoint if queryParams are provided
       const url = `${BASE_URL}${endpoint}${queryString ? `?${queryString}` : ''}`;

    return fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
        // You can add additional headers here if needed
      },
    }).then(handleResponse);
  },

  post: (endpoint, data) => {
    return fetch(`${BASE_URL}${endpoint}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
        // You can add additional headers here if needed
      },
      body: JSON.stringify(data),
    }).then(handleResponse);
  },

  put: (endpoint, data) => {
    return fetch(`${BASE_URL}/${endpoint}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
        // You can add additional headers here if needed
      },
      body: JSON.stringify(data),
    }).then(handleResponse);
  },

  // Add other HTTP methods as needed
};

export default apiService;