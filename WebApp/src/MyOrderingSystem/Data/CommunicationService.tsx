import { ProductType } from '../src/components/Form/productType';
import {
  queryStringValidator,
  isValid,
} from '../Validator/Validator';
export class ProductQuery {
  baseUrl: string;
  parameter: string;
  id?: number;
}
export class addProductType {
  baseUrl: string;
  newProduct: ProductType
}

export class ProductTypeQueryValidator {}

export const getAllProducts = async (
  query: ProductQuery,
): Promise<ProductType[]> => {
  const urlValidator = new queryStringValidator();
  const urlValidate = urlValidator.validate(query);

  if (isValid(urlValidate)) {
    const result = await fetch(query.baseUrl + query.parameter)
      .then((response) => {
        return response.json();
      })
      .catch((error) => {
        console.log(error);
      });
    return result;
  } else throw 'Error In validating the Url';
};

export const getProductById = (query: ProductQuery): Promise<ProductType> => {
  return fetch(query.baseUrl + query.parameter + query.id)
    .then((response) => {
      return response.json();
    })
    .catch((error) => {
      console.log(error);
    });
};

export const addNewProduct = async (query: addProductType)=>{
const response = await fetch(query.baseUrl , {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify(query.newProduct)
});
return response.json();
}

/////////////////////////////
export const productService = {
  getProducts: async (query: ProductQuery): Promise<ProductType[]> => {
    const res = await fetch(query.baseUrl + query.parameter);
    if (res.ok) {
      return await res.json();
    } else {
      throw console.log('Error');
    }
  },
  //Under implementation

  // getProductById: async (query: ProductQuery): Promise<ProductType> => {
  //   const res = await fetch(query.baseUrl + '/products?id=' + query.id);
  //   if (res.ok) {
  //     return res.json();
  //   } else {
  //     throw console.log('Error');
  //   }
  // },
};
