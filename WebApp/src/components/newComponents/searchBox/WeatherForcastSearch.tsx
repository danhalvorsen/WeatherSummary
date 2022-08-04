import { Children } from "../compTypes";


type weatherForcastSearchtype =   {
    children: Children;
    checked: boolean,
    cityName:string
   
};
export const WeatherForcastSearch: React.FC<weatherForcastSearchtype> =(props) => {
    console.log(props.checked);

    function something(props : weatherForcastSearchtype)  {
        console.log("!!!!");
    };

    return(
        <div>
            <h3>WeatherForcastSearch</h3>
            <h1>{props.cityName}</h1>
            {props?.children}
            
            
        </div>
        
    )
}
