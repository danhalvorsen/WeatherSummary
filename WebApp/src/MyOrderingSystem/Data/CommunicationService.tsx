import { ProductType } from '../src/components/Form/productType';
import {UrlValidator , isValid} from '../Validator-OrderingSystem/Validator'
export class ProductQuery {
  baseUrl: string;
  parameter: string;
  id?: number;
}
export class ProductTypeQueryValidator {}

export const getAllProducts = (query: ProductQuery): Promise<ProductType[]> => {
    const urlValidator = new UrlValidator();

  const result = urlValidator.validate(query);
  if (isValid(result)){
  return fetch(query.baseUrl + query.parameter)
    .then((response) => {
      return response.json();
    })
    .catch((error) => {
      console.log(error);
    });
  }
  else throw('Error In validating the Url');
  
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




/////////////////////////////
export const productService = {
  getProducts: async (query: ProductQuery): Promise<ProductType[]> => {
    const res = await fetch(query.baseUrl + query.parameter);
    if (res.ok) {
      return await res.json();
    } else {
      throw console.log('Error');
    }

    // try {
    //   return fetch(query.baseUrl + query.parameter);
    // } catch (error) {
    //   console.log('Error', error);
    // }
  },

  // getProducts: async function fetchProducts<T>(
  //   resourceUrl: string,
  // ): Promise<T> {
  //   return await fetch(resourceUrl)
  //     .then((response) => {
  //       return response.json();
  //     })
  //     .catch((error) => {
  //       console.log(error);
  //     });
  // }

  getProductById: async (query: ProductQuery): Promise<ProductType> => {
    const res = await fetch(query.baseUrl + '/products?id=' + query.id);
    if (res.ok) {
      return res.json();
    } else {
      throw console.log('Error');
    }
  },


};

// if (res.ok) {
//   console.log(res.json())
//   return await res.json();
// } else {
//   throw console.log('Error');
// }

// return { makeApi };

//////////////
// export class Pro {

//   date(query: ProductQuery): Promise<ProductType[]> {
//     return (async (): Promise<ProductType[]> => {
//       const res = await fetch(query.baseUrl + query.parameter)
//         .then((result) => result)
//         .catch(() => console.log('Error'));
//     })();
//   }
// }

/////////////////////////
// export const getAllProducts = async (query: ProductQuery): Promise<any> => {
//   const res = await fetch(query.baseUrl + query.parameter);
//   if (res.ok) {
//     return await res.json();
//   } else {
//     throw console.log('Error');
//   }
// };

////////////////////////
  /////////////////////////
  // getProductById: async function fetchProductsById<T>(
  //   resourceUrl: string,
  //   id: number,
  // ): Promise<T> {
  //   return fetch(resourceUrl + '/products?id=' + id)
  //     .then((response) => response.json())
  //     .catch((error) => {
  //       console.log(error);
  //     });
  // },

