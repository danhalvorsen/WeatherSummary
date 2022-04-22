import { render } from "@testing-library/react";
import React from "react";

class SearchBar extends React.Component {

  render() {
    return (
      <div>
        <div className="container">
          <div className="mb-3">
            <label htmlFor="exampleFormControlTextarea1" className="form-label">Search</label>
            <input className="form-control" id="exampleFormControlTextarea1" placeholder="Search a City" ></input>
          </div>
        </div>
      </div>

    )
  }

}
export default SearchBar;
