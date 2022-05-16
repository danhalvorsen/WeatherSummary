import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import App from '../App';
import exp from 'constants';
import  WeatherData from '../components/jsx-components/WeatherData';
import { MakeFakeHttpRequest } from '../components/jsx-components/httpRequests';



 
    test("Is the name 'Dan' on screen? ", async () =>{
        render(<WeatherData city = 'Stavanger' date='23/06/2013' fn={MakeFakeHttpRequest}/>);
        let element = await screen.getByText('Dan');
        expect (element).toBeDefined();
        // expect (element.innerText).toBe('Dan'); 
    })
 


// test('renders learn react link', async () => {
//     let component = <WeatherData city = 'Stavanger' date='23/06/2013' fn={MakeFakeHttpRequest}/>;

//     const {asFragment}  = render();

//   const firstRender = asFragment();
//   await fireEvent.click(screen.getByText('CLICK ON ME'));

//   expect(firstRender).toBe(true);  
  
// });



export{}
