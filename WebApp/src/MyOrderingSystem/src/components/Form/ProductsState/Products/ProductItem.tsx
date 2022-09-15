import React from 'react';
import { ProductType } from '../../productType';
import { Link } from 'react-router-dom';

type Props = {
  data: ProductType;
};
export default function ProductItem(props: Props) {
  const { id, title, imageurl, price } = props.data;
  const fontStyle = {fontWeight: 700 }
  return (
    <div className="border border-primary p-1 m-2 mb-5">
      <div>
        <b>Title:</b> {'  '}
        {title}
      </div>
      <div>
        <img src={imageurl} height="200" width="300"></img>{' '}
        <span style={{ fontWeight: 700 , fontSize: 18}}> Price: {price}</span>{' '}
        <span>
          {' '}
          <Link
            to={'/detail/' + id}
            style={{ fontWeight: 700, backgroundColor: 'lightblue' }}
          >
            <span data-testid="custom-element">Show More</span>
          </Link>
        </span>
      </div>
    </div>
  );
}
