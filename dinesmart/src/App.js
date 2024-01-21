import logo from './logo.svg';
import './App.css';
import Shop from './Shop.js';
import Dashboard from './Dashboard.js';
import { useState } from 'react';

function App() {
    const [isShopClicked, setIsShopClicked] = useState(null);
    let contentToRender = <Dashboard shopToggle={setIsShopClicked}/>
    if (isShopClicked) contentToRender = 
    <Shop 
        shopInfo={isShopClicked} 
        username={"teng"}
        shopToggle={setIsShopClicked}/>

    return (
        <>{contentToRender}</>
    );
}

export default App;
