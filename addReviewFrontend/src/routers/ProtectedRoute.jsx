import React from 'react';
import { connect } from 'react-redux';
import {Route, Redirect} from 'react-router-dom';
 
const ProtectedRouteComponent = ({ isAuth, component: Component, ...rest }) => {
    return (
        <Route 
            {...rest}
            render={ props => isAuth ? <Component {...props} /> : <Redirect  to="/" /> }
        />
    );
}

const mapStateToProps = ({ auth }) => {
    return {
        isAuth: auth.isAuth
    }
}
 
const ProtectedRoute = connect(mapStateToProps)(ProtectedRouteComponent);

export default ProtectedRoute;