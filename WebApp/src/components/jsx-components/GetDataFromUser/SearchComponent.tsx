import React , {useState , useEffect} from 'react';
import { ButtonSearch } from './ButtonSearch';
import InputCity from './InputCity';
import { PickDate } from './PickDate';


interface IfuncType {
    func : ()=>void
}


export default function SearchComponent() {

    const todayDate = Date().substring(0, 15);

    const [cityName , setCityName] = useState('Oslo');
    const [dates , setDates] = useState([todayDate]);


    const saveCityName = (cityName: string)=>{
        setCityName(cityName);
    }

    const clicked: ()=>void =  ()=>{
       console.log('clicked on Button to make a request');
    }

    const getDates = (e:null[])=>{
        //  setDates([startDateFrom, endDateTo])
       // console.log(e)

    }


    useEffect(()=>{
        //console.log(dates[0]);
    } , [])


    return(
        <>
        <div>Search a City</div>
        <InputCity City={saveCityName}/> <br/><br/>
        <PickDate getDates={getDates} /> <br/><br/>
        <ButtonSearch func={clicked}/>


        </>

    )
}
