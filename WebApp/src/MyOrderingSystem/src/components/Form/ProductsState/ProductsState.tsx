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
    const productValidate = new ProductValidator();

    Data.then((res) => {
      const validatedProducts: Array<ProductType> = [
        {
          id: 1,
          title: 'Microsoft Surface Laptop 4',
          description:
            'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
          price: 1900,
          customerprice: 14900,
          boughtprice: 14900,
          stock: 18,
          brand: 'Microsoft',
          category: 'Laptops',
          imageurl:
            'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
          coupon: 50,
        },
      ];
      res.forEach((row, index) => {
        const Product = productValidate.validate(res[index]);
        if (isValid(Product)) {
          validatedProducts[index] = row;
        } else {
          console.log('We have some issue on the received data');
        }
      });
      setProducts(validatedProducts);
    });
  }, []);

//Style
  const styles = {
    border: {
      padding: '5px',
      margin: '2px',
      border: '1px solid black',
      
    },
  };
  return (
    <>
      <SearchBar />
      <div style={styles.border}>
        <Products products={products} />
      </div>
    </>
  );

 
}
