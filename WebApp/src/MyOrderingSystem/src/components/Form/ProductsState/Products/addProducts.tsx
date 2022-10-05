import React, { useEffect, useState } from 'react';
import { ProductType } from '../../productType';
import {
  addNewProduct,
  addProductType,
} from '../../../../../Data/CommunicationService';

export default function AddProducts() {
  const oneProduct: ProductType = {
    id: 0,
    title: '',
    description: '',
    price: 0,
    customerprice: 0,
    boughtprice: 0,
    stock: 0,
    brand: '',
    category: '',
    imageUrl: '',
    coupon: 0,
  };
  const [newProduct, setNewProduct] = useState<ProductType>(oneProduct);
  const temporaryIdProducer = Math.floor(Math.random() * 900);

  const changeState = (e: React.ChangeEvent<HTMLInputElement>) => {
    switch (e.target.name) {
      case 'Title':
        setNewProduct({ ...newProduct, title: e.target.value });
        break;
      case 'description':
        setNewProduct({ ...newProduct, description: e.target.value });
        break;
      case 'Stock':
        setNewProduct({ ...newProduct, stock: +e.target.value });
        break;
      case 'Brand':
        setNewProduct({ ...newProduct, brand: e.target.value });
        break;
      case 'Category':
        setNewProduct({ ...newProduct, category: e.target.value });
        break;
      case 'ImageUrl':
        setNewProduct({ ...newProduct, imageUrl: e.target.value });
        break;
      case 'Coupon':
        setNewProduct({ ...newProduct, coupon: +e.target.value });
        break;
      case 'price':
        setNewProduct({ ...newProduct, price: +e.target.value });
        break;
    }
  };

  useEffect(() => {
    setNewProduct({ ...newProduct, id: temporaryIdProducer });
  }, []);

  const addQueryTypes = new addProductType();
  addQueryTypes.baseUrl = 'http://localhost:3002/products';
  addQueryTypes.newProduct = newProduct;

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    addNewProduct(addQueryTypes);
  };

  return (
    <>
      <div>You can add products here...</div>
      <div>
        <form onSubmit={(e) => handleSubmit(e)}>
          <label>Title:</label>
          <input
            type="text"
            name="Title"
            placeholder="title of product"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Description:</label>
          <input
            type="text"
            name="description"
            placeholder="Description of product"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Stock:</label>
          <input
            type="text"
            name="Stock"
            placeholder="quantity of product"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Brand:</label>
          <input
            type="text"
            name="Brand"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Category:</label>
          <input
            type="text"
            name="Category"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Image Url:</label>
          <input
            type="text"
            name="ImageUrl"
            placeholder="place your image URL"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Coupon:</label>
          <input
            type="text"
            name="Coupon"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <label>Price:</label>
          <input
            type="text"
            name="price"
            onChange={(e) => changeState(e)}
          />{' '}
          <br />
          <br />
          <button onClick={() => handleSubmit}>Add Product</button>
        </form>
      </div>
    </>
  );
}
