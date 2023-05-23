"use client";

import { userApi } from "@/core/api/user";
import { skillLevelText } from "@/core/models/user";
import { currencyFormat } from "@/core/utils/format";
import { useConfig } from "@/hooks/config";
import {
  Accordion,
  AccordionButton,
  AccordionIcon,
  AccordionItem,
  AccordionPanel,
  Avatar,
  Box,
  Card,
  CardBody,
  CardHeader,
  Flex,
  Grid,
  GridItem,
  Heading,
  Stack,
  Text,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { useQuery } from "@tanstack/react-query";
import { NextPage } from "next";
import { useMemo } from "react";

interface CompanyPageParams {
  id: number;
}

interface CompanyPageProps {
  params: CompanyPageParams;
}

const CompanyPage: NextPage<CompanyPageProps> = ({ params }) => {
  const queryUser = useQuery(["user", params.id], () => userApi.get(params.id));

  const { apiUrl } = useConfig();

  const user = queryUser.data;
  const jobs = useMemo(
    () =>
      user?.jobs?.map((x) => ({
        ...x,
        requiredSkills: x.skills.filter((x) => !x.optional),
        optionalSkills: x.skills.filter((x) => x.optional),
        level: skillLevelText[x.level],
        value: currencyFormat.format(x.value),
      })),
    [user?.jobs]
  );

  if (!queryUser.data) {
    return <></>;
  }

  return (
    <Box paddingTop="4">
      <Wrap justify="center" paddingBottom="4">
        <WrapItem>
          <Stack>
            <Card width="96" height="fit-content">
              <CardHeader>
                <Flex gap="4" alignItems="end">
                  <Avatar
                    size="2xl"
                    name={user?.name}
                    src={
                      user?.imageId && `${apiUrl}/api/images/${user.imageId}`
                    }
                    bg="gray.100"
                    color="black"
                  />
                  <Stack>
                    <Heading>{user?.name}</Heading>
                    <Text>
                      {user?.role === null || user?.role === ""
                        ? "-"
                        : user?.role}
                    </Text>
                  </Stack>
                </Flex>
              </CardHeader>
              <CardBody>
                <Stack>
                  <Heading size="md">Descrição</Heading>
                  <Text>{queryUser.data.description ?? "-"}</Text>
                </Stack>
              </CardBody>
            </Card>
            <Card width="96">
              <CardHeader>
                <Heading size="md">Vagas</Heading>
              </CardHeader>
              <CardBody>
                <Accordion defaultIndex={jobs?.map((_, i) => i)} allowMultiple>
                  {jobs?.map((x, i) => (
                    <AccordionItem key={i}>
                      <h2>
                        <AccordionButton>
                          <Box as="span" flex="1" textAlign="left">
                            {x.title} - {x.level}
                          </Box>
                          <AccordionIcon />
                        </AccordionButton>
                      </h2>
                      <AccordionPanel>
                        <Grid gap="4" templateColumns="repeat(2, 1fr)">
                          <GridItem colSpan={1}>
                            <Stack>
                              <Text as="strong">Nível</Text>
                              <Text>{x.level}</Text>
                            </Stack>
                          </GridItem>
                          <GridItem colSpan={1}>
                            <Stack>
                              <Text as="strong">Valor</Text>
                              <Text>{x.value}</Text>
                            </Stack>
                          </GridItem>
                          <GridItem colSpan={2}>
                            <Stack>
                              <Text as="strong">Descrição</Text>
                              <Text>{x.description}</Text>
                            </Stack>
                          </GridItem>
                          <GridItem colSpan={2}>
                            <Stack>
                              <Text as="strong">Habilidades Requeridas</Text>
                              {x.requiredSkills.map((x, i) => (
                                <Text key={i}>{x.skill}</Text>
                              ))}
                            </Stack>
                          </GridItem>
                          <GridItem colSpan={2}>
                            <Stack>
                              <Text as="strong">Habilidades Extras</Text>
                              {x.optionalSkills.map((x, i) => (
                                <Text key={i}>{x.skill}</Text>
                              ))}
                            </Stack>
                          </GridItem>
                        </Grid>
                      </AccordionPanel>
                    </AccordionItem>
                  ))}
                </Accordion>
              </CardBody>
            </Card>
          </Stack>
        </WrapItem>
      </Wrap>
    </Box>
  );
};

export default CompanyPage;
