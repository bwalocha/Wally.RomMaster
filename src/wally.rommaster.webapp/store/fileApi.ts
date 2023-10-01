import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react"
import buildQuery from 'odata-query'
import {
    PagedResponse
} from "wally.rommaster.fileservice.application.contracts/Responses/Abstractions/PagedResponse";
import {
    GetFilesResponse
} from "wally.rommaster.fileservice.application.contracts/Files/Responses/GetFilesResponse";
import {
    GetFilesRequest
} from "wally.rommaster.fileservice.application.contracts/Files/Requests/GetFilesRequest";
import {
    GetPathsResponse
} from "wally.rommaster.fileservice.application.contracts/Paths/Responses/GetPathsResponse";
import {
    GetPathsRequest
} from "wally.rommaster.fileservice.application.contracts/Paths/Requests/GetPathsRequest";

export const api = createApi({
    reducerPath: "fileApi",
    baseQuery: fetchBaseQuery({
        baseUrl: `${process.env.NEXT_PUBLIC_API_BASE_URL}/FileService-api`,
    }),
    tagTypes: ["files", "paths"],
    endpoints: (builder) => ({
        getPaths: builder.query<PagedResponse<GetPathsResponse>, GetPathsRequest | void>({
            query: (request) => {
                const query = request ? buildQuery({
                    orderBy: 'Name asc',
                    filter: [`startswith(Name, '${request.name}')`]
                }) : buildQuery({
                    orderBy: 'Name asc',
                })
                
                return `/paths${query}`
            },
            providesTags: (result, error, _) => [{
                type: "paths"
            }],
        }),
        getFiles: builder.query<PagedResponse<GetFilesResponse>, { pathId: string, request: GetFilesRequest | void }>({
            query: ({pathId, request}) => {
                const query = request ? buildQuery({
                    orderBy: 'Location asc',
                    // filter: [`startswith(Location, '${request.name}')`]
                }) : buildQuery({
                    orderBy: 'Location asc',
                })

                return `/paths/${pathId}/files${query}`
            },
            providesTags: (result, error, _) => [{
                type: "files"
            }],
        }),
    }),
});

export const {
    useGetPathsQuery,
    useGetFilesQuery,
} = api;
