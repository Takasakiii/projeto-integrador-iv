"use client";

import { imageApi } from "@/core/api/image";
import { jobApi } from "@/core/api/job";
import { userApi } from "@/core/api/user";
import BadRequestError from "@/core/errors/badRequest";
import {
  SkillLevel,
  UserJobCreate,
  UserJobSkill,
  skillLevelOptions,
  skillLevelText,
} from "@/core/models/user";
import { useConfig } from "@/hooks/config";
import { AddIcon, DeleteIcon } from "@chakra-ui/icons";
import {
  Accordion,
  AccordionButton,
  AccordionIcon,
  AccordionItem,
  AccordionPanel,
  Avatar,
  Box,
  Button,
  Card,
  CardBody,
  CardFooter,
  CardHeader,
  Flex,
  FormControl,
  FormLabel,
  Grid,
  GridItem,
  Heading,
  IconButton,
  Input,
  InputGroup,
  InputLeftAddon,
  NumberDecrementStepper,
  NumberIncrementStepper,
  NumberInput,
  NumberInputField,
  NumberInputStepper,
  Select,
  Stack,
  Switch,
  Tab,
  TabList,
  TabPanel,
  TabPanels,
  Table,
  Tabs,
  Tbody,
  Td,
  Text,
  Textarea,
  Th,
  Thead,
  Tr,
  Wrap,
  useToast,
} from "@chakra-ui/react";
import { yupResolver } from "@hookform/resolvers/yup";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { NextPage } from "next";
import { useEffect, useMemo, useRef, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import * as yup from "yup";

interface EditCompanyPageParams {
  id: number;
}

interface EditCompanyPageProps {
  params: EditCompanyPageParams;
}

interface EditCompanyFormData {
  name?: string;
  role?: string;
  description?: string;
  imageFile?: FileList;
  jobs?: UserJobCreate[];
}

const schemaEditCompany = yup.object({
  name: yup.string().notRequired().max(100).trim(),
  role: yup.string().notRequired().max(100).trim(),
  description: yup.string().notRequired().max(255).trim(),
  imageFile: yup.mixed().notRequired(),
  jobs: yup.array<UserJobCreate>().of(
    yup.object({
      title: yup.string().max(100).trim().required(),
      description: yup.string().max(2000).trim().required(),
      level: yup.number().required(),
      value: yup.number().required(),
      skills: yup
        .array<UserJobSkill>()
        .transform((x) => x.slice(0, -1))
        .of(
          yup.object({
            skill: yup.string().max(30).required(),
            optional: yup.boolean().required(),
          })
        ),
    })
  ),
});

const EditCompanyPage: NextPage<EditCompanyPageProps> = ({ params }) => {
  const queryCompany = useQuery(["company", params.id], () =>
    userApi.get(params.id)
  );
  const [companyLoading, setCompanyLoading] = useState(false);
  const {
    register,
    handleSubmit,
    control,
    watch,
    setValue,
    getValues,
    formState: { isSubmitting, errors },
  } = useForm<EditCompanyFormData>({
    resolver: yupResolver(schemaEditCompany),
  });
  const toast = useToast();
  const queryClient = useQueryClient();
  const [jobCount, setJobCount] = useState(0);

  useEffect(() => {
    if (queryCompany.data && !companyLoading) {
      var data = queryCompany.data;

      setValue("description", data.description);
      setValue("name", data.name);
      setValue("role", data.role);
      setValue("jobs", data.jobs);

      if (data.jobs && data.jobs.length > 0) {
        setJobCount(data.jobs.length);

        for (let i = 0; i < data.jobs.length; i++) {
          setValue(`jobs.${i}.skills.${data.jobs[i].skills.length}.skill`, "");
          setValue(
            `jobs.${i}.skills.${data.jobs[i].skills.length}.optional`,
            false
          );
        }
      }

      setCompanyLoading(true);
    }
  }, [companyLoading, queryCompany.data, setValue]);

  const onSubmit = handleSubmit(async ({ imageFile, jobs, ...values }) => {
    if (!queryCompany.data) return;

    try {
      const image = imageFile?.item(0);
      let imageId = queryCompany.data.imageId;

      if (image) {
        imageId = await imageApi.post(image);
      }

      await userApi.update(params.id, {
        ...values,
        imageId,
      });

      if (image && queryCompany.data.imageId)
        await imageApi.delete(queryCompany.data.imageId);

      if (jobs) {
        for (let i = 0; i < jobs.length; i++) {
          const job = jobs[i];
          console.log(jobs);
          await jobApi.post(job);
        }

        const removeJobs = queryCompany.data.jobs?.filter(
          (x) => !jobs.find((y) => y.id === x.id)
        );

        if (removeJobs) {
          for (let i = 0; i < removeJobs.length; i++) {
            const job = removeJobs[i];
            await jobApi.delete(job.id);
          }
        }
      }

      queryClient.invalidateQueries(["company", params.id]);

      toast({
        status: "success",
        title: "Saved user",
      });
    } catch (error) {
      if (error instanceof BadRequestError) {
        toast({
          status: "warning",
          title: error.message,
        });
      } else {
        toast({
          status: "error",
          title: "Unexpected error",
        });
      }
    }
  });

  const { apiUrl } = useConfig();
  const { ref: imageRef, ...imageField } = register("imageFile");
  const imageCustomRef = useRef<HTMLInputElement | null>(null);

  const image = watch("imageFile")?.item(0);
  const imageData = useMemo(() => {
    return image
      ? URL.createObjectURL(image)
      : queryCompany.data?.imageId &&
          `${apiUrl}/api/images/${queryCompany.data?.imageId}`;
  }, [apiUrl, image, queryCompany.data?.imageId]);

  if (!queryCompany.data) return <></>;

  const jobs = watch("jobs");

  const onSelectImage = () => {
    imageCustomRef.current?.click();
  };

  const onAddJob = () => {
    setValue(`jobs.${jobCount}.id`, 0);
    setValue(`jobs.${jobCount}.skills.0.skill`, "");
    setValue(`jobs.${jobCount}.skills.0.optional`, false);
    setJobCount((x) => ++x);
  };

  const onDeleteJob = (index: number) => {
    setValue(
      "jobs",
      jobs?.filter((_, i) => i !== index)
    );
    setJobCount((x) => --x);
  };

  const onAddSkill = (jobIndex: number) => {
    if (jobs) {
      const newIndex = jobs[jobIndex].skills.length;
      setValue(`jobs.${jobIndex}.skills.${newIndex}.skill`, "");
      setValue(`jobs.${jobIndex}.skills.${newIndex}.optional`, false);
    }
  };

  const onDeleteSkill = (jobIndex: number, skillIndex: number) => {
    if (jobs) {
      setValue(
        `jobs.${jobIndex}.skills`,
        jobs[jobIndex].skills.filter((_, i) => i !== skillIndex)
      );
    }
  };

  return (
    <Box paddingTop="4">
      <Wrap justify="center" paddingBottom="4">
        <form onSubmit={onSubmit}>
          <Card>
            <CardHeader>
              <Heading size="md">Edição de empresa</Heading>
            </CardHeader>
            <CardBody>
              <Grid gap="4" templateColumns="repeat(2, 1fr)">
                <GridItem colSpan={2}>
                  <Flex gap="4" alignItems="end">
                    <Avatar
                      src={imageData}
                      name={queryCompany.data.name}
                      size="2xl"
                    />
                    <Flex direction="column" gap="4">
                      <Text>{image?.name}</Text>
                      <Box>
                        <label>
                          <Button
                            onClick={onSelectImage}
                            variant="outline"
                            colorScheme="cyan"
                          >
                            Selecionar avatar
                          </Button>
                          <input
                            hidden
                            type="file"
                            {...imageField}
                            ref={(e) => {
                              imageRef(e);
                              imageCustomRef.current = e;
                            }}
                          />
                        </label>
                      </Box>
                    </Flex>
                  </Flex>
                </GridItem>
                <GridItem colSpan={2}>
                  <FormControl isInvalid={!!errors.name}>
                    <FormLabel>Nome</FormLabel>
                    <Input placeholder="Nome" {...register("name")} />
                  </FormControl>
                </GridItem>
                <GridItem colSpan={2}>
                  <FormControl isInvalid={!!errors.role}>
                    <FormLabel>Area</FormLabel>
                    <Input placeholder="Cargo" {...register("role")} />
                  </FormControl>
                </GridItem>
                <GridItem colSpan={2}>
                  <FormControl isInvalid={!!errors.description}>
                    <FormLabel>Descrição</FormLabel>
                    <Textarea
                      placeholder="Descrição"
                      {...register("description")}
                    />
                  </FormControl>
                </GridItem>
                <GridItem colSpan={2}>
                  <Tabs colorScheme="cyan">
                    <TabList>
                      <Tab>Vagas</Tab>
                    </TabList>
                    <TabPanels>
                      <TabPanel>
                        <Stack>
                          <Flex justifyContent="end">
                            <Button
                              variant="outline"
                              colorScheme="cyan"
                              onClick={onAddJob}
                            >
                              Nova vaga
                            </Button>
                          </Flex>
                          <Accordion defaultIndex={[jobCount]} allowMultiple>
                            {jobs?.map((_, jobIndex) => (
                              <AccordionItem key={jobIndex}>
                                <h2>
                                  <AccordionButton>
                                    <Box as="span" flex="1" textAlign="left">
                                      {watch(`jobs.${jobIndex}.title`)} -{" "}
                                      {
                                        skillLevelText[
                                          watch(`jobs.${jobIndex}.level`)
                                        ]
                                      }
                                    </Box>
                                    <AccordionIcon />
                                  </AccordionButton>
                                </h2>
                                <AccordionPanel>
                                  <Grid
                                    gap="4"
                                    templateColumns="repeat(2, 1fr)"
                                  >
                                    <GridItem colSpan={2}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.jobs?.[jobIndex]?.title
                                        }
                                      >
                                        <FormLabel>Titulo</FormLabel>
                                        <Input
                                          placeholder="Titulo"
                                          {...register(
                                            `jobs.${jobIndex}.title`
                                          )}
                                        />
                                      </FormControl>
                                    </GridItem>
                                    <GridItem colSpan={1}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.jobs?.[jobIndex]?.level
                                        }
                                        defaultValue={SkillLevel.Junior}
                                      >
                                        <FormLabel>Nível</FormLabel>
                                        <Select
                                          {...register(
                                            `jobs.${jobIndex}.level`
                                          )}
                                        >
                                          {skillLevelOptions.map((x, i) => (
                                            <option key={i} value={x.value}>
                                              {x.label}
                                            </option>
                                          ))}
                                        </Select>
                                      </FormControl>
                                    </GridItem>
                                    <GridItem>
                                      <FormControl
                                        isInvalid={
                                          !!errors.jobs?.[jobIndex]?.value
                                        }
                                      >
                                        <FormLabel>Valor</FormLabel>
                                        <InputGroup>
                                          <InputLeftAddon>R$</InputLeftAddon>
                                          <Controller
                                            name={`jobs.${jobIndex}.value`}
                                            control={control}
                                            render={({
                                              field: { ref, ...restField },
                                            }) => (
                                              <NumberInput
                                                allowMouseWheel
                                                step={1}
                                                precision={0}
                                                min={0}
                                                max={4294967295}
                                                {...restField}
                                              >
                                                <NumberInputField
                                                  borderLeftRadius="0"
                                                  placeholder="Valor"
                                                  ref={ref}
                                                />
                                                <NumberInputStepper>
                                                  <NumberIncrementStepper />
                                                  <NumberDecrementStepper />
                                                </NumberInputStepper>
                                              </NumberInput>
                                            )}
                                          />
                                        </InputGroup>
                                      </FormControl>
                                    </GridItem>
                                    <GridItem colSpan={2}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.jobs?.[jobIndex]?.description
                                        }
                                      >
                                        <FormLabel>Descrição</FormLabel>
                                        <Textarea
                                          placeholder="Descrição"
                                          {...register(
                                            `jobs.${jobIndex}.description`
                                          )}
                                        />
                                      </FormControl>
                                    </GridItem>
                                    <GridItem colSpan={2}>
                                      <Table size="sm">
                                        <Thead>
                                          <Tr>
                                            <Th>Habilidade</Th>
                                            <Th>Opcional</Th>
                                            <Th>Ações</Th>
                                          </Tr>
                                        </Thead>
                                        <Tbody>
                                          {watch(`jobs.${jobIndex}.skills`).map(
                                            (x, skillIndex) => (
                                              <Tr key={skillIndex}>
                                                <Td>
                                                  <Input
                                                    {...register(
                                                      `jobs.${jobIndex}.skills.${skillIndex}.skill`
                                                    )}
                                                    size="sm"
                                                    placeholder="Habilidade"
                                                    isInvalid={
                                                      !!errors.jobs?.[jobIndex]
                                                        ?.skills?.[skillIndex]
                                                        ?.skill
                                                    }
                                                  />
                                                </Td>
                                                <Td>
                                                  <Switch
                                                    {...register(
                                                      `jobs.${jobIndex}.skills.${skillIndex}.optional`
                                                    )}
                                                  />
                                                </Td>
                                                <Td>
                                                  {skillIndex ===
                                                  watch(
                                                    `jobs.${jobIndex}.skills`
                                                  ).length -
                                                    1 ? (
                                                    <IconButton
                                                      size="sm"
                                                      aria-label="Add"
                                                      colorScheme="green"
                                                      icon={<AddIcon />}
                                                      onClick={() =>
                                                        onAddSkill(jobIndex)
                                                      }
                                                      variant="outline"
                                                    />
                                                  ) : (
                                                    <IconButton
                                                      size="sm"
                                                      aria-label="Delete"
                                                      colorScheme="red"
                                                      icon={<DeleteIcon />}
                                                      onClick={() =>
                                                        onDeleteSkill(
                                                          jobIndex,
                                                          skillIndex
                                                        )
                                                      }
                                                      variant="outline"
                                                    />
                                                  )}
                                                </Td>
                                              </Tr>
                                            )
                                          )}
                                        </Tbody>
                                      </Table>
                                    </GridItem>
                                    <GridItem colSpan={2}>
                                      <Button
                                        colorScheme="red"
                                        variant="outline"
                                        onClick={() => onDeleteJob(jobIndex)}
                                      >
                                        Remover
                                      </Button>
                                    </GridItem>
                                  </Grid>
                                </AccordionPanel>
                              </AccordionItem>
                            ))}
                          </Accordion>
                        </Stack>
                      </TabPanel>
                    </TabPanels>
                  </Tabs>
                </GridItem>
              </Grid>
            </CardBody>
            <CardFooter justifyContent="end">
              <Button
                type="submit"
                isLoading={isSubmitting}
                colorScheme="cyan"
                color="white"
              >
                Salvar
              </Button>
            </CardFooter>
          </Card>
        </form>
      </Wrap>
    </Box>
  );
};

export default EditCompanyPage;
