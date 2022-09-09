export default function Detail() {
  const data = {
    id: 1,
    title: 'Microsoft Surface Laptop 4',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
    price: 1900,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageurl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };

  return (
    <div>
      <div>
       
        <img src={data.imageurl} width="60%" />{' '}
      </div>
      <div style={{ width: '60%' }}>
        <h1>{data.title}</h1>
        <h5>{data.description}</h5>
        <strong style={{ color: 'green', fontSize: 22 }}>
          Price: {data.price}
        </strong>
      </div>
    </div>
  );
}
