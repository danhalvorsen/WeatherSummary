import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ProductsType } from '../../productType';
import { productService } from '../../../../../Data/CommunicationService';

export default function Detail() {
  const params = useParams();
  const id = params?.id;

  const [productDetail, setProductDetail] = useState<ProductsType[]>();

  const baseUrl = 'http://localhost:3002/products';
  useEffect(() => {
    productService
      // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
      .getProductById<ProductsType[]>(baseUrl, id!)
      .then((result) => {
        setProductDetail(result);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <>
      {productDetail?.map((row) => {
        return row.id.toString() == id ? (
          <div key={row.id}>
            <div>
              <img src={row.imageurl} width="40%" height="35%" />{' '}
            </div>
            <div style={{ width: '60%' }}>
              <h1>{row.title}</h1>
              <h5>{row.description}</h5>
              <strong style={{ color: 'green', fontSize: 22 }}>
                Price: {row.price}
              </strong>
            </div>
          </div>
        ) : (
          <>
            <div>Page Not Found</div>
          </>
        );
      })}
    </>
  );
}
