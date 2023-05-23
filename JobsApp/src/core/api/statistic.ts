import { AxiosError } from "axios";
import { api } from ".";
import {
  StatisticAverageValue,
  StatisticJobLevel,
  StatisticUserSkill,
} from "../models/statistic";
import HttpError from "../errors/http";

const baseUrl = "/api/statistics";

export const statisticApi = {
  async mostUsedLanguage() {
    try {
      const response = await api.get(`${baseUrl}/most-used-language`);
      return response.data as StatisticUserSkill[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
  async averageValueLanguage() {
    try {
      const response = await api.get(`${baseUrl}/average-value-language`);
      return response.data as StatisticAverageValue[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
  async amountSkillUser() {
    try {
      const response = await api.get(`${baseUrl}/amount-skill-user`);
      return response.data as StatisticUserSkill[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
  async jobLevelCount() {
    try {
      const response = await api.get(`${baseUrl}/job-level-count`);
      return response.data as StatisticJobLevel[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
  async lessUsedLanguage() {
    try {
      const response = await api.get(`${baseUrl}/less-used-language`);
      return response.data as StatisticUserSkill[];
    } catch (error) {
      if (error instanceof AxiosError) {
        throw new HttpError(error);
      }
      throw error;
    }
  },
};
