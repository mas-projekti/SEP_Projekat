import PropTypes from 'prop-types'

const Item = ({title, imgSrc, description, cost, ammount}) => {
    return (
        <div className="col" style={cardStyle}>
            <div className="card" >
                <img    src={imgSrc} 
                        className="card-img-top" 
                        alt=""
                        style={{width:'200px', height:'200px'}}/>
                <div className="card-body" style={cardStyle}>
                    <h5 className="card-title">{title}</h5>
                    <h3>{cost}</h3>
                    <h3>{ammount}</h3>
                    <p class="card-text">{description}</p>
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
  }

  const cardStyle = {
      color:'black'
  }

export default Item
