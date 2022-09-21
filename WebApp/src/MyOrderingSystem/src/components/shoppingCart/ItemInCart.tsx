import {CartItem} from '../context/ShoppingCartContext' 

type ItemInCartProps ={
    item: CartItem
}
export const ItemInCart = ({item}: ItemInCartProps)=>{
    return <><p>my Item number is: {item.id}</p> <span> and its quantity is:</span> {item.quantity} <hr/></>
}