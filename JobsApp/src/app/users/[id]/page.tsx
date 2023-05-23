"use client";

import Skill from "@/components/Skill";
import UserHeader from "@/components/UserHeader";
import { userApi } from "@/core/api/user";
import { skillLevelText } from "@/core/models/user";
import { currencyFormat } from "@/core/utils/format";
import {
  Accordion,
  AccordionButton,
  AccordionIcon,
  AccordionItem,
  AccordionPanel,
  Badge,
  Box,
  Card,
  CardBody,
  CardHeader,
  Grid,
  GridItem,
  Heading,
  Stack,
  Text,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { useQuery } from "@tanstack/react-query";
import humanizeDuration from "humanize-duration";
import { DateTime, Interval } from "luxon";
import { NextPage } from "next";
import { useMemo } from "react";

interface UserPageParams {
  id: number;
}

interface UserPageProps {
  params: UserPageParams;
}

const UserPage: NextPage<UserPageProps> = ({ params }) => {
  const queryUser = useQuery(["user", params.id], () => userApi.get(params.id));

  const skills = useMemo(
    () => queryUser.data?.skills?.sort((x, y) => y.years - x.years),
    [queryUser.data?.skills]
  );

  const works = useMemo(
    () =>
      queryUser.data?.works
        ?.sort((x, y) => Number(x.startAt < y.startAt))
        .map((x) => ({
          ...x,
          years: humanizeDuration(
            Interval.fromDateTimes(
              DateTime.fromISO(x.startAt),
              x.endAt ? DateTime.fromISO(x.endAt) : DateTime.now()
            )
              .toDuration()
              .valueOf(),
            {
              language: "pt",
              largest: 2,
              conjunction: " e ",
            }
          ),
        })),
    [queryUser.data?.works]
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
                <UserHeader user={queryUser.data} />
              </CardHeader>
              <CardBody>
                <Stack>
                  <Heading size="md">Descrição</Heading>
                  <Text>{queryUser.data.description ?? "-"}</Text>
                </Stack>
              </CardBody>
            </Card>
            <Card>
              <CardHeader>
                <Heading size="md">Trabalhos</Heading>
              </CardHeader>
              <CardBody>
                <Accordion defaultIndex={works?.map((_, i) => i)} allowMultiple>
                  {works?.map((x, i) => (
                    <AccordionItem key={i}>
                      <h2>
                        <AccordionButton>
                          <Box as="span" flex="1" textAlign="left">
                            {x.title} - {x.years}
                          </Box>
                          <AccordionIcon />
                        </AccordionButton>
                      </h2>
                      <AccordionPanel>
                        <Grid gap="4" templateColumns="repeat(2, 1fr)">
                          <GridItem colSpan={2}>
                            <Stack>
                              <Text as="strong">Período</Text>
                              <Text>
                                {new Date(x.startAt).toLocaleDateString()} -{" "}
                                {x.endAt
                                  ? new Date(x.endAt).toLocaleDateString()
                                  : "Até o momento"}
                              </Text>
                            </Stack>
                          </GridItem>
                          <GridItem colSpan={2}>
                            <Stack>
                              <Text as="strong">Valor</Text>
                              <Text>{currencyFormat.format(x.value)}</Text>
                            </Stack>
                          </GridItem>
                          <GridItem colSpan={2}>
                            <Text as="strong">Descrição</Text>
                            <Text>{x.description}</Text>
                          </GridItem>
                          {x.skills && x.skills.length > 0 && (
                            <GridItem colSpan={2}>
                              <Wrap>
                                {x.skills.map((x, i) => (
                                  <Badge key={i}>{x}</Badge>
                                ))}
                              </Wrap>
                            </GridItem>
                          )}
                        </Grid>
                      </AccordionPanel>
                    </AccordionItem>
                  ))}
                </Accordion>
              </CardBody>
            </Card>
          </Stack>
        </WrapItem>
        <WrapItem>
          <Card width="96" height="fit-content">
            <CardHeader>
              <Heading size="md">Habilidades</Heading>
            </CardHeader>
            <CardBody>
              <Stack>
                {skills &&
                  skills.map((x, i) => (
                    <Skill
                      text={x.skill}
                      level={skillLevelText[x.level]}
                      years={x.years}
                      maxYears={skills[0].years}
                      key={i}
                    />
                  ))}
              </Stack>
            </CardBody>
          </Card>
        </WrapItem>
      </Wrap>
    </Box>
  );
};

export default UserPage;
