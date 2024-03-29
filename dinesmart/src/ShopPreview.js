import './Dashboard.css'
function ShopPreview(props) {

    const imglink = props.imglink;
    const waitingnumber = props.waitingnumber;
    const shopname = props.shopname;
    const clickhandle = props.onClick;

    return (
        <div className="shopSquare" onClick={() => clickhandle()}>
            <div className="top bottom_left">{shopname}</div>
            <div className="middle">
                <img src={imglink} />
            </div>
            <div className="bottom">
                <div className="bottom_left">Number of people waiting: {waitingnumber}</div>
            </div>
        </div>
    )
}

export default ShopPreview;