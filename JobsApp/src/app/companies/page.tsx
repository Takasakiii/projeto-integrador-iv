"use client";

import CompanyHeader from "@/components/CompanyHeader";
import { userApi } from "@/core/api/user";
import { UserType } from "@/core/models/user";
import { Box, Card, CardHeader, Wrap, WrapItem } from "@chakra-ui/react";
import { useQuery } from "@tanstack/react-query";
import { NextPage } from "next";
import Link from "next/link";

const CompaniesPage: NextPage = () => {
  const queryCompanies = useQuery(["users"], () =>
    userApi.list({
      type: UserType.Company,
    })
  );

  return (
    <Box paddingTop="4">
      <Wrap justify="center" paddingBottom="4">
        {queryCompanies.data?.map((x, i) => (
          <WrapItem key={i}>
            <Link href={`/companies/${x.id}`}>
              <Card width="96">
                <CardHeader>
                  <CompanyHeader user={x} />
                </CardHeader>
              </Card>
            </Link>
          </WrapItem>
        ))}
      </Wrap>
    </Box>
  );
};

export default CompaniesPage;
