import React, { useState } from 'react'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'

export const Checkbox = function Checkbox() {
    const [check, setCheck] = useState(true)
    const todayDate = Date().substring(0, 15)
    const [activeDate, setActiveDate] = useState(false)
    const [dateRange, setDateRange] = useState([null, null])
    const [startDateFrom, endDateTo] = dateRange

    const singledate =
        'https://localhost:63286/DateQueryAndCity?DateQuery.Date={2022-05-27}&CityQuery.City={Stavanger}'
    const doubledate =
        'https://localhost:63286/BetweenDateQueryAndCity?BetweenDateQuery.From={2022-05-27}&BetweenDateQuery.To={2022-05-28}&CityQuery.City=Stavanger'

    const showDate = (
        <div className="d-flex flex-row justify-content-center">
            <div className="d-flex flex-row m-2">
                {' '}
                <p className="me-2">Date:</p>
                <DatePicker
                    className="m-l-2"
                    selectsRange={true}
                    startDate={startDateFrom}
                    endDate={endDateTo}
                    onChange={(update: [null, null]) => {
                        setDateRange(update)
                    }}
                    isClearable={true}
                />
            </div>
        </div>
    )

    const chooseDate = activeDate ? showDate : ''

    return (
        <>
            <div className="d-inline-flex ms-5">
                <div className="text-start">
                    <input
                        type="checkbox"
                        id="Today"
                        checked={check}
                        onClick={() => {
                            setCheck(!check)
                            setActiveDate(!activeDate)
                        }}
                    />{' '}
                    <label htmlFor="Today">
                        <p className="me-2">Today:</p>
                    </label>{' '}
                    <strong>{todayDate}</strong> <br />
                    <input
                        type="checkbox"
                        id="Choose"
                        checked={!check}
                        onClick={() => {
                            setActiveDate(!activeDate)
                            setCheck(!check)
                        }}
                    />{' '}
                    <label htmlFor="Choose">Choose Dates:</label>
                </div>
            </div>

            {chooseDate}
        </>
    )
}
