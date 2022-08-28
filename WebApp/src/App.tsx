
import './App.css';
import Navbar from './components/jsx-components/Navbar';
import Showcase from './components/jsx-components/Showcase';
import { Form } from './components/newComponents/Form/Form';
import { WeatherForcastSearchState } from './components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearchState';

function App() {
  return (
    <>
        <Navbar/>
        <Showcase/>

      <Form>
        <WeatherForcastSearchState />
      </Form>
    </>
  );
}

export default App;
