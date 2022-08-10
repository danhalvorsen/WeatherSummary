import { RadioButton } from "../../../../../searchBox/RadioButton";
import { Children } from "../../../../compTypes";
import { WeatherForcastEnumType } from "../SelectSearchOptionState";

type WeatherForcastSearchTypeWeekNoProps = {
    children?: Children;
    radioButtonChecked: boolean;
    onChange: (typeName: WeatherForcastEnumType) => void;
};


export const WeatherForcastSearchTypeWeekNo: React.FC<WeatherForcastSearchTypeWeekNoProps>
    = (props): JSX.Element => {
        return (
            <>
                <div className='border border-info m-3 mt-5 pt-2 border-2'>Week No:
                    <RadioButton
                        enabled={props.radioButtonChecked}
                        onChange={props.onChange}
                        motherName={WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo}
                    />
                </div>

                {props.children}
            </>
        );
    }