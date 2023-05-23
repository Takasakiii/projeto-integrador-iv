import { AxiosError } from "axios";
import { api } from ".";
import { Login } from "../models/auth";
import { JwtToken } from "../models/jwt";
import UnauthorizedError from "../errors/unauthorized";
import HttpError from "../errors/http";

const baseUrl = "/api/auth";

export const authApi = {
  async login(data: Login) {
    try {
      const response = await api.post(`${baseUrl}/login`, data);
      return response.data as JwtToken;
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.response?.status) {
          case 401:
            throw new UnauthorizedError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
};
