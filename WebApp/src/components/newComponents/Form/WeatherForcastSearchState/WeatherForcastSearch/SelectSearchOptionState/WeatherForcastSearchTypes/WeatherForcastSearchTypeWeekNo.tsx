import { RadioButton } from "../../../../../searchBox/RadioButton";
import { Children } from "../../../../compTypes";

type WeatherForcastSearchTypeWeekNoProps = {
    children?: Children;
    radioButtonChecked: boolean;
};


export const WeatherForcastSearchTypeWeekNo: React.FC<WeatherForcastSearchTypeWeekNoProps>
    = (props): JSX.Element => {
        return (
            <>
                <RadioButton enabled={true} />
            </>
        );
    }