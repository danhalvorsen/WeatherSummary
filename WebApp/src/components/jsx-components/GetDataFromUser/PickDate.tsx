import { useState } from 'react'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'

interface Idates {
    getDates: (betweenDates: string[], check: boolean) => void
}
const date = new Date()
date.toString()

export function PickDate(props: Idates) {
    const [check, setCheck] = useState(true)
    const todayDate = date.toUTCString()
    const [activeDate, setActiveDate] = useState(false)
    const [dateRange, setDateRange] = useState([null, null])
    const [startDateFrom, endDateTo] = dateRange

    const isoFormat = new Date().toISOString()
    const isoDate = []
    isoDate.push(isoFormat.substring(0, 10))

    //console.log(dateRange);
    // console.log([startDateFrom, endDateTo]);

    // const singledate = 'https://localhost:63286/DateQueryAndCity?DateQuery.Date={2022-05-27}&CityQuery.City={Stavanger}';
    // const doubledate = 'https://localhost:63286/BetweenDateQueryAndCity?BetweenDateQuery.From={2022-05-27}&BetweenDateQuery.To={2022-05-28}&CityQuery.City=Stavanger';

    const getUpdate = (update: [null, null]) => {
        setDateRange(update)
        props.getDates(['date1', 'date2'], !check)
    }

    //console.log(dateRange);
    //props.getDates(['date1' , 'date2'])

    //const getUpdate2 = props.getDates(dateRange);

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
                    onChange={getUpdate}
                    isClearable={true}
                />
                {/* To find out more about Datepicker visit : https://www.npmjs.com/package/react-datepicker */}
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
                    />
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
                    />
                    <label htmlFor="Choose">Choose Dates:</label>
                </div>
            </div>

            {chooseDate}
        </>
    )
}
