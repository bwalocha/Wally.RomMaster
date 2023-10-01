import {configureStore} from "@reduxjs/toolkit";
import {api as fileApi} from "@/store/fileApi";

export const store = configureStore({
    reducer: {
        [fileApi.reducerPath]: fileApi.reducer,
    },
    middleware(getDefaultMiddleware) {
        return getDefaultMiddleware()
            .concat(fileApi.middleware)
            ;
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
