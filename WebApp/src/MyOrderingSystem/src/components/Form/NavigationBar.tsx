function NavigationBar() {
  const basketNumber = 3;
  return (
    <>
      <div className=" border border-primary p-3">
        <a href="#">Home</a> || <a href="#">Link1</a> | <a href="#">Link2</a> | <a href="#">Link3</a>
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
