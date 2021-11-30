import PropTypes from 'prop-types'
import "./../styles/Item.css"

const Item = ({title, imgSrc, description, cost, ammount, onClick}) => {

    return (
        <div className="col cardHover" style={{cursor: 'pointer'}} onClick={onClick}>
            <div className="card" >
            <div className="card-body" style={cardStyle}>
                <h3 className="card-title">{title}</h3>
            </div>
                <img    src={imgSrc} 
                        className="card-img-top" 
                        alt=""
                        style={{height:'200px'}}/>
                <div className="card-body" style={cardStyle}>
                    <h5>Cost: {cost}</h5>
                    <h5>Ammount available: {ammount}</h5>
                    <p className="card-text" style={{textOverflow:'ellipsis', overflow:'hidden', height:'30px'}}> Description: gfd gdg dfgfd gfd gdf {description}</p>
                </div>
            </div>
        </div>
    )
}

Item.defaultProps = {
    title: 'Item default',
    imgSrc: 'imgSrc default',
    description: 'Description default',
    cost: 'Cost default',
    ammount: 'Ammount default',
  }
  
  // After connecting to server set to .isRequired
  Item.propTypes = {
    title: PropTypes.string,
    imgSrc: PropTypes.string,
    description: PropTypes.string,
    cost: PropTypes.number,
    ammount: PropTypes.number,

    onClick: PropTypes.func
  }

  const cardStyle = {
      color:'black',
      cursor:'pointer',
      transition: 'all 0.3s ease 0s',
  }

export default Item
