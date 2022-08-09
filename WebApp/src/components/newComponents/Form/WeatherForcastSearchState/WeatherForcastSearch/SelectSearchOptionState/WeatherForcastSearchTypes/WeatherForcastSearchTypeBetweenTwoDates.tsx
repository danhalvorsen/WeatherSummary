import { RadioButton } from "../../../../../searchBox/RadioButton";
import { Children } from "../../../../compTypes";

type WeatherForcastSearchTypeBetweenTwoDatesProps = {
    children?: Children;
    radioButtonChecked: boolean;
};

export const WeatherForcastSearchTypeBetweenTwoDates: React.FC<WeatherForcastSearchTypeBetweenTwoDatesProps>
    = (props): JSX.Element => {
        return (
            <>
                <RadioButton enabled={true}
                />
            </>
        );
    }
