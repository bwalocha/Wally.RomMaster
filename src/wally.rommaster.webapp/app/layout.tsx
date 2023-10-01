"use client"
import './globals.css'
import { Inter } from 'next/font/google'
import {FluentProvider, makeStyles, mergeClasses, ProgressBar, webLightTheme} from '@fluentui/react-components';
import Header from "@/components/Header";
import Providers from "@/components/Provider";
import useGlobalStyles from "@/hooks/useGlobalStyles";

const inter = Inter({ subsets: ['latin'] })

const theme = {
    ...webLightTheme,
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
    const styles = useGlobalStyles()

    return (
        <html lang="en">
        <body className={inter.className}>
        <Providers>
            <FluentProvider theme={theme}>
                <div className={mergeClasses(styles.heightMax, styles.flexColumn)}>
                    <Header/>
                    {children}
                </div>
            </FluentProvider>
        </Providers>
        </body>
        </html>
    )
}
