import { AxiosError } from "axios";
import { api } from ".";
import { UserWork, UserWorkCreate } from "../models/user";
import NotFoundError from "../errors/notFound";
import ForbiddenError from "../errors/forbidden";
import HttpError from "../errors/http";

const baseUrl = "/api/works";

export const workApi = {
  async post(data: UserWorkCreate) {
    try {
      const response = await api.post(baseUrl, data);
      return response.data as UserWork;
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.status) {
          case 404:
            throw new NotFoundError(error.response?.data);
          case 403:
            throw new ForbiddenError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
  async delete(id: number) {
    try {
      const response = await api.delete(`${baseUrl}/${id}`);
      return response.data as UserWork;
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.status) {
          case 404:
            throw new NotFoundError(error.response?.data);
          case 403:
            throw new ForbiddenError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
};
