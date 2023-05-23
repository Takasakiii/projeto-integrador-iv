import { SkillLevel } from "./user";

export interface StatisticUserSkill {
  skill: string;
  count: number;
}

export interface StatisticAverageValue {
  skill: string;
  value: number;
}

export interface StatisticJobLevel {
  level: SkillLevel;
  count: number;
  value: number;
}
