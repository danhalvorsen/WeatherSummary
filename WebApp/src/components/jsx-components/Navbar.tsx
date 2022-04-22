import React, { Component } from "react";

function Navbar(){
    return (

        <div>

<nav className="navbar navbar-expand-lg bg-dark navbar-dark">
          <div className="container">

            <a href="#" className="navbar-brand">Compare weather</a>

            <button
              className="navbar-toggler"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navmenu"
            >
              <span className="navbar-toggler-icon"></span>
            </button>

            <div className="collapse navbar-collapse" id="navmenu">
              <ul className="navbar-nav ms-auto">
                <li className="navbar-item">
                  <a href="#learn" className="nav-link">preferred weather</a>
                </li>
                <li className="navbar-item">
                  <a href="#questions" className="nav-link">weather for tomorrow</a>
                </li>
                <li className="navbar-item">
                  <a href="#contact-us" className="nav-link">weather history</a>
                </li>
              </ul>
            </div>
            

          </div>

        </nav>

        </div>

    )
}

export default Navbar;