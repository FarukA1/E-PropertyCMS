import apiService from "./api-service";

export const dashboardService = {
    getDetails: () => {
        const endpoint = "api/dashboard/detail";
        return apiService.get(endpoint);
    },
}  