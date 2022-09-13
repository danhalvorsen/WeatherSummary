import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ProductType } from '../../productType';
import {
  ProductQuery,
  getProductById,
} from '../../../../../Data/CommunicationService';

export default function Detail() {
  const [productDetail, setProductDetail] = useState<ProductType>();
  const params = useParams();
  const id = Number(params.id);
  const baseUrl = 'http://localhost:3002/';

  const productQuery = new ProductQuery();
  productQuery.baseUrl = baseUrl;
  productQuery.parameter = 'products/';
  productQuery.id = id;

  useEffect(() => {
    const Data = getProductById(productQuery);
    Data.then((res) => {
      setProductDetail(res);
    });
  }, []);

  return (
    <>
      <div key={productDetail?.id}>
        <div>
          <img src={productDetail?.imageurl} width="40%" height="35%" />{' '}
        </div>
        <div style={{ width: '60%' }}>
          <h1>{productDetail?.title}</h1>
          <h5>{productDetail?.description}</h5>
          <strong style={{ color: 'green', fontSize: 22 }}>
            Price: {productDetail?.price}
          </strong>
        </div>
      </div>
    </>
  );
}
