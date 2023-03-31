import styles from "@/components/tree.module.scss";
import {useEffect, useState} from "react";
// import PropTypes from 'prop-types'
import {PagedResponse} from "./../models/generated/Abstractions/PagedResponse";
import {GetPathsResponse} from "./../models/generated/Contracts/GetPathsResponse";

export default function Tree({ model, isLoading, fetchPaths } : { model: GetPathsResponse[], isLoading: boolean, fetchPaths: (parentId: string) => Promise<PagedResponse<any>>}) {
    // const [isLoading, setIsLoading] = useState(true)
    const [viewModel, setViewModel] = useState(model)

    const handleClick = async (path: any) => {
        // setIsLoading(true)
        const data = await fetchPaths(path.id)
        path.children = data?.items
        //viewModel = path.children
        setViewModel(path.children ?? [])
        // setIsLoading(false)
    }

    return (
        <>
            {isLoading ? '...' : ''}
            {model && (
            <ul>
            {
                model.map((path: any, index: number) => (
                    <li className={styles.path} key={path.id}>
                        {!path.children && <a onClick={() => handleClick(path)}>[expand]</a>}                        
                        <a onClick={() => !path.children && handleClick(path)}>{path.name}</a>
                        <Tree model={path.children} isLoading={false} fetchPaths={fetchPaths} />
                    </li>
                ))
            }
            </ul>)}
        </>
    )
}

/*Tree.propTypes = {
    model: PropTypes.instanceOf(PagedResponse<GetPathsResponse>)
}*/
