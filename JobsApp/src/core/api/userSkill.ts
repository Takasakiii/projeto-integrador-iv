import { AxiosError } from "axios";
import { api } from ".";
import { UserSkill, UserSkillCreate } from "../models/user";
import HttpError from "../errors/http";

const baseUrl = "/api/user-skills";

export const userSkillApi = {
  async list(userId: number) {
    try {
      const response = await api.get(baseUrl, {
        params: {
          userId,
        },
      });
      return response.data as UserSkill[];
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
  async post(userSkill: UserSkillCreate) {
    try {
      const response = await api.post(baseUrl, userSkill);
      return response.data as UserSkill;
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
