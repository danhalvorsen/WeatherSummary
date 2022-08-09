import { FC } from "react";
import { Children, propsType } from "../Form/compTypes";


type RadioButtonProps = {
    children?: Children;
    enabled: boolean;
  };

export const RadioButton = (props?: RadioButtonProps): JSX.Element => {
    return (
        <>
         <input type="radio" checked={props?.enabled} />
        </>
    )
}

