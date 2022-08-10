import { RadioButton } from "../../../../../searchBox/RadioButton";
import { Children } from "../../../../compTypes";
import { WeatherForcastEnumType } from "../SelectSearchOptionState";

type WeatherForcastSearchTypeBetweenTwoDatesProps = {
    children?: Children;
    radioButtonChecked: boolean;
    onChange: (typeName: WeatherForcastEnumType ) => void

};

export const WeatherForcastSearchTypeBetweenTwoDates: React.FC<WeatherForcastSearchTypeBetweenTwoDatesProps>
    = (props): JSX.Element => {
        return (
            <>
                <div className='border border-info m-3 mt-5 pt-2 border-2'>Between Dates
                <RadioButton enabled={props.radioButtonChecked}
                    onChange={props.onChange }
                    motherName={WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates}
                    />
                </div>

                {props.children}
            </>
        );
    }
