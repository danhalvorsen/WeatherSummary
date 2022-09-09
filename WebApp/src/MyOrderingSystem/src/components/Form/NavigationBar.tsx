function NavigationBar() {
  const basketNumber = 3;
  return (
    <>
      <div className=" border border-primary p-3">
        <a>Home</a> || <a>Link1</a> | <a>Link2</a> | <a>Link3</a>
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
