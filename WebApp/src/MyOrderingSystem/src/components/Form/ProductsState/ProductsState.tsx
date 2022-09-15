import Products from './Products/Products';
import React, { useState, useEffect } from 'react';
import { ProductType } from '../productType';
import SearchBar from './SearchBar';
import {
  ProductQuery,
  getAllProducts,
} from '../../../../Data/CommunicationService';
import {
  ProductValidator,
  isValid,
} from '../../../../Validator-OrderingSystem/Validator';

export default function ProductsState() {
  const [products, setProducts] = useState<ProductType[]>();
  const baseUrl = 'http://localhost:3002';

  const productsQuery = new ProductQuery();
  productsQuery.baseUrl = baseUrl;
  productsQuery.parameter = '/products';

  useEffect(() => {
    const Data = getAllProducts(productsQuery);
    Data.then((res) => {
      const productValidate = new ProductValidator();
      const validProduct = productValidate.validate(res[0]);

      if (isValid(validProduct)) {
        setProducts(res);
      } else {
        console.log('We got Error about Price');
      }
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
