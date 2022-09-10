import React from "react";
import NavigationBar from "./NavigationBar";
import ProductsState from "./ProductsState/ProductsState";
import {BrowserRouter , Routes , Route , Navigate } from "react-router-dom"
import Detail from "./ProductsState/Products/Detail";

function Form() {
    return (
      <>
       <BrowserRouter>
       <NavigationBar />


      <Routes>
        <Route path="/" element={<ProductsState/>} />
        <Route path="/home" element={<Navigate replace to="/"/>} />
        <Route path="/detail/:id" element={<Detail/>} />

    
      </Routes>
      
       
       </BrowserRouter>
      
      
      </>
    );
  }
  
  export default Form;
  