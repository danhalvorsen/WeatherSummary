import Products from './Products/Products';
import React, { useState, useEffect } from 'react';
import { ProductsType } from '../productType';
import SearchBar from './SearchBar';
import { productService } from '../../../../Data/CommunicationService';

export default function ProductsState() {
  const [products, setProducts] = useState<ProductsType[]>();
  const baseUrl = 'http://localhost:3002/products';
  useEffect(() => {
    productService
      .getProduct<ProductsType[]>(baseUrl)
      .then((productResult) => {
        setProducts(productResult);
      });
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
