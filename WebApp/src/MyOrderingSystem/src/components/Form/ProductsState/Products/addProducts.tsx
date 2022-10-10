import React, { useEffect, useState } from 'react';
import { ProductType } from '../../productType';
import {
  addNewProduct,
  addProductType,
} from '../../../../../Data/CommunicationService';
import { useShoppingCart } from '../../../context/ShoppingCartContext';

import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

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
  const { lastProductId } = useShoppingCart();
  const newId = lastProductId + 1;
  const changeState = (e: React.ChangeEvent<HTMLInputElement>) => {
    switch (e.target.id) {
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
        setNewProduct({ ...newProduct, price: +e.target.value, id: newId });

        break;
    }
  };

  ////Used this library in order to validate form/// https://www.bezkoder.com/react-hook-form-typescript/
  const validationSchema = Yup.object().shape({
    title: Yup.string().required('Title is required'),
    description: Yup.string()
      .required('Description is required')
      .min(6, 'Description must be at least 6 characters')
      .max(300, 'Description must not exceed 300 characters'),
    price: Yup.number()
      .required('Price is required')
      .min(2, 'The Price could not be less than 10'),
    stock: Yup.number().required('Stock is required'),
    brand: Yup.string().required('Brand is required'),
    coupon: Yup.number().required('Coupon is required'),
    category: Yup.string().required('Category is required'),
    imageUrl: Yup.string().required('Image URL is required'),
  });

  const addQueryTypes = new addProductType();
  addQueryTypes.baseUrl = 'http://localhost:3002/products';
  addQueryTypes.newProduct = newProduct;

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<ProductType>({ resolver: yupResolver(validationSchema) });

  const onSubmit = () => {
    const addProductAndCleanPage = () => {
      addNewProduct(addQueryTypes);
      alert("You've added the product successfully");
      reset();
    };
    const isExecuted = confirm('Are you sure to add this Item?');
    isExecuted ? addProductAndCleanPage() : undefined;
  };

  return (
    <div style={{ marginLeft: '90px', marginRight: '400px' }}>
      <h2>add products form...</h2>
      <br />
      <div className="register-form ">
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="form-group">
            <label>Title</label>
            <input
              type="text"
              {...register('title')}
              className={`form-control ${errors.title ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="Title"
            />
            <div className="invalid-feedback">{errors.title?.message}</div>
          </div>

          <div className="form-group">
            <label>Description</label>
            <input
              type="text"
              {...register('description')}
              className={`form-control ${
                errors.description ? 'is-invalid' : ''
              }`}
              onChange={(e) => changeState(e)}
              id="description"
            />
            <div className="invalid-feedback">
              {errors.description?.message}
            </div>
          </div>

          <div className="form-group">
            <label>Brand</label>
            <input
              type="text"
              {...register('brand')}
              className={`form-control ${errors.brand ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="Brand"
            />
            <div className="invalid-feedback">{errors.brand?.message}</div>
          </div>

          <div className="form-group">
            <label>Price</label>
            <input
              type="number"
              {...register('price')}
              className={`form-control ${errors.price ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="price"
            />
            <div className="invalid-feedback">{errors.price?.message}</div>
          </div>

          <div className="form-group">
            <label>Category</label>
            <input
              type="text"
              {...register('category')}
              className={`form-control ${errors.category ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="Category"
            />
            <div className="invalid-feedback">{errors.category?.message}</div>
          </div>

          <div className="form-group">
            <label>Image Url</label>
            <input
              type="text"
              {...register('imageUrl')}
              className={`form-control ${errors.imageUrl ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="ImageUrl"
            />
            <div className="invalid-feedback">{errors.imageUrl?.message}</div>
          </div>

          <div className="form-group">
            <label>Stock</label>
            <input
              type="number"
              {...register('stock')}
              className={`form-control ${errors.stock ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="Stock"
            />
            <div className="invalid-feedback">{errors.stock?.message}</div>
          </div>

          <div className="form-group">
            <label>Coupon</label>
            <input
              type="number"
              {...register('coupon')}
              className={`form-control ${errors.coupon ? 'is-invalid' : ''}`}
              onChange={(e) => changeState(e)}
              id="Coupon"
            />
            <div className="invalid-feedback">{errors.coupon?.message}</div>
          </div>

          <div className="form-group">
            <button
              type="submit"
              className="btn btn-primary"
              onClick={() => handleSubmit(onSubmit)}
            >
              Add Product
            </button>
            <button
              type="button"
              onClick={() => reset()}
              className="btn btn-warning float-center"
            >
              Reset
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
