import ReactStars from "react-rating-stars-component";
import React from "react";
import { render } from "react-dom";
import './shop.css'
import { useState, useRef, useEffect } from "react";
import axios from "axios";
import Reviews from "./Reviews";

function Shop(props) {
    const shopInfo = props.shopInfo;

    const restaurantName = shopInfo.restaurantName;
    const imageLink = shopInfo.imageLink;
    const peopleInQueue = shopInfo.peopleInQueue;

    const shopToggle = props.shopToggle;

    const [queueButtonState, setQueueButtonState] = useState("Join the queue")
    const [queueButtonColor, setQueueButtonColor] = useState("green")

    const username = props.username;

    const textAreaRef = useRef(null);
    const [rating, SetRating] = useState(3);

    const [loading, setLoading] = useState(true);
    const [reviews, setReviews] = useState({});

    const handleQueuePressed = async () => {
        //poll isQueue first
        try {
            await axios.get("http://localhost:5166/api/Restaurant/allusers");
            const response = await axios.get('http://localhost:5166/api/Restaurant/isQueue/' + username)
            if (response.data == "Not in Queue") {
                setQueueButtonState("Exit the queue"); 
                setQueueButtonColor("red")
                alert("You have joined the queue!")
            } else {
                setQueueButtonState("Join the queue"); 
                setQueueButtonColor("green"); 
                alert("You have exit the queue!")
            }
            const response_updateQueue = await axios.post('http://localhost:5166/api/Restaurant/queue/' + username + '/' + restaurantName);

        } catch (error) {
            console.error(error);
        } finally {
                
        }
    }

    const handleReviewSubmit = async () => {
        const element = textAreaRef.current;
        const text = element.value;
        try {
            const response = await axios.post('http://localhost:5166/api/Restaurant/newReview/' + username + '/' + restaurantName + '/' + text + '/' + String(rating));
            alert("You have submitted the review!")
        } catch (error) {
            console.error(error);
        } finally {
            element.value = "";
        }
    }

    const fetchReviews = async () => {
        try {
            const response = await axios.get('http://localhost:5166/api/Restaurant/findreview/' + restaurantName);
            setReviews(response.data);
        } catch (error) {
            console.error(error);
        } finally {
            
        }
    }

    useEffect(() => {
        fetchReviews().then(data => { setLoading(false); console.log(reviews); });
    }, []);

    return (
        <div className="shop">
            <div className="shop_left">
                <div className="shop_left_top">
                    {restaurantName}
                </div>
                <div className="shop_left_middle">
                    <img src={imageLink} />
                </div>
                <div className="shop_left_bottom" onClick={() => shopToggle(null)}>
                    Back
                </div>
            </div>
            <div className="shop_right">
                <div className="shop_right_top">
                    <div className="shop_right_top_left">Leave a rating!</div>
                    <div className="shop_right_top_right">
                        <ReactStars  
                            style={{'height':"200%"}}
                            count={5}
                            size={24}    
                            activeColor="#ffd700"
                            onChange={SetRating}
                        ></ReactStars>
                    </div>
                </div>
                {loading ? (
                    <p>Loading...</p>
                ) : <div className="shop_right_middle">
                        {Object.entries(reviews).map(([key, value], index) => {
                            (<Reviews username={key} content={value["key"]} key={index} rating={value["value"]} />)
                        })}
                </div>}
                <div className="shop_right_bottom">
                    <textarea ref={textAreaRef} className="shop_right_bottom_left" placeholder="Leave some review for this food option!"/>
                    <div className="shop_right_bottom_right">
                        <div 
                            className='shop_review_queue' 
                            style={{'background-color':queueButtonColor}} onClick={() => {handleQueuePressed()}}>
                            {queueButtonState}
                        </div>
                        <div className='shop_review_submit' onClick={() => {handleReviewSubmit()}}>
                            Submit review!
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Shop;