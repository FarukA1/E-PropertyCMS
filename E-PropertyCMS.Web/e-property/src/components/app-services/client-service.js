import apiService from "./api-service";

export const clientService = {
    getClients: (queryParams) => {
        const endpoint = "api/clients";
        return apiService.get(endpoint, queryParams);
    },

    getClientById: (id,queryParams) => {
        const endpoint = `api/clients/${id}`;
        return apiService.get(endpoint, queryParams);
    },

    getClientCases: (id,queryParams) => {
        const endpoint = `api/clients/${id}/cases`;
        return apiService.get(endpoint, queryParams);
    }
}