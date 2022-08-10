import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Form } from './components/newComponents/Form/Form';
import { WeatherForcastSearchState } from './components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearchState';

function App() {
  return (
  <>
   <Form>
    <h1>After Form</h1>
      <WeatherForcastSearchState stringDate={'1900-01-01'} />
   </Form>
   </>
  );
}

export default App;



// import React, { ChangeEvent, Children, useState } from 'react';
// import './App.css';
// // import { Form } from './components/newComponents/Form/Form';
// // import { WeatherForcastSearchState } from './components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearchState';


// function App() {
//   return (
//     <div className="App">
//       <h1>Debugging</h1>
      
//     </div>
//   );
// }

// export default App;

// // class SearchProp {
// //   constructor() {
// //     this.name1 = "Name1";
// //     this.name2 = "Name2";
// //   }
// //   name1: string = "Name1";
// //   name2: string = "Name2";
// // }

// // class GlobalState {
// //   name: string = "Bergen";
// //   search = new SearchProp()
// // }

// // class GlobalState1 {
// //   name: string = "Oslo";
// //   search = new SearchProp()
// // }


// // export interface State {
// //   name1: string;
// //   name2: string;
// //   child: {
// //     name1: string;
// //     name2: string;
// //   }
// // };
 

