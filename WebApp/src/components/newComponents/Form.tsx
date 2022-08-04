
import { LookupCityField } from "./searchBox/LookupCityField"
import { RadioButton } from "./searchBox/RadioButton"
import { SelectSearchOptionState } from "./searchBox/SelectSearchOptionState"
import { WeatherForcastSearchState } from "./searchBox/WeatherForcastSearchState"

interface  FormProps  {
    children?: JSX.Element| JSX.Element[]
};

export const Form = (props: FormProps) => {
    return (
       <>
       {props?.children}
       </>
    )
}

         