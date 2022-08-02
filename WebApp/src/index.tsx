import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Header } from './components/Hurray/Header';




ReactDOM.render(
  <React.StrictMode>
    <Header>
      <Nav></Nav>
    </Header>
    <App>
      <WeatherForcastSearchState>
        <WeatherForcastSearch>
          <InputCity></InputCity><SearchButton></SearchButton>
          <SelectSearchOptionState>
            <SelectSearchOption>
              <DayPicker></DayPicker>
            </SelectSearchOption>
          </SelectSearchOptionState>

          <SelectSearchOptionState>
            <SelectSearchOption>
              <WeekPick></WeekPick>
            </SelectSearchOption>
          </SelectSearchOptionState>
          <SelectSearchOptionState>
          </WeatherForcastSearchState>

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
            <ListItem>

            </ListItem>
          </List>
        </ListState>
      </WeatherForcastSearchState>
    </App>
    <Footer></Footer>

  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
