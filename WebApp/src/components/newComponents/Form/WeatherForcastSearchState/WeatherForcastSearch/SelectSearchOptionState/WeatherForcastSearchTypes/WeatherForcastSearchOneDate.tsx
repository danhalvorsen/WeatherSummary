import { RadioButton } from "../../../../../searchBox/RadioButton";
import { Children } from "../../../../compTypes";
import { WeatherForcastEnumType } from "../SelectSearchOptionState";

type WeatherForcastSearchOneDateProps = {
    children?: Children;
    radioButtonChecked: boolean;
    onChange: (typeName: WeatherForcastEnumType ) => void
};

export const WeatherForcastSearchOneDate: React.FC<WeatherForcastSearchOneDateProps>
    = (props): JSX.Element => {
        return (
            <>
                 <div className='border border-info m-3 mt-5 pt-2 border-2'> Today: 
                    <RadioButton enabled={props.radioButtonChecked}
                    onChange= {props.onChange}
                    motherName={WeatherForcastEnumType.WeatherForcastSearchOneDate}
                    />
                    
                </div>
             
                {props.children}
            </>
        );
    }
