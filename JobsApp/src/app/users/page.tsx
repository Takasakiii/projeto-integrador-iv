"use client";

import UserHeader from "@/components/UserHeader";
import { userApi } from "@/core/api/user";
import { UserType } from "@/core/models/user";
import { Box, Card, CardHeader, Wrap, WrapItem } from "@chakra-ui/react";
import { useQuery } from "@tanstack/react-query";
import { NextPage } from "next";
import Link from "next/link";

const UsersPage: NextPage = () => {
  const queryUsers = useQuery(["users"], () =>
    userApi.list({
      type: UserType.Professional,
    })
  );

  return (
    <Box paddingTop="4">
      <Wrap justify="center" paddingBottom="4">
        {queryUsers.data?.map((x, i) => (
          <WrapItem key={i}>
            <Link href={`/users/${x.id}`}>
              <Card width="96">
                <CardHeader>
                  <UserHeader user={x} />
                </CardHeader>
              </Card>
            </Link>
          </WrapItem>
        ))}
      </Wrap>
    </Box>
  );
};

export default UsersPage;
