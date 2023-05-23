import { AxiosError } from "axios";
import { api } from ".";
import NotFoundError from "../errors/notFound";
import HttpError from "../errors/http";
import BadRequestError from "../errors/badRequest";

const baseUrl = "/api/images";

export const imageApi = {
  async post(file: File) {
    try {
      const response = await api.postForm(`${baseUrl}`, {
        file,
      });
      return response.data as string;
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.response?.status) {
          case 400:
            throw new BadRequestError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
  async delete(id: string) {
    try {
      await api.delete(`${baseUrl}/${id}`);
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.response?.status) {
          case 404:
            throw new NotFoundError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
};
