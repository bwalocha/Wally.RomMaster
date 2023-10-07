import {makeStyles} from "@fluentui/react-components";

const useGlobalStyles = makeStyles({
    flex: {
        display: "flex",
        // justifyContent: "space-between",
        // width: "100%",
        // height: "100%",
        // flexWrap: "wrap"
    },
    flexRow: {
        display: "flex",
        flexDirection: "row",
        // width: "100%",
        // rowGap: "15px",
        // columnGap: "15px",
    },
    flexColumn: {
        display: "flex",
        flexDirection: "column",
        // height: "100%",
    },
    flexCell: {
        alignItems: "center",
        alignSelf: "stretch",
        display: "flex",
        flexDirection: "row",
        marginTop: "0",
        marginRight: "0",
        marginBottom: "0",
        marginLeft: "0",
        paddingTop: "0",
        paddingRight: "0",
        paddingBottom: "0",
        paddingLeft: "0",
        position: "relative"
    },
    flexCenter: {
        alignItems: "center",
        // justifyContent: "space-between",
    },
    flexGrow: {
        flexGrow: 1,
    },
    flexWrap: {
        flexWrap: "wrap",
    },
    flexRowReverse: {
        flexDirection: "row-reverse",
    },
    flexJustifyContentCenter: {
        justifyContent: "center",
    },
    paddingHorizontal: {
        paddingLeft: "20px",
        paddingRight: "20px",
    },
    paddingVertical: {
        paddingTop: "20px",
        paddingBottom: "20px",
    },
    hidden: {
        visibility: "hidden"
    },
    header: {
        width: "100%",
        // boxShadow: "rgba(0,0,0,.08) 0 1px 0",
    },
    sidebar: {
        borderRightStyle: "solid",
        borderRightColor: "var(--palette-black-alpha-10,rgba(0, 0, 0, .1))",
        borderRightWidth: "1px"
    },
    logo: {
        minHeight: "48px",
        color: "rgba(0,120,212,1)",
        fontWeight: "bold",
        textDecorationLine: "none",
        columnGap: "15px"
    },
    login: {
        // width: "240px",
    },
    expanded: {
        maxWidth: "240px",
        minWidth: "240px"
    },
    heightMax: {
        height: "100vh"
    },
    widthMax: {
        width: "100%"
    },
    textDisabled: {
        textDecorationLine: "line-through"
    }
})

export default useGlobalStyles
