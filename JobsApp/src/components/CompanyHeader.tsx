import { User } from "@/core/models/user";
import { currencyFormat } from "@/core/utils/format";
import { useConfig } from "@/hooks/config";
import {
  Avatar,
  Flex,
  Heading,
  Stack,
  Stat,
  StatLabel,
  StatNumber,
  Text,
} from "@chakra-ui/react";

export interface UserHeaderProps {
  user: User;
}

const CompanyHeader: React.FC<UserHeaderProps> = ({
  user,
}: UserHeaderProps) => {
  const { apiUrl } = useConfig();

  return (
    <Flex gap="4" alignItems="end">
      <Avatar
        size="2xl"
        name={user.name}
        src={user.imageId && `${apiUrl}/api/images/${user.imageId}`}
        bg="gray.100"
        color="black"
      />
      <Stack>
        <Heading>{user.name}</Heading>
        <Text>{user.role === null || user.role === "" ? "-" : user.role}</Text>
      </Stack>
    </Flex>
  );
};

export default CompanyHeader;
