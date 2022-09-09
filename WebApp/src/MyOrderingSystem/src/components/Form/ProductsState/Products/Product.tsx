import React, { useState } from 'react';
import { ProductsType } from '../../productType';
import ProductItem from './ProductItem';

type Props = {
  products: ProductsType[] | undefined;
};

export default function Product(props: Props) {
  const data = props.products?.map((item) => {
    return <ProductItem key={item.id} data={item} />;
  });

  return <div>{data}</div>;
}
