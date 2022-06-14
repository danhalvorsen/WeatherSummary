import React , {useState} from 'react';
import InputCity from './InputCity';


export default function SearchComponent() {

    const [cityName , setCityName] = useState('Oslo');

    

    const saveCityName = (cityName: string)=>{
        setCityName(cityName);
    }



    return(
        <>
        <div>Search a City</div>
        <InputCity City={saveCityName}/>


        </>

    )
}
