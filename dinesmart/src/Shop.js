import ReactStars from "react-rating-stars-component";
import React from "react";
import { render } from "react-dom";
import './shop.css'

function Shop(props) {
    const shopId = props.shopId;
    const shopToggle = props.shopToggle;
    

    const handleQueuePressed = () => {

    }

    return (
        <div className="shop">
            <div className="shop_left">
                <div className="shop_left_top">
                    {/*TODO: insert shop name here*/}
                </div>
                <div className="shop_left_middle">
                    {/*TODO: insert shop sentimental analysis here*/}
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
                        ></ReactStars>
                    </div>
                </div>
                <div className="shop_right_middle"></div>
                <div className="shop_right_bottom">
                    <textarea className="shop_right_bottom_left" placeholder="Type something..."/>
                    <div className="shop_right_bottom_right">
                        <div className='shop_review_queue' onClick={() => {
                            
                        }}>
                            Join the queue
                        </div>
                        <div className='shop_review_submit'>
                            Submit review!
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Shop;