import React, { ChangeEvent } from 'react';
import './App.css';
import SearchBar from './components/jsx-components/SearchBar';
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';
import WeatherData from './components/jsx-components/WeatherData';
import { MakeHttpRequest } from './components/jsx-components/httpRequests';
import SearchComponent from './components/jsx-components/GetDataFromUser/SearchComponent';

function App(): JSX.Element {
  const callback = (e: ChangeEvent) => { }
  return (
    <div className="App">
      <Showcase />
    </div>
  );
}

export default App;
