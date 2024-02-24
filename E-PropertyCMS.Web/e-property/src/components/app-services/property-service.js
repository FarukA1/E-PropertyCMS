import apiService from "./api-service";

export const propertyService = {
    getProperties: (queryParams) => {
        const endpoint = "api/properties";
        return apiService.get(endpoint, queryParams);
    },

    getPropertyById: (id,queryParams) => {
        const endpoint = `api/properties/${id}`;
        return apiService.get(endpoint, queryParams);
    }
}