import React from 'react';
import { ProductsType } from '../../productType';
import {Link} from 'react-router-dom'

type Props = {
  data: ProductsType;
};
export default function ProductItem(props: Props) {
  const {id, title, imageurl, price } = props.data;
  return (
    <div className="border border-primary p-1 m-2 mb-5">
      <div>
        <b>Title:</b> {'  '}
        {title}
      </div>
      <div>
        <img src={imageurl} height="200" width="300"></img>{' '}
        <span style={{ fontSize: 18, fontWeight: 700 }}> Price: {price}</span>{' '}
        <span>
          {' '}
          <Link to={"/detail/"+id} style={{ fontWeight: 700, backgroundColor: 'lightblue' }}>
            Show More
          </Link>
        </span>
      </div>
    </div>
  );
}
