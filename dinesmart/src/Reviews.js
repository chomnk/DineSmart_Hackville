import "./Reviews.css"

function Reviews(props) {

    const content = props.content;
    const username = props.username;
    const rating = props.rating;

    return (
        <div className="review">
            <div>{username}</div>
            <div>{content}</div>
            <div>{rating}</div>
        </div>
    )

}

export default Reviews;