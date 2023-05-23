import { Inter } from "next/font/google";
import Provider from "./providers";
import Navbar from "@/components/Navbar";

import "@/assets/css/global.css";
import BasePage from "@/components/BasePage";

const inter = Inter({ subsets: ["latin"] });

export const metadata = {
  title: "Jobs",
  description: "A job center",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <Provider>
          <Navbar />
          <BasePage>{children}</BasePage>
        </Provider>
      </body>
    </html>
  );
}
