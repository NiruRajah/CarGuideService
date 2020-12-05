import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Badge, Button, Col, ListGroup, ListGroupItem, Row } from 'reactstrap';
 
class SeeYourAddReview extends Component {

    render() { 
        let data = this.props.history.location.state.DataReview
        const newData = JSON.parse(data);
        return (
            <div style={{ padding: 20 }}>
                <h3 style={{ textAlign: 'center' }}>the data goes through <strong>{newData.userName}</strong></h3>
                <hr />
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                         <ListGroup>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Model <Badge pill>{newData.model}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Make <Badge pill>{newData.make}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Year <Badge pill>{newData.year}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Build Quality <Badge pill>{newData.buildQuality}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Handling <Badge pill>{newData.handling}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Technology <Badge pill>{newData.technology}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Comfort Level <Badge pill>{newData.comfortLevel}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Styling <Badge pill>{newData.styling}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Resale Value <Badge pill>{newData.resaleValue}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Power <Badge pill>{newData.power}</Badge></ListGroupItem>
                            <ListGroupItem className="justify-content-between" style={{ textAlign: 'center' }}>Size <Badge pill>{newData.size}</Badge></ListGroupItem>
                        </ListGroup>
                        <Link to='/home'><Button size="lg" style={{ marginTop: 10 }} block>Return to Home</Button></Link>
                    </Col>

                </Row>
            </div>
        );
    }
}
 
export default SeeYourAddReview;
