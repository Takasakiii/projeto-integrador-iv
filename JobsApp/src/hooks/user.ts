import { userApi } from "@/core/api/user";
import { userSkillApi } from "@/core/api/userSkill";
import { useQuery } from "@tanstack/react-query";

export const useUser = (id: number) => {
  const queryClient = useQuery(["user", id], () => userApi.get(id));

  return {
    user: queryClient.data,
  };
};

export const useUserSkills = (userId?: number) => {
  const queryClient = useQuery(["userSkills", userId], () => {
    if (userId) return userSkillApi.list(userId);
  });

  return {
    skills: queryClient.data,
  };
};
