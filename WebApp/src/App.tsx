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
import {WeatherForcastSearch} from './components/newComponents/searchBox/WeatherForcastSearch';
import {SearchButton} from './components/newComponents/searchBox/SearchButton';
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



function App(): JSX.Element {
  const callback = (e: ChangeEvent) => { }

   const [cityName , setCityName] = useState('Stavanger')
  return (
    <div className="App">
      <>
        <Form>
<WeatherForcastSearchState cityName="NAME" >


  <WeatherForcastSearch checked={true} cityName={cityName} > 

                 <LookupCityField state={true}>
                    
                </LookupCityField><SearchButton></SearchButton>
                <br/><br/><br/><br/>
    <SelectSearchOptionState>
        <SelectSearchOption>
          <RadioButton>
            <DayPicker></DayPicker>
          </RadioButton>
        </SelectSearchOption>
    </SelectSearchOptionState>
    <br/><br/><br/><br/>
    <SelectSearchOptionState>
        <SelectSearchOption>
          <RadioButton>
            <WeekPicker></WeekPicker>
          </RadioButton>
        </SelectSearchOption>
    </SelectSearchOptionState>
<br/><br/><br/><br/>
    <SelectSearchOptionState>
        <SelectSearchOption>
          <RadioButton>
            <FromDate></FromDate>
            <ToDate></ToDate>
          </RadioButton>
        </SelectSearchOption>
    </SelectSearchOptionState>

  </WeatherForcastSearch>
  <br/><br/><br/><br/>
    <ListState>
      <List>
        <ListItem></ListItem>
        <ListItem></ListItem>
        <ListItem></ListItem>
        <ListItem></ListItem>
      </List>
    </ListState>

</WeatherForcastSearchState>


        </Form>
      </>
    </div>
  );
}

export default App;
