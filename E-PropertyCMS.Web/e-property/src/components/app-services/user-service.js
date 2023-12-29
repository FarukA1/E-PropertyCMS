import apiService from "./api-service";

export const UserService = {
    getCurrentUser: (queryParams) => {
        const endpoint = "api/users/currentuser";
        return apiService.get(endpoint, queryParams);
    }
}