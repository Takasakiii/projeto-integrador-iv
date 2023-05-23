import { AxiosError } from "axios";
import { api } from ".";
import { UserJob, UserJobCreate } from "../models/user";
import NotFoundError from "../errors/notFound";
import ForbiddenError from "../errors/forbidden";
import HttpError from "../errors/http";

const baseUrl = "/api/jobs";

export const jobApi = {
  async post(data: UserJobCreate) {
    try {
      const response = await api.post(baseUrl, data);
      return response.data as UserJob;
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
      await api.delete(`${baseUrl}/${id}`);
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
  async list() {
    try {
      const response = await api.get(baseUrl);
      return response.data as UserJob[];
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.status) {
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
};
