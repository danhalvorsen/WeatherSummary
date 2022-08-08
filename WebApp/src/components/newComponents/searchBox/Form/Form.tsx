
import { LookupCityField } from "../LookupCityField"
import { RadioButton } from "../RadioButton"
import { SelectSearchOptionState } from "../SelectSearchOptionState"
import { WeatherForcastSearchState } from "./WeatherForcastSearchState"

interface  FormProps  {
    children?: JSX.Element | JSX.Element[]
};

export const Form = (props: FormProps) => {
    return (
       <>
       {props?.children}
       </>
    )
}

         