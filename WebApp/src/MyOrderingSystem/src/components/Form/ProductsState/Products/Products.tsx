import React from 'react';
import Product from './Product';
import { productsType } from '../../productType';




export default function Products() {

  async function fetchProducts<T>(resourceUrl: string): Promise<T> {
    return await fetch(resourceUrl).then((response) => {
      return response.json();
    });
  }
  const resourceUrl = 'http://localhost:3002/products'
  fetchProducts<productsType[]>(resourceUrl).then(
    (productItems) => {
      console.log(productItems);
      console.log(productItems[0].title);
    },
  );

  
  // const productData = async () => {
  //   const response = await fetch('http://localhost:3002/products');
  //   const body = await response.json();
  //   // console.log(body);
  // };
  // productData();

  return (
    <>
      <Product />
      <Product />
      <Product />
    </>
  );
}
