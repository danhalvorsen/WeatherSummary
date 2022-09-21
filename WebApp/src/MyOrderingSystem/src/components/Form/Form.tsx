import React from 'react';
import NavigationBar from './NavigationBar';
import ProductsState from './ProductsState/ProductsState';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Detail from './ProductsState/Products/Detail';
import { ShoppingCartProvider } from '../context/ShoppingCartContext';
import { ShoppingCart } from '../shoppingCart/ShoppingCart';

function Form() {
  return (
    <ShoppingCartProvider>
      <BrowserRouter>
        <NavigationBar />

        <Routes>
          <Route path="/" element={<ProductsState />} />
          <Route path="/home" element={<Navigate replace to="/" />} />
          <Route path="/detail/:id" element={<Detail />} />
          <Route path="/shoppingCart" element={<ShoppingCart />} />
        </Routes>
      </BrowserRouter>
    </ShoppingCartProvider>
  );
}

export default Form;
