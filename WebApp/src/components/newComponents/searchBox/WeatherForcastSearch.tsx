import { Children } from "../Form/compTypes";
import { LookupCityField } from './LookupCityField';
import { SelectSearchOptionState } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { propsType } from '../Form/compTypes';


export const WeatherForcastSearch: React.FC<propsType> = (props) => {

    

    return (
        <div className='border border-danger mt-5 m-3'>

            <h3>WeatherForcastSearch</h3>

            <LookupCityField cityName={props.cityName} />

            <SelectSearchOptionState date={props.date}/>
            <SelectSearchOptionState weekNo={props.weekNo}/>
            <SelectSearchOptionState />
            


        </div >

    )
}
