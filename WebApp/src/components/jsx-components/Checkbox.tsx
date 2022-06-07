import React, { useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";



export const Checkbox = function Checkbox() {
  const [check, setCheck] = useState(true);
  const todayDate = Date().substring(0, 15);
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());
  const [activeDate , setActiveDate] = useState(false);

  const showDate = <div className="d-flex flex-row justify-content-center">
  <div> From:<DatePicker selected={startDate} onChange={(date:Date)=>setStartDate(date)}/></div>
  <div> To:<DatePicker selected={endDate} onChange={(date:Date)=>setEndDate(date)}/></div>
  </div>

  const chooseDate = (activeDate) ? showDate : '';

  
  
  return (
    <>
      <div>
      <input type="checkbox" id="Today" defaultChecked={check} /> <label htmlFor="Today">Today:</label> <strong>{todayDate}</strong> <br />
      <input type="checkbox" id="Choose" onClick={()=>{setActiveDate(!activeDate)}} /> <label htmlFor="Choose">Choose Dates:</label>
      </div>

      {chooseDate}


    </>
  );
};
