"use client";

import { useConfig } from "@/hooks/config";
import {
  Accordion,
  AccordionButton,
  AccordionIcon,
  AccordionItem,
  AccordionPanel,
  Avatar,
  Badge,
  Box,
  Button,
  Card,
  CardBody,
  CardFooter,
  CardHeader,
  Divider,
  Flex,
  FormControl,
  FormLabel,
  Grid,
  GridItem,
  Heading,
  Icon,
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
  Tab,
  TabList,
  TabPanel,
  TabPanels,
  Table,
  TableContainer,
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
import { NextPage } from "next";
import { KeyboardEvent, useEffect, useMemo, useRef, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { AddIcon, DeleteIcon, SmallCloseIcon } from "@chakra-ui/icons";
import {
  UserSkillCreate,
  SkillLevel,
  UserWorkCreate,
  skillLevelOptions,
} from "@/core/models/user";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { userApi } from "@/core/api/user";
import { userSkillApi } from "@/core/api/userSkill";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { imageApi } from "@/core/api/image";
import BadRequestError from "@/core/errors/badRequest";
import { workApi } from "@/core/api/work";

interface EditUserPageParams {
  id: number;
}

interface EditUserPageProps {
  params: EditUserPageParams;
}

interface EditUserFormData {
  name?: string;
  role?: string;
  description?: string;
  expectedValue?: number;
  imageFile?: FileList;
  skills?: UserSkillCreate[];
  works?: UserWorkCreate[];
  newWorkSkill?: string[];
}

const schemaEditUser = yup.object<EditUserFormData>({
  name: yup.string().notRequired().max(100).trim(),
  role: yup.string().notRequired().max(100).trim(),
  description: yup.string().notRequired().max(255).trim(),
  expectedValue: yup.number().notRequired().min(1).max(4294967295),
  imageFile: yup.mixed().notRequired(),
  skills: yup
    .array()
    .transform((x) => x.slice(0, -1))
    .of(
      yup.object<UserSkillCreate>({
        id: yup.number().required(),
        level: yup.number().required(),
        years: yup.number().required().max(4294967295),
        skill: yup.string().required().max(30),
      })
    ),
  works: yup.array().of(
    yup.object<UserWorkCreate>({
      id: yup.number().required(),
      title: yup.string().required().max(100).trim(),
      description: yup.string().required().max(255),
      startAt: yup.date().required(),
      endAt: yup
        .string()
        .transform((x) => (x === "" ? null : x))
        .notRequired(),
      value: yup.number().required(),
      skills: yup.array().of(yup.string().max(30)),
    })
  ),
  newWorkSkill: yup.array(yup.string().notRequired()),
});

const EditUserPage: NextPage<EditUserPageProps> = ({ params }) => {
  const queryUser = useQuery(["user", params.id], () => userApi.get(params.id));
  const [userLoading, setUserLoading] = useState(false);
  const {
    register,
    handleSubmit,
    control,
    watch,
    setValue,
    getValues,
    formState: { isSubmitting, errors },
  } = useForm<EditUserFormData>({
    resolver: yupResolver(schemaEditUser),
  });
  const toast = useToast();
  const queryClient = useQueryClient();
  const [skillCount, setSkillCount] = useState(0);
  const [workCount, setWorkCount] = useState(0);

  useEffect(() => {
    if (queryUser.data && !userLoading) {
      var data = queryUser.data;
      setValue("description", data.description);
      setValue("expectedValue", data.expectedValue);
      setValue("name", data.name);
      setValue("role", data.role);
      setValue("skills", data.skills);
      setValue(
        "works",
        data.works?.map((x) => ({
          ...x,
          startAt: x.startAt.substring(0, 10),
          endAt: x.endAt?.substring(0, 10),
        }))
      );

      if (data.skills && data.skills.length > 0) {
        setSkillCount(data.skills.length);
      }

      if (data.works && data.works.length > 0) {
        setWorkCount(data.works.length);
      }

      if (data.skills) {
        setValue(`skills.${data.skills.length}.id`, 0);
        setValue(`skills.${data.skills.length}.level`, SkillLevel.Junior);
        setValue(`skills.${data.skills.length}.years`, 1);
      }

      setUserLoading(true);
    }
  }, [queryUser, setValue, skillCount, userLoading]);

  const onSubmit = handleSubmit(
    async ({ imageFile, skills, works, ...values }) => {
      if (!queryUser.data) return;

      try {
        const image = imageFile?.item(0);
        let imageId = queryUser.data.imageId;

        if (image) {
          imageId = await imageApi.post(image);
        }

        await userApi.update(params.id, {
          ...values,
          imageId,
        });

        if (image && queryUser.data.imageId)
          await imageApi.delete(queryUser.data.imageId);

        if (skills) {
          for (let i = 0; i < skills.length; i++) {
            const skill = skills[i];
            await userSkillApi.post(skill);
          }
        }

        if (works) {
          for (let i = 0; i < works.length; i++) {
            const work = works[i];
            await workApi.post(work);
          }

          const removeWorks = queryUser.data.works?.filter(
            (x) => !works.find((y) => y.id === x.id)
          );

          if (removeWorks) {
            for (let i = 0; i < removeWorks.length; i++) {
              const work = removeWorks[i];

              await workApi.delete(work.id);
            }
          }
        }

        queryClient.invalidateQueries(["@me"]);
        queryClient.invalidateQueries(["user", params.id]);

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
    }
  );
  const { apiUrl } = useConfig();
  const { ref: imageRef, ...imageField } = register("imageFile");
  const imageCustomRef = useRef<HTMLInputElement | null>(null);

  const image = watch("imageFile")?.item(0);
  const imageData = useMemo(() => {
    return image
      ? URL.createObjectURL(image)
      : queryUser.data?.imageId &&
          `${apiUrl}/api/images/${queryUser.data?.imageId}`;
  }, [apiUrl, image, queryUser.data?.imageId]);

  if (!queryUser.data) {
    return <></>;
  }

  const skills = watch("skills");
  const works = watch("works");
  const newWorkSkill = watch("newWorkSkill");

  const onSelectImage = () => {
    imageCustomRef.current?.click();
  };

  const onAddSkill = () => {
    setSkillCount((x) => ++x);
    setValue(`skills.${skillCount + 1}.level`, SkillLevel.Junior);
    setValue(`skills.${skillCount + 1}.years`, 1);
  };

  const onDeleteSkill = (index: number) => {
    setValue(
      "skills",
      skills?.filter((_, i) => i !== index)
    );
    setSkillCount((x) => --x);
  };

  const onAddWork = () => {
    setValue(`works.${workCount}.id`, 0);
    setValue(`works.${workCount}.skills`, []);
    setValue(`works.${workCount}.endAt`, undefined);
    setWorkCount((x) => ++x);
  };

  const onDeleteWork = (index: number) => {
    setValue(
      "works",
      works?.filter((_, i) => i !== index)
    );
    setWorkCount((x) => --x);
  };

  const onAddWorkSkill = (
    e: KeyboardEvent<HTMLInputElement>,
    workIndex: number
  ) => {
    if (e.key !== "Enter") return;

    e.preventDefault();

    const workSkills = getValues(`works.${workIndex}.skills`);

    if (newWorkSkill && newWorkSkill[workIndex]) {
      if (workSkills.includes(newWorkSkill[workIndex].toUpperCase())) return;
      setValue(
        `works.${workIndex}.skills.${workSkills.length}`,
        newWorkSkill[workIndex].toUpperCase()
      );
    }
  };

  const onDeleteWorkSkill = (workIndex: number, index: number) => {
    const workSkills = getValues(`works.${workIndex}.skills`);
    setValue(
      `works.${workIndex}.skills`,
      workSkills.filter((_, i) => i !== index)
    );
  };

  return (
    <Box paddingTop="4">
      <Wrap justify="center" paddingBottom="4">
        <form onSubmit={onSubmit}>
          <Card>
            <CardHeader>
              <Heading size="md">Edição de usuário</Heading>
            </CardHeader>
            <CardBody>
              <Grid gap="4" templateColumns="repeat(2, 1fr)">
                <GridItem colSpan={2}>
                  <Flex gap="4" alignItems="end">
                    <Avatar
                      src={imageData}
                      name={queryUser.data.name}
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
                <GridItem colSpan={1}>
                  <FormControl isInvalid={!!errors.role}>
                    <FormLabel>Cargo</FormLabel>
                    <Input placeholder="Cargo" {...register("role")} />
                  </FormControl>
                </GridItem>
                <GridItem colSpan={1}>
                  <FormControl isInvalid={!!errors.expectedValue}>
                    <FormLabel>Valor esperado</FormLabel>
                    <InputGroup>
                      <InputLeftAddon>R$</InputLeftAddon>
                      <Controller
                        name="expectedValue"
                        control={control}
                        render={({ field: { ref, value, ...restField } }) => (
                          <NumberInput
                            allowMouseWheel
                            step={1}
                            precision={0}
                            min={0}
                            max={4294967295}
                            {...restField}
                            value={value ?? 0}
                          >
                            <NumberInputField
                              placeholder="Valor esperado"
                              borderLeftRadius="0"
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
                      <Tab>Habilidades</Tab>
                      <Tab>Trabalhos</Tab>
                    </TabList>
                    <TabPanels>
                      <TabPanel>
                        <TableContainer>
                          <Table size="sm">
                            <Thead>
                              <Tr>
                                <Th>Habilidade</Th>
                                <Th>Nível</Th>
                                <Th>Anos</Th>
                                <Th>Ações</Th>
                              </Tr>
                            </Thead>
                            <Tbody>
                              {skills?.map((_, i) => (
                                <Tr key={i}>
                                  <Td>
                                    <Input
                                      {...register(`skills.${i}.skill`)}
                                      size="sm"
                                      placeholder="Habilidade"
                                      isInvalid={!!errors.skills?.[i]?.skill}
                                    />
                                  </Td>
                                  <Td>
                                    <Select
                                      {...register(`skills.${i}.level`)}
                                      size="sm"
                                      width="32"
                                      isInvalid={!!errors.skills?.[i]?.level}
                                      defaultValue={SkillLevel.Junior}
                                    >
                                      {skillLevelOptions.map((x, i) => (
                                        <option key={i} value={x.value}>
                                          {x.label}
                                        </option>
                                      ))}
                                    </Select>
                                  </Td>
                                  <Td>
                                    <Controller
                                      name={`skills.${i}.years`}
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
                                          size="sm"
                                          minWidth="32"
                                          {...restField}
                                        >
                                          <NumberInputField
                                            placeholder="Anos"
                                            ref={ref}
                                          />
                                          <NumberInputStepper>
                                            <NumberIncrementStepper />
                                            <NumberDecrementStepper />
                                          </NumberInputStepper>
                                        </NumberInput>
                                      )}
                                    />
                                  </Td>
                                  <Td>
                                    {i === skillCount ? (
                                      <IconButton
                                        size="sm"
                                        aria-label="Add"
                                        colorScheme="green"
                                        icon={<AddIcon />}
                                        onClick={onAddSkill}
                                        variant="outline"
                                      />
                                    ) : (
                                      <IconButton
                                        size="sm"
                                        aria-label="Delete"
                                        colorScheme="red"
                                        icon={<DeleteIcon />}
                                        onClick={() => onDeleteSkill(i)}
                                        variant="outline"
                                      />
                                    )}
                                  </Td>
                                </Tr>
                              ))}
                            </Tbody>
                          </Table>
                        </TableContainer>
                      </TabPanel>
                      <TabPanel>
                        <Stack>
                          <Flex justifyContent="end">
                            <Button
                              variant="outline"
                              colorScheme="cyan"
                              onClick={onAddWork}
                            >
                              Novo trabalho
                            </Button>
                          </Flex>
                          <Accordion defaultIndex={[workCount]} allowMultiple>
                            {works?.map((_, workIndex) => (
                              <AccordionItem key={workIndex}>
                                <h2>
                                  <AccordionButton>
                                    <Box as="span" flex="1" textAlign="left">
                                      {watch(`works.${workIndex}.title`)}
                                    </Box>
                                    <AccordionIcon />
                                  </AccordionButton>
                                </h2>
                                <AccordionPanel pb="4">
                                  <Grid
                                    gap="4"
                                    templateColumns="repeat(2, 1fr)"
                                  >
                                    <GridItem colSpan={1}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.works?.[workIndex]?.title
                                        }
                                      >
                                        <FormLabel>Titulo</FormLabel>
                                        <Input
                                          placeholder="Titulo"
                                          {...register(
                                            `works.${workIndex}.title`
                                          )}
                                        />
                                      </FormControl>
                                    </GridItem>
                                    <GridItem>
                                      <FormControl
                                        isInvalid={
                                          !!errors.works?.[workIndex]?.value
                                        }
                                      >
                                        <FormLabel>Valor</FormLabel>
                                        <InputGroup>
                                          <InputLeftAddon>R$</InputLeftAddon>
                                          <Controller
                                            name={`works.${workIndex}.value`}
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
                                    <GridItem colSpan={1}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.works?.[workIndex]?.startAt
                                        }
                                      >
                                        <FormLabel>Inicio</FormLabel>
                                        <Input
                                          type="date"
                                          {...register(
                                            `works.${workIndex}.startAt`
                                          )}
                                        />
                                      </FormControl>
                                    </GridItem>
                                    <GridItem colSpan={1}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.works?.[workIndex]?.endAt
                                        }
                                      >
                                        <FormLabel>Fim</FormLabel>
                                        <Input
                                          type="date"
                                          {...register(
                                            `works.${workIndex}.endAt`
                                          )}
                                        />
                                      </FormControl>
                                    </GridItem>
                                    <GridItem colSpan={2}>
                                      <FormControl
                                        isInvalid={
                                          !!errors.works?.[workIndex]
                                            ?.description
                                        }
                                      >
                                        <FormLabel>Descrição</FormLabel>
                                        <Textarea
                                          placeholder="Descrição"
                                          {...register(
                                            `works.${workIndex}.description`
                                          )}
                                        />
                                      </FormControl>
                                    </GridItem>
                                    <GridItem colSpan={2}>
                                      <Flex flex="6" direction="column" gap="2">
                                        <Text>Habilidades</Text>
                                        <Flex
                                          borderWidth="1px"
                                          borderRadius="lg"
                                          p="2"
                                          gap="2"
                                          wrap="wrap"
                                          minHeight="10"
                                        >
                                          {watch(
                                            `works.${workIndex}.skills`
                                          )?.map((x, skillIndex) => (
                                            <Badge
                                              key={skillIndex}
                                              borderRadius="4"
                                            >
                                              <IconButton
                                                aria-label="Close"
                                                size="1"
                                                icon={<SmallCloseIcon />}
                                                onClick={() =>
                                                  onDeleteWorkSkill(
                                                    workIndex,
                                                    skillIndex
                                                  )
                                                }
                                              />
                                              {x}
                                            </Badge>
                                          ))}
                                          <Input
                                            placeholder="Nova habilidade"
                                            variant="unstyled"
                                            {...register(
                                              `newWorkSkill.${workIndex}`
                                            )}
                                            display="inline"
                                            width="25"
                                            size="sm"
                                            onKeyDown={(e) =>
                                              onAddWorkSkill(e, workIndex)
                                            }
                                          />
                                        </Flex>
                                      </Flex>
                                    </GridItem>
                                    <GridItem colSpan={2}>
                                      <Button
                                        colorScheme="red"
                                        variant="outline"
                                        onClick={() => onDeleteWork(workIndex)}
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

export default EditUserPage;
