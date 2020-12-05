import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Badge, Button, Col, ListGroup, ListGroupItem, Row } from 'reactstrap';
 
class SeeYourBestVehicle extends Component {
    render() { 
        let data = this.props.history.location.state.DataBestVehicle;
        console.log(data)
        return (
            <div style={{ padding: 20 }}>
                <h3 style={{ textAlign: 'center' }}>This Is Your Favorite Vehicle</h3>
                <hr />
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                         <ListGroup>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Model  <Badge pill>{data.model}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Make <Badge pill>{data.make}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Year <Badge pill>{data.year}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Build Quality <Badge pill>{data.buildQuality}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Handling <Badge pill>{data.handling}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Technology <Badge pill>{data.technology}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Comfort Level <Badge pill>{data.comfortLevel}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Styling <Badge pill>{data.styling}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Resale Value <Badge pill>{data.resaleValue}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Power <Badge pill>{data.power}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Size <Badge pill>{data.size}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Luxury Level <Badge pill>{data.luxuryLevel}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Number Of Reviews <Badge pill>{data.numberOfReviews}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Price Range <Badge pill>{data.priceRange}</Badge></ListGroupItem>
                        </ListGroup>
                        <Link to='/home'><Button size="lg" style={{ marginTop: 10 }} block>Return to Home</Button></Link>
                    </Col>

                </Row>
            </div>
        );
    }
}
 
export default SeeYourBestVehicle;

