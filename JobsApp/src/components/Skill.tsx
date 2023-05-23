import { Badge, Box, Progress, Stack, Text } from "@chakra-ui/react";

export interface SkillProps {
  text: string;
  level: string;
  years: number;
  maxYears: number;
}

const Skill: React.FC<SkillProps> = (props: SkillProps) => {
  return (
    <Stack>
      <Stack direction="row" alignItems="center">
        <Text>{props.text}</Text>
        <Box>
          <Badge colorScheme="blue" borderRadius="4">
            {props.level}
          </Badge>
        </Box>
        <Text>
          {props.years} Ano{props.years > 1 && "s"}
        </Text>
      </Stack>
      <Progress
        borderRadius="4"
        colorScheme="cyan"
        value={props.years}
        max={props.maxYears}
      />
    </Stack>
  );
};

export default Skill;
