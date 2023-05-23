"use client";

import { Box } from "@chakra-ui/react";

const BasePage: React.FC<React.PropsWithChildren> = ({ children }) => {
  return (
    <Box bg="gray.50" minHeight="calc(100vh - 56px)">
      {children}
    </Box>
  );
};

export default BasePage;
