import Products from './Products/Products';
import React, { useState, useEffect } from 'react';
import { ProductType } from '../productType';
import SearchBar from './SearchBar';
import {ProductQuery , getAllProducts } from '../../../../Data/CommunicationService';

export default function ProductsState() {
  const [products, setProducts] = useState<ProductType[]>();
  const baseUrl = 'http://localhost:3002';

  const productsQuery = new ProductQuery();
  productsQuery.baseUrl = baseUrl;
  productsQuery.parameter = '/products';

  useEffect(() => {

    const Data = getAllProducts(productsQuery);
    Data.then(res =>{
    setProducts(res);
    })
  }, []);


  return (
    <>
      <SearchBar />
      <div className="border border-secondary m-2">
        <Products products={products} />
      </div>
    </>
  );
}
