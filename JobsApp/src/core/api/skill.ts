import { AxiosError } from "axios";
import { api } from ".";
import { Skill } from "../models/skill";
import HttpError from "../errors/http";

const baseUrl = "skills";

export const skillApi = {
  async api() {
    try {
      const response = await api.get(baseUrl);
      return response.data as Skill[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
};
