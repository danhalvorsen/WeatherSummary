import React from 'react';
import { ProductsType } from '../../productType';

type Props = {
  data: ProductsType;
};
export default function ProductItem(props: Props) {
  const { title, imageurl, price } = props.data;
  return (
    <div className="border border-primary p-1 m-2 mb-5">
      <div>
        <b>Title:</b> {'  '}
        {title}
      </div>
      <div>
        <img src={imageurl} height="200" width="300"></img>{' '}
        <span> Price: {price}</span>{' '}
      </div>
    </div>
  );
}
