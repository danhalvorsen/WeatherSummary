import Products from './Products/Products';
import React, { useState, useEffect } from 'react';
import { ProductsType } from '../productType';
import SearchBar from './SearchBar';

export default function ProductsState() {
  const [products, setProducts] = useState<ProductsType[]>();

  useEffect(() => {
    const resourceUrl = 'http://localhost:3002/products';
    async function fetchProducts<T>(resourceUrl: string): Promise<T> {
      return await fetch(resourceUrl).then((response) => {
        return response.json();
      });
    }
    fetchProducts<ProductsType[]>(resourceUrl).then((productItems) => {
      setProducts(productItems);
    });
  }, [products]);

  return (
    <>
    <SearchBar/>
      <div className="border border-secondary m-2">
        
        <Products products={products} />
      </div>
    </>
  );
}
