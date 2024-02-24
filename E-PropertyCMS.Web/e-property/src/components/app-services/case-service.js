import apiService from "./api-service";

export const caseService = {
    getCases: (queryParams) => {
        const endpoint = "api/cases";
        return apiService.get(endpoint, queryParams);
    },

    getCaseById: (id,queryParams) => {
        const endpoint = `api/cases/${id}`;
        return apiService.get(endpoint, queryParams);
    }
}