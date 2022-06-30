import React , {useState , useEffect} from 'react';
import { ButtonSearch } from './ButtonSearch';
import InputCity from './InputCity';
import { PickDate } from './PickDate';
import {SingleRequest} from './SingleRequest'
import {BetweenRequest} from './BetweenRequest';
import { ShowNewTable } from './ShowNewTable';
import {Iprops} from './SingleRequest'


interface IfuncType {
    func : ()=>void
}


export default function SearchComponent() {

    const date = new Date();
    

    const [cityName , setCityName] = useState('Oslo');
    const [todayDate , setTodayDate] = useState(date);
    const [betweenDates , setBetweenDates]= useState(['2022-06-10' , '2022-06-16']);
    const [isRequestForOneDay , setIsRequestForOneDay ]= useState(true);
    const [flag , setFlag] = useState(false);


    //date.toISOString()

    const saveCityName = (cityName: string)=>{
        setCityName(cityName);
    }

    const showTable = <ShowNewTable/>

 
    const showResultData= flag ? <SingleRequest date={date} cityName={cityName}/> : <></> 



    const getDates = (betweenDates:string[] , check:boolean)=>{
        setBetweenDates(betweenDates)
    }



    return(
        <>
        <div>Search a City</div>
        <InputCity City={saveCityName}/> <br/><br/>
        <PickDate getDates={getDates} /> <br/><br/>
        <ButtonSearch setFlag={setFlag}/>

       {showTable}
       {showResultData}
        


        </>

    )
}
