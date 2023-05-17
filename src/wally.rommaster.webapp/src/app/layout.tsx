"use client"
import '@/assets/globals.scss'
import Header from "@/components/header";
import Menu from "@/components/menu";
import Footer from "@/components/footer";
import { FluentProvider, teamsLightTheme } from '@fluentui/react-components';

/*export const metadata = {
    title: 'Wally.RomMaster',
    description: 'by Wally',
}*/

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
// styles the main html tag
    const styles = {
        display: "flex",
        flexDirection: "row"
    };

  return (
      <html lang="en">
          <body>
          <FluentProvider theme={teamsLightTheme}>
              <Header />
              <main>
                  <aside>
                      <Menu />
                  </aside>
                  <section style={{ width: "1024px" }}>{children}</section>
              </main>
              <Footer />
          </FluentProvider>,
          </body>
      </html>
  )
}
