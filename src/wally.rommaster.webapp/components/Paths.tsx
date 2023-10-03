"use client"
import React, {useState} from "react";
import {useGetPathsQuery} from "@/store/fileApi";

type Params = {
    pathId?: string
    pathName: string
}
const Paths = ({pathId, pathName}: Params) => {
    const {data, error, isLoading} = useGetPathsQuery({ pathId: pathId, request: undefined })
    const [isOpen, setIsOpen] = useState(false)

    const children = (isLoading || error) ? (<h1>loading...</h1>) : (<>{data!.items.map(a =>
            isOpen && (<Paths pathId={a.id} pathName={a.name} />)
        )}</>)
    
    return (
        <>
            <button onClick={() => setIsOpen(true)}>{pathName || "root"}</button>
            <div style={{ marginLeft: '0.8rem' }}>
                {children}
            </div>
        </>
    )
}

export default Paths;
