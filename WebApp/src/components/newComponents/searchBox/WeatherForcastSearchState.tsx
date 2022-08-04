

export type Childrens = JSX.Element|JSX.Element[];

type weatherForcastSearchStatetype =   {
    children?: Childrens
};

export const WeatherForcastSearchState: React.FC<weatherForcastSearchStatetype> 
= (props :weatherForcastSearchStatetype) => {
    return(
        <div>
            <h3>WeatherForcastSearchState</h3>
            {props?.children}
        </div>
        
    )
}


