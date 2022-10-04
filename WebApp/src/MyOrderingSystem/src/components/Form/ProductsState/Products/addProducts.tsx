import produce from 'immer';
import React, { useEffect, useState } from 'react';
import { ProductType } from '../../productType';

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
  const temporaryIdProductor = Math.floor(Math.random() * 900);

  const changeState = (e: string) => {
    setNewProduct({ ...newProduct, title: e });
  };

  const handleSubmit = () => {
    console.log(newProduct);
  };

  useEffect(() => {
    console.log(newProduct);
  }, []);

  return (
    <>
      <div>You can add products here...</div>
      <div>
        <form
          onSubmit={() => {
            handleSubmit;
          }}
        >
          <label>Title:</label>
          <input
            type="text"
            name="title"
            placeholder="title of product"
            onChange={(e) => changeState}
          />{' '}
          <br />
          <label>Description:</label>
          <input
            type="text"
            name="title"
            placeholder="Description of product"
            onChange={(e) => changeState}
          />{' '}
          <br />
          <label>Stock:</label>
          <input
            type="text"
            name="title"
            placeholder="quantity of product"
            onChange={(e) => changeState}
          />{' '}
          <br />
          <label>Brand:</label>
          <input type="text" name="title" onChange={(e) => changeState} />{' '}
          <br />
          <label>Category:</label>
          <input type="text" name="title" onChange={(e) => changeState} />{' '}
          <br />
          <label>Image Url:</label>
          <input
            type="text"
            name="title"
            placeholder="place your image URL"
            onChange={(e) => changeState}
          />{' '}
          <br />
          <label>Coupon:</label>
          <input type="text" name="title" onChange={(e) => changeState} />{' '}
          <br />
          <label>Price:</label>
          <input type="text" name="title" onChange={(e) => changeState} />{' '}
          <br />
          <br />
          <button onClick={()=>handleSubmit}>Add Product</button>
        </form>
      </div>
    </>
  );
}
