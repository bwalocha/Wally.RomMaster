"use client";
import { Inter } from 'next/font/google'
import styles from './page.module.scss'
import Tree from "@/components/tree";
import { useEffect, useState } from "react";
import {GetPathsResponse} from "@/models/generated/Contracts/GetPathsResponse";
import {PagedResponse} from "@/models/generated/Abstractions/PagedResponse";
import {DefaultButton, IStackTokens, PrimaryButton, Stack} from "@fluentui/react";

const inter = Inter({ subsets: ['latin'] })

const Home = () => {
    const [paths, setPaths] = useState({ items: [], pageInfo: { index: 0, size: 0, totalItems: 0}} as PagedResponse<GetPathsResponse>)
    const [isLoading, setIsLoading] = useState(true)
    const fetchPaths = async (parentId?: string): Promise<PagedResponse<GetPathsResponse>> => {
        setIsLoading(true)
        try {
            const response = await fetch(`https://localhost:5001/FileService-api/paths/${parentId ?? ''}`)
            if (response.status == 200) {
                const data = await response.json()
                
                return data as PagedResponse<GetPathsResponse>
            }
        }
        catch(error) {
            console.error(error);
            setPaths({ items: [], pageInfo: { index: 0, size: 0, totalItems: 0}})
        }
        finally {
            setIsLoading(false)
        }

        return {} as PagedResponse<GetPathsResponse>
    }
        
    useEffect(() =>
    {
        (async () => {
            const data = await fetchPaths();
            setPaths(data);
        })();
        
        return () => {
            // on unmount
        }
    }, [])

    function _alertClicked(): void {
        alert('Clicked');
    }

    const stackTokens: IStackTokens = { childrenGap: 40 };

    return (
      <div className={styles.center}>
          <Stack horizontal tokens={stackTokens}>
              <DefaultButton text="Standard" onClick={_alertClicked} allowDisabledFocus disabled={false} checked={false} />
              <PrimaryButton text="Primary" onClick={_alertClicked} allowDisabledFocus disabled={false} checked={false} />
          </Stack>
         <Tree model={paths.items} isLoading={isLoading} fetchPaths={fetchPaths}></Tree>
      </div>
  )
}

export default Home