import { AxiosError } from "axios";
import { api } from ".";
import { UserWork, UserWorkCreate } from "../models/user";
import NotFoundError from "../errors/notFound";
import ForbiddenError from "../errors/forbidden";
import HttpError from "../errors/http";

const baseUrl = "/api/work-skills";

export const workSkillApi = {
  async post(data: string[], id: number) {
    try {
      const response = await api.post(`${baseUrl}/${id}`, data);
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
