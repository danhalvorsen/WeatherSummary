import Product from './Product';
import { ProductsType } from '../../productType';

type Props = {
  products: ProductsType[] | undefined;
};

export default function Products(props: Props) {
  return <Product products={props.products} />;
}
