import { Children } from "../../../../compTypes";
import { WeatherForcastSearchTypeBetweenTwoDates as WeatherForcastSearchTypeBetweenDates } from "./WeatherForcastSearchTypeBetweenTwoDates";
import { WeatherForcastSearchTypeWeekNo } from "./WeatherForcastSearchTypeWeekNo";

type WeatherForcastSearchOneDateProps = {
    children?: Children;
    radioButtonChecked: boolean;
};

export const WeatherForcastSearchOneDate: React.FC<WeatherForcastSearchOneDateProps>
    = (props): JSX.Element => {
        return (
            <>
                <WeatherForcastSearchOneDate radioButtonChecked={true} />
                <WeatherForcastSearchTypeBetweenDates radioButtonChecked={false} />
                <WeatherForcastSearchTypeWeekNo radioButtonChecked={false} />
            </>
        );
    }
