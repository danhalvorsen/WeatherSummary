import React from 'react';
import logo from './logo.svg';
import './App.css';
import SearchBar from './components/jsx-components/SearchBar';
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';

function App() {
  return (
    
    <div className="App">
      <Navbar/>
      <Showcase/>
      

      <h4>Test application to compare Weather forecast</h4>
      <br/>

      <SearchBar/>


    </div>
  );
}

export default App;
