import React, { ChangeEvent } from 'react';
import './App.css';
import SearchBar from './components/jsx-components/SearchBar';
import Axiostest from './tests/Axiostest';
 
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';
import ShowData from './components/ShowData';

import { Axios } from 'axios';
import WeatherData from './components/jsx-components/WeatherData';
// import { ResultFormat} from './components/jsx-components/WeatherData';
import { MakeHttpRequest } from './components/jsx-components/httpRequests';
//import {MakeFakeHttpRequest} from './components/jsx-components/httpRequests'



function App() {

  
  let onSearchSubmit = (e : ChangeEvent)=> {
 // console.log('OnSearchSubmit');
}

  const callback = (e : ChangeEvent) => {
  //  console.log(e +'Hey!');

  }

  function MakeFakeHttpRequest(): Array<number> {
    const obj = [1, 2, 3];
    return obj;
  }

  return (
   
    <div className="App">
      {/* <Navbar/>
      <Showcase/>
      <Axiostest/> */}
      <WeatherData city = 'Stavanger' date='23/06/2013' fn={MakeHttpRequest}/>

      {/* <div id='1000'></div>
      <h4>Test application to compare Weather forecast</h4>
      <br/>

      <SearchBar onSubmit = 'test' fn = {callback} />
      <ShowData /> */}
 
    </div>
  );
}

export default App;
