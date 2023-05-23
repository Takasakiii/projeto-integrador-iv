"use client";

import { UserType } from "@/core/models/user";
import { useAuth } from "@/hooks/auth";
import { useConfig } from "@/hooks/config";
import { Link } from "@chakra-ui/next-js";
import {
  Avatar,
  Box,
  Button,
  Flex,
  Menu,
  MenuButton,
  MenuDivider,
  MenuItem,
  MenuList,
  Stack,
  Text,
} from "@chakra-ui/react";

const Navbar: React.FC = () => {
  const { user, logoff } = useAuth();
  const { apiUrl } = useConfig();

  return (
    <Box>
      <Flex
        bg="cyan.600"
        justifyContent="space-between"
        alignItems="center"
        height="14"
        paddingX="3"
      >
        <Flex gap="2" alignItems="center">
          <Link href="/" color="white">
            <Text as="b" fontSize="xl">
              Jobs
            </Text>
          </Link>
          <Stack direction="row">
            <Link href="/users" color="white">
              Usuários
            </Link>
            <Link href="/companies" color="white">
              Empresas
            </Link>
            <Link href="/statistics" color="white">
              Estatísticas
            </Link>
          </Stack>
        </Flex>
        <Flex gap="2">
          {!user ? (
            <>
              <Link href="/auth/register">
                <Button>Register</Button>
              </Link>
              <Link href="/auth/login">
                <Button>Login</Button>
              </Link>
            </>
          ) : (
            <Menu>
              <MenuButton>
                <Avatar
                  size="sm"
                  name={user.name}
                  src={user.imageId && `${apiUrl}/api/images/${user.imageId}`}
                />
              </MenuButton>
              <MenuList>
                {user.type === UserType.Professional ? (
                  <>
                    <MenuItem as={Link} href={`/users/${user.id}`}>
                      Perfil
                    </MenuItem>
                    <MenuItem as={Link} href={`/users/${user.id}/edit`}>
                      Editar perfil
                    </MenuItem>
                  </>
                ) : (
                  <>
                    <MenuItem as={Link} href={`/companies/${user.id}`}>
                      Perfil
                    </MenuItem>
                    <MenuItem as={Link} href={`/companies/${user.id}/edit`}>
                      Editar perfil
                    </MenuItem>
                  </>
                )}
                <MenuDivider />
                <MenuItem onClick={logoff}>Logoff</MenuItem>
              </MenuList>
            </Menu>
          )}
        </Flex>
      </Flex>
    </Box>
  );
};

export default Navbar;
