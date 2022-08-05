import { Children } from "../compTypes";


type weatherForcastSearchtype =   {
    children: Children;
    week: number;
};

export const WeatherForcastSearch: React.FC<weatherForcastSearchtype> =(props) => {
   

    return(
        <div>
            <h3>WeatherForcastSearch</h3>
            
            {props?.children}
            
            
        </div>
        
    )
}
