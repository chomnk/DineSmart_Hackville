import './Dashboard.css';

function Dashboard(props) {
    const shopToggle = props.shopToggle;

    const merchants = Array.from({ length: 20 }, (_, index) => index + 1);

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
                <div className="scrollableContainer">
                    {merchants.map((merchant) => (
                        <div key={merchant} className="shopSquare" onClick={() => shopToggle(true)}>Merchant {merchant}</div>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default Dashboard;