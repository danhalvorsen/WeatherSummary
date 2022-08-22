import { LookupCityField } from '../searchBox/LookupCityField'
import { RadioButton } from '../searchBox/RadioButton'
import { SelectSearchOptionState } from './WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState'
import { WeatherForcastSearchState } from './WeatherForcastSearchState/WeatherForcastSearchState'

interface FormProps {
    children?: JSX.Element | JSX.Element[]
}

export const Form = (props: FormProps) => {
    return (
        <div className="border border-primary m-2">
            <h1>Form</h1>
            {props?.children}
        </div>
    )
}