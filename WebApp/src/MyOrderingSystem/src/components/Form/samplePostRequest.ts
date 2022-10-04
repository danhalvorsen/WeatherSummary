import { useEffect } from 'react';
import { ProductType } from './productType';

////////////// test for inserting data to json-server //////////////////
const newIphone: ProductType = {
  id: 12,
  title: 'Iphone 14',
  description: 'this is a new Iphone which released in September 2022',
  price: 18000,
  customerprice: 18000,
  boughtprice: 15000,
  stock: 50,
  brand: 'Iphone',
  category: 'Phone',
  imageUrl: 'https://img.gfx.no/2739/2739387/iphone-14-pro.1000x563.jpg',
  coupon: 100,
};

async function postData(
  url = 'http://localhost:3002/products',
  data = newIphone,
) {
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  });
  return response.json();
}

const post = () => {
  postData();
};

useEffect(() => {
  // post()
}, []);

////////////////////////////////////////////////////////////////////////
