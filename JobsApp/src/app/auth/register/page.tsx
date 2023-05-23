"use client";

import {
  AbsoluteCenter,
  Box,
  Button,
  Checkbox,
  Flex,
  FormControl,
  Input,
  Text,
} from "@chakra-ui/react";
import { NextPage } from "next";
import { useFormRegister } from "@/hooks/register";

const RegisterPage: NextPage = () => {
  const { register, errors, isSubmitting, onSubmit } = useFormRegister();

  return (
    <Box h="calc(100vh - 56px)" pos="relative">
      <AbsoluteCenter>
        <Box rounded="lg" boxShadow="lg" p="4">
          <form onSubmit={onSubmit}>
            <Text fontSize="4xl" textAlign="center" marginBottom="4">
              Register
            </Text>
            <Flex direction="column" gap="2">
              <FormControl isInvalid={!!errors.name}>
                <Input placeholder="Name" {...register("name")} />
              </FormControl>
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
              <FormControl isInvalid={!!errors.repeatPassword}>
                <Input
                  type="password"
                  placeholder="RepeatPassword"
                  {...register("repeatPassword")}
                />
              </FormControl>
              <FormControl>
                <Checkbox colorScheme="cyan" {...register("isCompany")}>
                  Empresa
                </Checkbox>
              </FormControl>
              <Button
                type="submit"
                isLoading={isSubmitting}
                bg="cyan.600"
                color="white"
                marginTop="4"
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

export default RegisterPage;
