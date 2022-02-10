import axios from "axios";

const api = axios.create({
    baseURL : "http://192.168.3.106:5000/api"
})

export default api;