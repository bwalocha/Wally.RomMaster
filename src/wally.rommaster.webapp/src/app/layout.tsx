import '@/assets/globals.scss'
import Header from "@/components/header";
import Menu from "@/components/menu";
import Footer from "@/components/footer";

export const metadata = {
    title: 'Wally.RomMaster',
    description: 'by Wally',
}

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
              <Header />
              {/*<main style={styles}>*/}
              <main>
                  <Menu />
                  <section style={{ width: "1024px" }}>{children}</section>
              </main>
              <Footer />
          </body>
      </html>
  )
}
