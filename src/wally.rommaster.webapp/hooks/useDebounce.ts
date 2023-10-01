import {useEffect, useState} from "react";

const useDebounce = (callback: (input: any) => void, delay: number) => {
    const [value, setValue] = useState();

    useEffect(() => {
        if (value === undefined) { // skip initial useEffect
            return
        }

        const timeoutId = setTimeout(() => {
            console.log('Send to server: ', value)
            callback(value)
        }, delay)

        return () => clearTimeout(timeoutId)
    }, [value])
    
    return setValue; 
};

export default useDebounce
