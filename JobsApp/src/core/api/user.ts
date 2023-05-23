import { AxiosError } from "axios";
import {
  User,
  UserCreate,
  UserFilter,
  UserType,
  UserUpdate,
} from "../models/user";
import { api } from ".";
import HttpError from "../errors/http";
import ConflictError from "../errors/conflict";
import NotFoundError from "../errors/notFound";
import ForbiddenError from "../errors/forbidden";

const baseUrl = "/api/users";

export const userApi = {
  async post(data: UserCreate) {
    try {
      const response = await api.post(baseUrl, data);
      return response.data;
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.status) {
          case 401:
            throw new ConflictError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
  async me() {
    try {
      const response = await api.get(`${baseUrl}/@me`);
      return response.data as User;
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
  async get(id: number) {
    try {
      const response = await api.get(`${baseUrl}/${id}`);
      return response.data as User;
    } catch (error) {
      if (error instanceof AxiosError) {
        switch (error.status) {
          case 404:
            throw new NotFoundError(error.response?.data);
          default:
            throw new HttpError(error);
        }
      }
      throw error;
    }
  },
  async update(id: number, user: UserUpdate) {
    try {
      const response = await api.put(`${baseUrl}/${id}`, user);
      return response.data as User;
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
  async list(filter?: UserFilter) {
    try {
      const response = await api.get(baseUrl, {
        params: filter,
      });
      return response.data as User[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
};
