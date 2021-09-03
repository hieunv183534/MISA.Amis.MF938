import axios from "axios";

var BaseAPIConfig = axios.create({
	baseURL: "http://localhost:62168/",
});

export default BaseAPIConfig;