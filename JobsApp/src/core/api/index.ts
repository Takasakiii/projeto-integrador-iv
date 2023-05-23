import axios from "axios";
import { API_URL } from "../config";

const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Context-Type": "application/json",
  },
});

const setApiToken = (token: string) => {
  api.defaults.headers.common.Authorization = `Bearer ${token}`;
};

export { api, setApiToken };
