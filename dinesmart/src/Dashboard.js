import { useState, useEffect } from 'react';
import './Dashboard.css';
import ShopPreview from './ShopPreview';
import axios from 'axios';

function Dashboard(props) {
    const shopToggle = props.shopToggle;
    const [loading, setLoading] = useState(true);
    
    const [shops, setShops] = useState([]);

    const fetchData = async () => {
        try {
            const response = await axios.get("http://localhost:5166/api/Restaurant");
            console.log(response);
            setShops(response.data);
        } catch (error) {
            console.error(error);
        } finally {
            
        }
    }

    //setShops(data);

    useEffect(() => {
        fetchData().then(data => { setLoading(false);});
    }, []);

    return (
        
        <div className="App">
            <div className="leftDiv">
                <div className="topComponent">
                    DineSmart
                </div>
                <div className="middleComponent">
                    Smart Dining Choices for Students
                </div>  
                <div className="bottomComponent">
                    <select className="bottomComponentDropdown">
                        <option value="option1">Option 1</option>
                        <option value="option2">Option 2</option>
                        <option value="option3">Option 3</option>
                    </select>
                </div>
            </div>
            <div className="rightDiv">
                {loading ? (
                    <p>Loading...</p>
                ) : (
                <div className="scrollableContainer">
                    {shops.map((shop, index) => (
                        <ShopPreview 
                            key={index}
                            imglink={shop.imageLink} 
                            //avgprice={shop.avgprice} 
                            waitingnumber={shop.peopleInQueue}
                            shopname={shop.restaurantName}
                            onClick={() => shopToggle(true)}
                            className="shopSquare"
                        />
                    ))}
                </div>)}
            </div>
        </div>
    );
}

export default Dashboard;