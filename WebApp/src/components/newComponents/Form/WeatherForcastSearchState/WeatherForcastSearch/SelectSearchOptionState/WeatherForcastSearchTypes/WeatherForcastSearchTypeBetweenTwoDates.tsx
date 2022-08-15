import { FromDate } from '../../../../../searchBox/FromDate'
import { RadioButton } from '../../../../../searchBox/RadioButton'
import { ToDate } from '../../../../../searchBox/ToDate'
import { Children } from '../../../../compTypes'
import { WeatherForcastEnumType } from '../SelectSearchOptionState'

type WeatherForcastSearchTypeBetweenTwoDatesProps = {
    children?: Children
    radioButtonChecked: boolean
    onChange: (typeName: WeatherForcastEnumType) => void
}

export const WeatherForcastSearchTypeBetweenTwoDates: React.FC<
    WeatherForcastSearchTypeBetweenTwoDatesProps
> = (props): JSX.Element => {
    return (
        <>
            <div className="border border-info m-3 mt-5 pt-2 border-2">
                <h5 className="">WeatherForcastSearchTypeBetweenTwoDates</h5>
                <span>Between Dates: </span>
                <RadioButton
                    enabled={props.radioButtonChecked}
                    onChange={props.onChange}
                    motherName={
                        WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates
                    }
                />
                <FromDate /> <ToDate />
            </div>

            {props.children}
        </>
    )
}
