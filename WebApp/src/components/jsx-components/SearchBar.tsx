import { render } from "@testing-library/react";
import { createRoot } from "react-dom/client";

import React, { ChangeEvent } from "react";
import axios from "axios";
import { Checkbox } from "./Checkbox";


 export type ISearchbarProps=  {
  onSubmit: string,
  fn:(e: ChangeEvent) => void
}

class SearchBar extends React.Component<ISearchbarProps> {
  
  state = { term: "" };

 

  onInputSubmit=(event : any) => {
    event.preventDefault();
        console.log(this.state.term);
        this.props.fn(event);
  };

  render() {
    
       //another sample source is:  https://api.github.com/users/hacktivist123
    // axios
    //   .get("https://localhost:5001/2020-04")
    //   .then(function (response: any) {
    //     // handle success
    //     console.log(response);
    //     console.log(response.data['city']);
    //   });


    return (
      <div>
        <form onSubmit={this.onInputSubmit}>
          <div className="container">
            <div className="mb-3">
              <label 
                htmlFor="exampleFormControlTextarea1"
                className="form-label"
              >
                Search for a City:
              </label>
              <input
                className="form-control"
                id="exampleFormControlTextarea1"
                placeholder="Enter a location name"
                onChange={e=> this.setState({term: e.target.value})}
              ></input>

              <Checkbox />
                    
            </div>
          </div>
        </form>
      </div>
    );
  }
}
export default SearchBar;
