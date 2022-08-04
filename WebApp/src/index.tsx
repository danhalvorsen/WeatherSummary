import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Form } from './components/newComponents/searchBox/Form/Form';
import { WeatherForcastSearchState } from './components/newComponents/searchBox/Form/WeatherForcastSearchState';
import { RadioButton } from './components/newComponents/searchBox/RadioButton';
//import { Header } from './components/Hurray/Header';

ReactDOM.render(
  <React.StrictMode>
    <App />  
  
  
    

    {/* <Header>
      <Nav></Nav>
    </Header>
    <App>
    <Form>
      <WeatherForcastSearchState>
        <WeatherForcastSearch>

          <LookupCityField></LookupCityField><SearchButton></SearchButton>
          <SelectSearchOptionState>
            <RadioButton>
            <SelectSearchOption>
              <DayPicker></DayPicker>
            </RadioButton>
            </SelectSearchOption>
          </SelectSearchOptionState>

          <SelectSearchOptionState>
            <SelectSearchOption>
             <RadioButton>
              <WeekPick></WeekPick>
              </RadioButton>
            </SelectSearchOption>
          </SelectSearchOptionState>
          <SelectSearchOptionState>
          

          <SelectSearchOptionState>
            <SelectSearchOption>
              <RadioButton>
                <FromDate></FromDate>
                <ToDate></ToDate>
              </RadioButton>
            </SelectSearchOption>
          </SelectSearchOptionState>

        </WeatherForcastSearch>
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
    </App>
    <Footer></Footer> */}

  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
