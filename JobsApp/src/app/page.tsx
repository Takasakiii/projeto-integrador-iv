"use client";

import { jobApi } from "@/core/api/job";
import { skillLevelText } from "@/core/models/user";
import { currencyFormat } from "@/core/utils/format";
import {
  Box,
  Card,
  CardBody,
  CardHeader,
  Heading,
  Stack,
  Text,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { useQuery } from "@tanstack/react-query";
import { NextPage } from "next";
import Link from "next/link";

const HomePage: NextPage = () => {
  const querJobs = useQuery(["jobs"], () => jobApi.list());

  return (
    <Box paddingTop="4">
      <Wrap justify="center" paddingBottom="4">
        {querJobs.data?.map((x, i) => (
          <WrapItem key={i}>
            <Link href={`/companies/${x.userId}`}>
              <Card width="96">
                <CardHeader>
                  <Heading>{x.companyName}</Heading>
                </CardHeader>
                <CardBody>
                  <Stack>
                    <Text>
                      {x.title} - {skillLevelText[x.level]} -{" "}
                      {currencyFormat.format(x.value)}
                    </Text>
                    <Text>{x.description}</Text>
                  </Stack>
                </CardBody>
              </Card>
            </Link>
          </WrapItem>
        ))}
      </Wrap>
    </Box>
  );
};

export default HomePage;
