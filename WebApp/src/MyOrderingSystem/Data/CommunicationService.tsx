export const productService = {
  getProduct: async function fetchProducts<T>(resourceUrl: string): Promise<T> {
    return await fetch(resourceUrl)
      .then((response) => {
        return response.json();
      })
      .catch((error) => {
        console.log(error);
      });
  },
  getProductById: async function fetchProductsById<T>(
    resourceUrl: string,
    id: string,
  ): Promise<T> {
    return await fetch(resourceUrl + '?id=' + id)
      .then((response) => response.json())
      .catch((error) => {
        console.log(error);
      });
  },
};
