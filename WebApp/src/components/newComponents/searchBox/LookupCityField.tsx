import { FC } from "react";

type lookupCityFieldhProps = {
    state?: boolean,
    children?: JSX.Element | JSX.Element[]
};

export const LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {
    return (
        <>
        <h2>HH</h2>
        {props?.children}
        </>)
}