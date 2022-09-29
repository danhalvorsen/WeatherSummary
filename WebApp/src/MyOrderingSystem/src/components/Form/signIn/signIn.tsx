import React from 'react'
import { useNavigate } from 'react-router-dom';
import { useShoppingCart } from '../../context/ShoppingCartContext'

export default function SignIn() {
    const navigate = useNavigate();
    const {changeLoginRole} = useShoppingCart()

    const loginHeader = (e: string)=>{
        changeLoginRole(e);
        navigate('/')

        }
  return (
    <div>
      <div><button onClick={()=>{loginHeader('Admin')}}>Login as Admin</button></div>
      <br/>
      <div><button>Login as Customer</button></div>
    </div>
  )
}
