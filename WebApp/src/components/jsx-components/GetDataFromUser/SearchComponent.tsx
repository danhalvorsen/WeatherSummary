import React , {useState , useEffect} from 'react';
import { ButtonSearch } from './ButtonSearch';
import InputCity from './InputCity';
import { PickDate } from './PickDate';
import {SingleRequest} from './SingleRequest';
import {BetweenRequest} from './BetweenRequest';


interface IfuncType {
    func : ()=>void
}


export default function SearchComponent() {

    const date = new Date();
    const todayDate = date.toISOString().substring(0 , 10);




    const [cityName , setCityName] = useState('Oslo');
    const [betweenDates , setBetweenDates]= useState(['2022-06-10' , '2022-06-16']);
    const [isRequestForOneDay , setIsRequestForOneDay ]= useState(true);


    const saveCityName = (cityName: string)=>{
        setCityName(cityName);
    }




    const request = ()=>{
        return (isRequestForOneDay ? SingleRequest(todayDate) :  BetweenRequest(betweenDates));
    }
    const clicked: ()=>void =  ()=>{
       console.log('clicked on Button to make a request');
         request();
    }



    const getDates = (betweenDates:string[] , check:boolean)=>{
        setBetweenDates(betweenDates)
        console.log(betweenDates);
        console.log(check);

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
