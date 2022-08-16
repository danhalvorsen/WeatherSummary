import React from 'react'
import logo from './logo.svg'
import './App.css'
import { Form } from './components/newComponents/Form/Form'
import { WeatherForcastSearchState } from './components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearchState'

function App() {
    return (
        <>
            <Form>
            <WeatherForcastSearchState stringDate={'1900-01-01'} />
            </Form>
        </>
    )
}

export default App