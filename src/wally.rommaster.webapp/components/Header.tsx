"use client"
import {
    Avatar,
    Divider,
    makeStyles, mergeClasses, Persona,
    Popover,
    PopoverSurface,
    PopoverTrigger,
    ProgressBar,
    Text,
} from "@fluentui/react-components";
import {XboxController32Regular} from '@fluentui/react-icons';
import Link from "next/link";
// import {signIn, signOut, useSession} from "next-auth/react"
// import Search from "@/components/Search";
import React, {useEffect, useState} from "react";
// import gravatar from "gravatar";
// import {useSelector} from "react-redux";
// import {QueryStatus} from "@reduxjs/toolkit/query";
// import {RootState, store} from "@/store";
import {Alert} from "@fluentui/react-components/unstable";
import useGlobalStyles from "@/hooks/useGlobalStyles";

const Header = () => {
    // const {data: session, status, update} = useSession()
    const [gravatarUrl, setGravatarUrl] = useState<string | null>(null);

    /*useEffect(() => {
        if (status == "authenticated") {
            setGravatarUrl(gravatar.url(session.user!.email!));
        } else {
            setGravatarUrl(null);
        }
    }, [session, status])*/

    const styles = useGlobalStyles();
    /*const [isLoading, errors] = useSelector<RootState, [boolean, any]>((state: RootState) => {
        const data = Object.values({
            ...state.organizationApi.queries,
            ...state.organizationApi.mutations,
            ...state.projectApi.queries,
            ...state.projectApi.mutations,
            ...state.userApi.queries,
            ...state.userApi.mutations,
            ...state.timesheetApi.queries,
            ...state.timesheetApi.mutations,
        })
        const isLoading = data.some((a: any) => a.status === QueryStatus.pending)
        const errors = data.filter(a => a?.error).map(a => a?.error)
        return [isLoading, errors]
    })*/

    return (
        <>
            <header className={mergeClasses(styles.flexRow, styles.header)}>
                <div className={mergeClasses(styles.flexRow, styles.flexCenter, styles.paddingHorizontal, styles.flexGrow)}>
                    <Link className={mergeClasses(styles.flexRow, styles.flexCenter, styles.logo)} href='/'>
                        <XboxController32Regular/>
                        <span>Wally.RomMaster</span>
                    </Link>
                    <div className={mergeClasses(styles.flexGrow)}></div>
                    {/*<Search/>*/}
                    {/*<Popover positioning="below-end">
                        <PopoverTrigger disableButtonEnhancement>
                            {(status === "authenticated") &&
                                <Avatar
                                    name={session?.user?.name ?? undefined}
                                    image={{
                                        src: gravatarUrl || undefined,
                                    }}
                                /> || <Avatar onClick={() => signIn()}/>}
                        </PopoverTrigger>
                        <PopoverSurface hidden={status !== "authenticated"}>
                            <div className={mergeClasses(styles.flex, styles.login)}>
                                <div className={mergeClasses(styles.flexColumn)}>
                                    
                                        <div className={mergeClasses(styles.flexRow, styles.flexRowReverse, styles.flexGrow)}>
                                            <a href="#" onClick={() => signOut({redirect: true})}>Sign out</a>
                                        </div>
                                            <Persona className={styles.paddingVertical} name={session?.user.name ?? session?.user.id} secondaryText={session?.user.email} size={"extra-large"} avatar={{image: {
                                                src: gravatarUrl || undefined,
                                            }}} /> 
                                    <Divider className={mergeClasses(styles.flexGrow)}/>
                                    <div className={mergeClasses(styles.flexRow, styles.flexJustifyContentCenter)}>
                                        <Text size={100}>v.{process.env.NEXT_PUBLIC_VERSION}</Text>
                                    </div>
                                </div>
                            </div>
                        </PopoverSurface>
                    </Popover>*/}
                </div>
            </header>

            {/*[
                {
                    "status":409,
                    "data":{
                        "errors":{},
                        "title":"One or more validation errors occurred.",
                        "status":409,
                        "detail":"Please refer to the errors property for additional details.","instance":"/organizations"
                    }
                }
                ]*/}

            {/*<a>{JSON.stringify(errors)}</a>*/}
            {/*<ProgressBar value={isLoading ? undefined : 0}/>
            {errors.map((a: any, index: number) =>
                <Alert key={index} intent="error" action="Dismiss">
                    {index}. [{a.status}] {a.error} ({JSON.stringify(a.data)})
                </Alert>)}*/}
        </>
    )
}

export default Header;
