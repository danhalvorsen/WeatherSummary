
import { FC } from "react";
type FooProps = {state : boolean}

export const FooProps = (props : FooProps) : JSX.Element => {return(<><h1>FooProps{props.state}</h1></>)}
