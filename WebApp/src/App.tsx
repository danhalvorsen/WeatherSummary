import React, { ChangeEvent } from 'react';
import './App.css';
import SearchBar from './components/jsx-components/SearchBar';
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';
import WeatherData from './components/jsx-components/WeatherData';
import { MakeHttpRequest } from './components/jsx-components/httpRequests';
import SearchComponent from './components/jsx-components/GetDataFromUser/SearchComponent';
import { Form } from './components/newComponents/Form';
import { WeatherForcastSearchState } from './components/newComponents/searchBox/WeatherForcastSearchState';
import { RadioButton } from './components/newComponents/searchBox/RadioButton';
import { WeatherForcastSearch as WeatherForcastSearchComponent } from './components/newComponents/searchBox/WeatherForcastSearch';
import InputCity from './components/jsx-components/GetDataFromUser/InputCity';
import { LookupCityField } from './components/newComponents/searchBox/LookupCityField';
import { Foo } from './components/newComponents/searchBox/Foo'
import { FooProps } from './components/newComponents/FooProps';


function App(): JSX.Element {
  const callback = (e: ChangeEvent) => { }
  return (
    <div className="App">
      <>
        <Form>
          <WeatherForcastSearchState >
            <LookupCityField state={true}>
              <RadioButton state={false}></RadioButton>
            </LookupCityField>
          </WeatherForcastSearchState>

        </Form>
      </>
    </div>
  );
}

export default App;
