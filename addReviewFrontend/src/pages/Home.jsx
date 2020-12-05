import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Col, Row, Table} from 'reactstrap';
import { getCars } from '../api/cars';
import axios from 'axios';
import { connect } from 'react-redux';


class HomePage extends Component {

    constructor(props){
        super(props);
        this.table = this.table.bind(this);
        this.onDelete = this.onDelete.bind(this);
        // this.onUpdate = this.onUpdate.bind(this);
        this.state = {
            cars: []
        }
    }

    componentDidMount(){
        getCars().then(res => {
            const cars = res.data;
            this.setState({
                cars
            });
        });
        // console.log(this.props.location.state)
    }

    componentDidUpdate() {
        // const {profile, isAuth} = this.props;
    }

    onDelete(id){
        axios.delete(`https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehicle/${id}`).then(res => {
            console.log(res);
            console.log(res.data)
            window.location.reload(false);
        });
    }

    // onUpdate(car){
    //     this.props.location.state = car
    //     this.props.history.push('/update/'+car.id)
    // }

    table(cars, username) {
        return (
          <>
          {/* <!--/CarGuideServiceAPI/vehicle/criterias = Find The Best Car For Your Criterias--> */}
            <Link to="/search"><Button color="warning" size="lg" style={{ margin: 10, textAlign: 'center' }}>Best Cars</Button></Link>
            <Link to={{ pathname: `/addReviews/${username}`}}><Button color="info" style={{ marginRight: 10, textAlign: 'center' }} size="lg">Add Review</Button></Link>
            {/* <!--POST/CarGuideServiceAPI/vehiclereview = Post A Car Review--> */} 
            <Link to="/addToCar"><Button color="primary" style={{ marginRight: 10, textAlign: 'center' }} size="lg">Add Car</Button></Link>
            <Link to="/all_Users"><Button color="success" style={{ marginRight: 10, textAlign: 'center' }}  size="lg">See Review User</Button></Link>
            <Link to="/maintenance"><Button color="secondary" className="mr-auto" size="lg">Maintenance</Button></Link>
            <Table striped>
                <thead>
                    <tr>
                        <th>Make</th>
                        <th>Model</th>
                        <th>Year</th>
                        <th>Number Of Reviews</th>
                        {/* <th>Actions</th> */}
                    </tr>
                </thead>
                <tbody>
                   {
                       cars.map(car => (
                        <tr key={car.id}>
                            <th>{car.make}</th>
                            <td>{car.model}</td>
                            <td>{car.year}</td>
                            <td>{car.numberOfReviews}</td>
                            {/* <td>
                                <Link to={{ pathname: `/update/${car.id}`, state: {car} }}>
                                    <Button color="primary" style={{ marginRight: 10 }} size="sm">Edit</Button>
                                </Link>
                                <Button color="danger" size="sm" onClick={() => {this.onDelete(car.id)}}>Delete</Button>
                            </td> */}
                        </tr>
                       ))
                   }
                </tbody>
            </Table>
          </>
        );
    }
    render() {
        // console.log(this.props.profile)
        let username = this.props.profile.userName;
        return (
            <Row style={{ marginTop: 20 }}>
              <Col>
                {this.table(this.state.cars, username)}
              </Col>
            </Row>
        );
    }
}


const mapStateToProps = ({ auth }) => {
    return {
        isAuth: auth.isAuth,
        profile: auth.profile
    };
};

const Home = connect(mapStateToProps)(HomePage);
export {Home};