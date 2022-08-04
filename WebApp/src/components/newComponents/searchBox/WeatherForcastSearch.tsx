import { Children } from "../compTypes";


type weatherForcastSearchtype =   {
    children: Children;
    checked: boolean
};
export const WeatherForcastSearch: React.FC<weatherForcastSearchtype> =(props) => {
    console.log(props.checked);
    return(
        <div>
            <h3>WeatherForcastSearch</h3>
            {props?.children}
            
            
        </div>
        
    )
}
