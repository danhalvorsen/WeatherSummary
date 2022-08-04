import { FC } from "react"
import { Children } from "../compTypes"


type SearchButtonProps = {
    children?: Children
}

export const SearchButton: FC<SearchButtonProps> = (props): JSX.Element => {
    return (
        <>
            <button>Search</button>
            {props?.children}
           
            
        </>
    )
}




