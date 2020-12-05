import React, { Component } from 'react';
import { Button, Table } from 'reactstrap';
import { apiViewsVicule } from '../api/cars';
import { Link } from 'react-router-dom';
import {connect} from 'react-redux';

class allUserPage extends Component {

    // eslint-disable-next-line no-useless-constructor
    constructor(props){
        super(props);
        this.state = {
            vehicleUsers: []
        };
    }

    componentDidMount(){
        const {profile} = this.props;
        apiViewsVicule(profile.userName)
        .then(res => {
            console.log(res)
          const vehicleUsers = res.data ;
          this.setState({ vehicleUsers });
        })
        .catch(err => {
            console.log(err);
        });
    }
    
    render() { 
        const vehicles = this.state.vehicleUsers
        return (
          <>
            <Table striped className="mt-5">
                <thead>
                    <tr>
                    <th>Year</th>
                    <th>Make</th>
                    <th>Model</th>
                    </tr>
                </thead>
                <tbody>
                   {
                       vehicles.map(vehicle => (
                           <tr key={vehicle.id}>
                                <td>{vehicle.year}</td>
                                <td>{vehicle.make}</td>
                                <td>{vehicle.model}</td>
                           </tr>
                       ))
                   }
                </tbody>
            </Table>
           <Link to="/home"><Button color="info" size="lg">Return to Home</Button></Link>
          </>
        );
    }
}


const mapStateToProps = ({ auth }) => {
    return {
        isAuth: auth.isAuth,
        profile: auth.profile
    };
};


const allUser = connect(mapStateToProps, {apiViewsVicule} )(allUserPage)

export  {allUser};