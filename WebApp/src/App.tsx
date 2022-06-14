import React, { ChangeEvent } from 'react';
import './App.css';
import SearchBar from './components/jsx-components/SearchBar';
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';
import WeatherData from './components/jsx-components/WeatherData';
import { MakeHttpRequest } from './components/jsx-components/httpRequests';
import SearchComponent from './components/jsx-components/GetDataFromUser/SearchComponent';




function App() {
  
  const callback = (e : ChangeEvent) => {
  //  console.log(e +'Hey!');
  }

  return (
   
    <div className="App">
      <Navbar/>
      <Showcase/>
      <br/>
      <SearchComponent/>
      <br/><br/>
      <a>--------------</a>
      <SearchBar onSubmit = 'test' fn = {callback} />
      <WeatherData city = 'Stavanger' date='23/06/2013' fn={MakeHttpRequest}/>
      </div>
  );
}

export default App;
