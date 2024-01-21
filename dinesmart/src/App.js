
import './App.css';
import Shop from './Shop.js';
import Dashboard from './Dashboard.js';
import { useState } from 'react';
import Authentification from './Authentification.js';

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState(false)
    const [isShopClicked, setIsShopClicked] = useState(null);

    let contentToRender = <Authentification setIsLoggedIn={setIsLoggedIn}/>;

    if (isLoggedIn) contentToRender = <Dashboard shopToggle={setIsShopClicked}/>
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
