"use client"
import {NextPage} from "next";
import {useGetPathsQuery} from "@/store/fileApi";
import React from "react";
import Link from "next/link";

const Page: NextPage = () => {
    const {data, error, isLoading} = useGetPathsQuery()

    if (isLoading || error) {
        return <h1>loading...</h1>
    }
    
  return (
      <>
        <h1>Paths</h1>

          {data!.items.map(a =>
              <Link href={`/${a.name}`} className={'bolt-link'} key={a.id}>
                  {a.id} {a.name}
              </Link>
          )}
          
      </>
  )
}

export default Page;
