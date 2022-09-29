import {Link} from 'react-router-dom'
import { CartButton } from '../cart/CartButton';

function NavigationBar() {
  return (
    <>
      <div className=" border border-primary p-3">
        <Link to="/">Home</Link> || <Link to="signIn">Sign In</Link>
        <div style={styles.leftButton}>
          <CartButton/>
        </div>
      </div>
    </>
  );
}
export default NavigationBar;

const styles = {
  leftButton: {
    right: '0px',
    width: '300px',
    padding: '10px',
    marginLeft: '1000px',
  },
};
