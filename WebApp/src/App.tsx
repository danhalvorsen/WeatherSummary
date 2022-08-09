import React, { ChangeEvent, Children, useState } from 'react';
import './App.css';
import { Form } from './components/newComponents/Form/Form';
import { WeatherForcastSearchState } from './components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearchState';


class SearchProp {
  constructor() {
    this.name1 = "Name1";
    this.name2 = "Name2";
  }
  name1: string = "Name1";
  name2: string = "Name2";
}

class GlobalState {
  name: string = "Bergen";
  search = new SearchProp()
}

class GlobalState1 {
  name: string = "Oslo";
  search = new SearchProp()
}


export interface State {
  name1: string;
  name2: string;
  child: {
    name1: string;
    name2: string;
  }
};
 

function App() {
  return (
    <div className="App">
      <h1>Debugging</h1>
      <Form>
        <WeatherForcastSearchState />
      </Form>
    </div>
  );
}

export default App;
