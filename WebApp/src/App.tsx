import React, { ChangeEvent, Children, useState } from 'react';
import './App.css';
import SearchBar from './components/jsx-components/SearchBar';
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';
import WeatherData from './components/jsx-components/WeatherData';
import { MakeHttpRequest } from './components/jsx-components/httpRequests';
import SearchComponent from './components/jsx-components/GetDataFromUser/SearchComponent';
import { Form } from './components/newComponents/searchBox/Form/Form';
import { WeatherForcastSearchState } from './components/newComponents/searchBox/Form/WeatherForcastSearchState';
import { RadioButton } from './components/newComponents/searchBox/RadioButton';
import { WeatherForcastSearch as WeatherForcastSearchComponent } from './components/newComponents/searchBox/WeatherForcastSearch';
import InputCity from './components/jsx-components/GetDataFromUser/InputCity';
import { LookupCityField } from './components/newComponents/searchBox/LookupCityField';
import { WeatherForcastSearch } from './components/newComponents/searchBox/WeatherForcastSearch';
import { SearchButton } from './components/newComponents/searchBox/SearchButton';
import { SelectSearchOptionState } from './components/newComponents/searchBox/SelectSearchOptionState';
import { SelectSearchOption } from "./components/newComponents/searchBox/SelectSearchOptionProps";
import { DayPicker } from './components/newComponents/searchBox/DayPicker';
import { WeekPicker } from './components/newComponents/searchBox/WeekPicker';
import { FromDate } from './components/newComponents/searchBox/FromDate';
import { ToDate } from './components/newComponents/searchBox/ToDate';
import { ListState } from './components/newComponents/searchBox/ListState';
import { List } from './components/newComponents/searchBox/List';
import { ListItem } from './components/newComponents/searchBox/ListItem';
import { isPropertySignature } from 'typescript';
import { stringify } from 'querystring';
import FooDate from './components/types';


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
const FuncState = (nn1: string, nn2: string): State => {

  let s: State = { name1: nn1, name2: nn2, child: { name1: nn1, name2: nn2 } };
  return s;

}


function App() {


  // const [global, setGlobal] = useState(FuncState("Oslo", "Bergen"))

  // function X(e: any) {
  //   const s = global;
  //     setGlobal(FuncState("Iran", "Norway"))
  //     // setGlobal(new GlobalState1())
  //     // setGlobal(new GlobalState())
  // };

  
  return (
    <div className="App">

      {/* <WeatherForcastSearchState name={global.child.name1} /> */}
      <Form>
        <WeatherForcastSearchState />
       
  
      </Form>

    </div>
  );
}

export default App;
