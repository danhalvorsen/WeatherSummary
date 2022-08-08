import { Children } from "../compTypes";

import {LookupCityField} from './LookupCityField';


type weatherForcastSearchtype = {
        cityName?: string,
        choiceDate?: Date,
        isChecked?: Boolean,
        weekNo?: Number,
        fromDate?: Date,
        ToDate?: Date,
        todayDate: string
        };

export const WeatherForcastSearch: React.FC<weatherForcastSearchtype> = (props) => {


    return (
        <div>
            <h3>WeatherForcastSearch</h3>
            

            {props.cityName} <br/>
            {props.todayDate}

            <LookupCityField cityName={props.cityName}/>
            
            



        </div>

    )
}
