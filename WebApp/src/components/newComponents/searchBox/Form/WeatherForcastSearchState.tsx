import { Children } from "../../compTypes";
import React, { useState } from 'react';



export type weatherForcastSearchStatetype =   {
    children?: Children
    cityName: string,
};

export const WeatherForcastSearchState: React.FC<weatherForcastSearchStatetype> 
= (props :weatherForcastSearchStatetype) => {

    //Define States
    
    //const [cityName , setCityName] = useState('Stavanger');
    // const [choiceDate , setChoiceDate] = useState(new Date());
    // const [isChecked , setIsChecked] = useState( true );
    const [weekNo , setWeekNo] = useState(14);
    // const [fromDate , setFromDate] = useState(new Date());
    // const [ToDate , setToDate] = useState(new Date());



    


    return(
        <div>
            <h3>WeatherForcastSearchState</h3>
            {props?.children}
            
        </div>
        
    )
}


