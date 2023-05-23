"use client";

import { useSelector } from "@legendapp/state/react";
import { userStorage } from "../core/storage";
import { setApiToken } from "@/core/api";
import { userApi } from "@/core/api/user";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useEffect, useState } from "react";

export const useAuth = () => {
  const user = useSelector(userStorage);
  const queryClient = useQueryClient();
  const [token, setToken] = useState(
    () => typeof window != "undefined" && localStorage.getItem("token")
  );

  const query = useQuery(["@me", token], () => {
    return userApi.me();
  });

  useEffect(() => {
    if (token) {
      setApiToken(token);
    }
  }, [token]);

  useEffect(() => {
    if (query.data && query.isFetched) {
      userStorage.set(query.data);
    }
  }, [query]);

  const logoff = () => {
    localStorage.removeItem("token");
    userStorage.set(null);
    setApiToken("");
    setToken("");
    queryClient.invalidateQueries(["@me"]);
  };

  return {
    user,
    logoff,
  };
};
