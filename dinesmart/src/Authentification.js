import "./Authentification.css"
import React, { useState } from 'react';
import axios from "axios";

const Authentification = (props) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const setIsLoggedIn = props.setIsLoggedIn;

  const handleUsernameChange = (e) => {
    setUsername(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
        const response = await axios.get("http://localhost:5166/api/Restaurant/checklogin/" + username + '/' + password);
        
        if (response.data = "Granted") {
            setIsLoggedIn(username);
        }
    } catch (error) {
        console.error(error);
    } finally {  

    }
  };

  return (
    <div className="LoginForm">
      <div className="LoginForm_title">DineSmart</div>
      <form onSubmit={(e) => handleSubmit(e)}>
        <label>
          Username:
          <input type="text" value={username} onChange={handleUsernameChange} />
        </label>
        <br />
        <label>
          Password:
          <input type="password" value={password} onChange={handlePasswordChange} />
        </label>
        <br />
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default Authentification;
