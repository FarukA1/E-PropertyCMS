import apiService from "./api-service";

export const clientService = {
    getClients: (queryParams) => {
        const endpoint = "api/clients";
        return apiService.get(endpoint, queryParams);
    }
}