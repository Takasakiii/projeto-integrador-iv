"use client";

import { setApiToken } from "@/core/api";
import { authApi } from "@/core/api/auth";
import UnauthorizedError from "@/core/errors/unauthorized";
import {
  AbsoluteCenter,
  Box,
  Button,
  Flex,
  FormControl,
  Input,
  Text,
  useToast,
} from "@chakra-ui/react";
import { yupResolver } from "@hookform/resolvers/yup";
import { useQueryClient } from "@tanstack/react-query";
import { NextPage } from "next";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import * as yup from "yup";

const schema = yup.object({
  email: yup.string().required().email(),
  password: yup.string().required(),
});

export type LoginForm = yup.InferType<typeof schema>;

const LoginPage: NextPage = () => {
  const {
    register,
    handleSubmit,
    formState: { isSubmitting, errors },
  } = useForm<LoginForm>({
    resolver: yupResolver(schema),
  });
  const router = useRouter();
  const toast = useToast();
  const queryClient = useQueryClient();

  const onSubmit = handleSubmit(async (values) => {
    try {
      const response = await authApi.login(values);
      setApiToken(response.token);

      localStorage.setItem("token", response.token);

      queryClient.invalidateQueries({
        predicate: (x) => x.queryKey[0] === "@me",
      });

      toast({
        title: "Login",
        description: "Successful login",
        status: "success",
      });

      router.push("/");
    } catch (error) {
      if (error instanceof UnauthorizedError) {
        toast({
          title: "Login",
          description: error.message,
          status: "warning",
        });
      }
    }
  });

  return (
    <Box h="calc(100vh - 56px)" pos="relative">
      <AbsoluteCenter>
        <Box rounded="lg" boxShadow="lg" p="4">
          <form onSubmit={onSubmit}>
            <Text fontSize="4xl" textAlign="center" marginBottom="4">
              Login
            </Text>
            <Flex direction="column" gap="2">
              <FormControl isInvalid={!!errors.email}>
                <Input
                  type="email"
                  placeholder="Email"
                  {...register("email")}
                />
              </FormControl>
              <FormControl isInvalid={!!errors.password}>
                <Input
                  type="password"
                  placeholder="Password"
                  {...register("password")}
                />
              </FormControl>
              <Button
                type="submit"
                isLoading={isSubmitting}
                bg="cyan.600"
                color="white"
              >
                Send
              </Button>
            </Flex>
          </form>
        </Box>
      </AbsoluteCenter>
    </Box>
  );
};

export default LoginPage;
