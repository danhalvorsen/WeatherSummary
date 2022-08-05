import { Children } from "../compTypes";

import {LookupCityField} from './LookupCityField';


type weatherForcastSearchtype = {
    children?: Children;
    cityName?: string
};

export const WeatherForcastSearch: React.FC<weatherForcastSearchtype> = (props) => {


    return (
        <div>
            <h3>WeatherForcastSearch</h3>
            

            {props?.children}

      


        </div>

    )
}
