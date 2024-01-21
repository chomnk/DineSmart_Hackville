import './Dashboard.css'
function ShopPreview(props) {
    const imglink = props.imglink;
    //const avgprice = props.avgprice;
    //<div className="bottom_right">{avgprice}</div>
    const waitingnumber = props.waitingnumber;
    const shopname = props.shopname;
    const clickhandle = props.onClick;
    return (
        <div className="shopSquare" onClick={() => clickhandle()}>
            <div className="top">{shopname}</div>
            <div className="middle">
                <img src={imglink} />
            </div>
            <div className="bottom">
                <div className="bottom_left">{waitingnumber}</div>
                
            </div>
        </div>
    )
}

export default ShopPreview;