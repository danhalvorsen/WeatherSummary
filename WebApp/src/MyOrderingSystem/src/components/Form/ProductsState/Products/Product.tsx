import { ProductsType } from '../../productType';
import ProductItem from './ProductItem';

type Props = {
  products: ProductsType[] | undefined;
};

export default function Product(props: Props) {
  const items = props.products?.map((row) => {
    return <ProductItem key={row.id} data={row} />;
  });

  return <div>{items}</div>;
}
