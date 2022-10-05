import React, { useEffect } from 'react';
import Product from './Product';
import { ProductType } from '../../productType';

type Props = {
  products: ProductType[] | undefined;
};

export default function Products(props: Props) {
  return <Product products={props.products} />;
}
