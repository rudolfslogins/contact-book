import axios from "axios";

export const baseClient = axios.create({
  baseURL: "http://localhost:8082/api/"
});
