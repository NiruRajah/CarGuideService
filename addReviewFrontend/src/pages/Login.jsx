import React, { Component } from 'react';
import { Button, Col, FormFeedback, FormGroup, Input, Label, Row, Badge, Alert } from 'reactstrap';
import {Link} from 'react-router-dom';
import { connect } from 'react-redux';
import {Formik} from 'formik';
import * as Yup from 'yup';

import {signIn} from '../actions';


class LoginPage extends Component {
    // eslint-disable-next-line no-useless-constructor
    constructor(props){
        super(props);
        this._handleFormSubmit = this._handleFormSubmit.bind(this);
        this.state = {
            username: ''
        }
    }

    componentDidUpdate() {
        const { error, isAuth } = this.props;
        if (error && this.bag) {
            this.bag.setSubmitting(false);
        }
        if (isAuth) {
            this.props.history.push('/home');
        }
    }

    _handleFormSubmit(values, bag) {
        this.props.signIn(values);
        this.bag = bag;
        this.setState({
            username: values.username
        })
    }

    _renderErrorIfAny() {
        const { error } = this.props;
        if (error) {
            return (
                <Alert color="danger">
                    {error}
                </Alert>
            );
        }
    }

    render() { 
        return (
            <div style={{ padding: 20 }}>
                <h3 style={{ textAlign: 'center' }}>Sign in to your account</h3>
                <hr />
                <Row>
                  <Col sm="12" md={{ size: 6, offset: 3 }}>
                    { this._renderErrorIfAny() }
                    <Formik initialValues={{ username: '', password: '' }} 
                        onSubmit={this._handleFormSubmit}
                        validationSchema={Yup.object().shape({
                            username: Yup.string().required(),
                            password: Yup.string().min(4).required()
                    })}>
                    {({ handleChange, handleSubmit, isValid, isSubmitting, handleBlur, errors, touched }) => (
                            <FormGroup>
                              <Label>Username</Label>
                              <Input  name="username"
                                      invalid={errors.username && touched.username}
                                      type="string"
                                      placeholder="someone"
                                      onChange={handleChange}
                                      onBlur={handleBlur}
                              />
                                {
                                    errors.username && touched.username ? 
                                    <FormFeedback>{errors.username}</FormFeedback> :
                                    null
                                }
                              <Label>Password</Label>
                              <Input  name="password"
                                      invalid={errors.password && touched.password}
                                      type="password"
                                      placeholder="Your Password"
                                      onChange={handleChange}
                                      onBlur={handleBlur}
                              />
                                {
                                    errors.password && touched.password ? 
                                    <FormFeedback>{errors.password}</FormFeedback> :
                                    null
                                }
                             <Link to={{ pathname: '/home' }}>
                              <Button style={{ marginTop: 10 }} 
                                        color="primary" 
                                        block
                                        onClick={handleSubmit}
                                        disabled={!isValid || isSubmitting}
                                        >Sign In
                              </Button>
                            </Link>
                            </FormGroup>
                    )}
                    </Formik>
                    Do not have an account?
                   <Link to='/signup'> 
                        <Badge color="info">Sign Up Now</Badge>
                   </Link>
                   </Col>
                </Row>
            </div>
        );
    }
}


const mapStateToProps = ({ auth }) => {
    return {
        attempting: auth.attempting,
        error: auth.error,
        isAuth: auth.isAuth,
        profile: auth.profile
    };
};

const Login = connect(mapStateToProps, {signIn})(LoginPage);

export {Login};