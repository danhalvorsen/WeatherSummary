import React from 'react';
import { ProductType } from '../../productType';
import { Link } from 'react-router-dom';

type Props = {
  data: ProductType;
};
export default function ProductItem(props: Props) {
  const { id, title, imageurl, price } = props.data;
 
//Style
const styles = {
  border: {
    padding: '15px',
    margin: '12px',
    border: '1px solid black',
    
  },
};
  return (
    <div style={styles.border}>
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
            <span role="custom-element">Show More</span>
          </Link>
        </span>
      </div>
    </div>
  );
}
