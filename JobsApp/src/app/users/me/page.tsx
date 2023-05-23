"use client";

import Skill from "@/components/Skill";
import UserHeader from "@/components/UserHeader";
import { useAuth } from "@/hooks/auth";
import {
  Box,
  Card,
  CardBody,
  CardHeader,
  Heading,
  Stack,
  Text,
} from "@chakra-ui/react";
import { NextPage } from "next";
import { useRouter } from "next/navigation";
import { useEffect } from "react";

const UserMePage: NextPage = () => {
  const { user } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!user) {
      router.push("/");
    }
  }, [router, user]);

  return (
    <>
      {user && (
        <Box paddingTop="4">
          <Stack direction="row" width="full" justifyContent="center">
            <Card width="md" height="fit-content">
              <CardHeader>
                <UserHeader user={user} />
              </CardHeader>
              <CardBody>
                <Stack>
                  <Heading size="md">Descrição</Heading>
                  <Text>{user.description ?? "-"}</Text>
                </Stack>
              </CardBody>
            </Card>
            <Card width="sm" height="fit-content">
              <CardHeader>
                <Heading size="md">Habilidades</Heading>
              </CardHeader>
              <CardBody>
                <Stack>
                  <Skill text="C#" level="Senior" years={3} maxYears={3} />
                  <Skill
                    text="Javascript"
                    level="Pleno"
                    years={2}
                    maxYears={3}
                  />
                  <Skill text="Rust" level="Junior" years={1} maxYears={3} />
                </Stack>
              </CardBody>
            </Card>
          </Stack>
        </Box>
      )}
    </>
  );
};

export default UserMePage;
