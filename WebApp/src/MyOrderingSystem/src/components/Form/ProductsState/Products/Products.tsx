import React, { useState, useEffect } from 'react';
import Product from './Product';
import { ProductsType } from '../../productType';

export default function Products() {
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
  }, []);

  return <Product products={products} />;
}
