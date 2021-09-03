import BaseApi from "../base/BaseApi.js"

class EmployeeApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "api/v1/Employees";
    }
}

export default new EmployeeApi();