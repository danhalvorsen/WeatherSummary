import {Link} from 'react-router-dom'

function NavigationBar() {
  const basketNumber = 3;
  return (
    <>
      <div className=" border border-primary p-3">
        <Link to="/">Home</Link> || <Link to="#">Link1</Link> | <Link to="#">Link2</Link> | <Link to="#">Link3</Link>
        <div style={styles.leftButton}>
          <button>Basket {basketNumber}</button>{' '}
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
