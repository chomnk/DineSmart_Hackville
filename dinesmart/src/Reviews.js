function Reviews(props) {
    const infopack = props.props;

    const content = infopack.content;
    const username = infopack.username;
    const rating = infopack.rating;

    return (
        <div>
            <div>{username}</div>
            <div>{content}</div>
            <div>{rating}</div>
        </div>
    )

}

export default Reviews;