"use client"
import React, {useState} from "react";
import {useGetPathsQuery} from "@/store/fileApi";
import {Checkbox} from "@fluentui/react-checkbox";
import {Link} from "@fluentui/react-link";
import useGlobalStyles from "@/hooks/useGlobalStyles";

type Params = {
    pathId?: string
    pathName: string
}
const Paths = ({pathId, pathName}: Params) => {
    const styles = useGlobalStyles();
    const [isOpen, setIsOpen] = useState(false)
    const {data, error, isLoading} = useGetPathsQuery({ pathId: pathId, request: undefined }, { skip: isOpen !== true})
    const children = () => {
        if (error) {
            return <h1>error!</h1>
        }
        
        if (isOpen) {
            return data?.items.map(a =>
                <Paths key={a.id} pathId={a.id} pathName={a.name}/>
            )
        }
    }
    
    return (
        <div className={styles.flexColumn}>
            <div className={styles.flexRow}>
                {isLoading ? <Checkbox checked={"mixed"} /> : <Checkbox checked={isOpen} />}
                <Link onClick={() => setIsOpen(() => !isOpen)}>{pathName || "root"}</Link>
            </div>
            <div className={styles.flexColumn} style={{ marginLeft: '0.8rem' }}>
                {children()}
            </div>
        </div>
    )
}

export default Paths;
