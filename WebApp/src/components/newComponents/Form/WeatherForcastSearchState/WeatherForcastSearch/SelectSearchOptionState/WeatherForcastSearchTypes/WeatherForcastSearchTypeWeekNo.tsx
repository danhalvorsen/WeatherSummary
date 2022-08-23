import { RadioButton } from '../../../../../searchBox/RadioButton'
import { WeekPicker } from '../../../../../searchBox/WeekPicker'
import { Children } from '../../../../compTypes'
import { weekNo } from '../apiTypes'
import { WeatherForcastEnumType } from '../SelectSearchOptionState'

type WeatherForcastSearchTypeWeekNoProps = {
    children?: Children
    radioButtonChecked: boolean
    ChoiceWeekNo: (weekNo: weekNo) => void;
    onChange: (typeName: WeatherForcastEnumType) => void
}

export const WeatherForcastSearchTypeWeekNo: React.FC<
    WeatherForcastSearchTypeWeekNoProps
> = (props): JSX.Element => {
    return (
        <>
            <div className="border border-info m-3 mt-5 pt-2 border-2">
                <h5 className="">WeatherForcastSearchTypeWeekNo</h5>
                <span>Week No:</span>
                <RadioButton
                    enabled={props.radioButtonChecked}
                    onChange={props.onChange}
                    motherName={
                        WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo
                    }
                />
                <WeekPicker ChoiceWeekNo={props.ChoiceWeekNo} />
            </div>

            {props.children}
        </>
    )
}
