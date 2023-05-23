import { Option } from ".";

export enum UserType {
  Professional = 1,
  Company = 2,
}

export interface UserCreate {
  name: string;
  email: string;
  password: string;
  repeatPassword: string;
  type: UserType;
}

export interface User {
  id: number;
  name: string;
  email: string;
  type: UserType;
  imageId?: string;
  description?: string;
  expectedValue?: number;
  role?: string;
  skills?: UserSkill[];
  works?: UserWork[];
  jobs?: UserJob[];
}

export interface UserUpdate {
  imageId?: string;
  description?: string;
  expectedValue?: number;
  role?: string;
}

export enum SkillLevel {
  Junior,
  Pleno,
  Senior,
}

export interface UserSkill {
  id: number;
  userId: number;
  skillId: number;
  skill: string;
  level: SkillLevel;
  years: number;
}

export interface UserSkillCreate {
  id: number;
  skill: string;
  level: SkillLevel;
  years: number;
}

export interface UserWork {
  id: number;
  title: string;
  description: string;
  startAt: string;
  endAt?: string;
  value: number;
  skills: string[];
}

export interface UserWorkCreate {
  id: number;
  title: string;
  description: string;
  startAt: string;
  endAt?: string;
  value: number;
  skills: string[];
}

export interface UserFilter {
  page?: number;
  pageSize?: number;
  type?: UserType;
}

export interface UserJobCreate {
  id: number;
  title: string;
  description: string;
  level: SkillLevel;
  value: number;
  skills: UserJobSkill[];
}

export interface UserJob {
  id: number;
  title: string;
  description: string;
  level: SkillLevel;
  value: number;
  userId: number;
  companyName?: string;
  skills: UserJobSkill[];
}

export interface UserJobSkill {
  skill: string;
  optional: boolean;
}

export const skillLevelOptions: Option<SkillLevel>[] = [
  {
    value: SkillLevel.Junior,
    label: "Junior",
  },
  {
    value: SkillLevel.Pleno,
    label: "Pleno",
  },
  {
    value: SkillLevel.Senior,
    label: "Senior",
  },
];

export const skillLevelText = {
  [SkillLevel.Junior]: "Junior",
  [SkillLevel.Pleno]: "Pleno",
  [SkillLevel.Senior]: "Senior",
};
