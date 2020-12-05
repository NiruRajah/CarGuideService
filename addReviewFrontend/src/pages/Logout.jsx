import React, { Component } from 'react';
import { connect } from 'react-redux';
import {
  Button,
  FormGroup,
  Label,
  Input,
  FormFeedback,
  Alert,
  Badge,
  Row,
  Col
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { Formik } from 'formik';
import * as Yup from 'yup';

import { signUp } from '../actions/auth_actions';

class SignupPage extends Component {

  constructor(props){
      super(props);
      this._handleFormSubmit = this._handleFormSubmit.bind(this);
  }

  componentDidUpdate() {
    const { error, signedUp } = this.props;
    if (error && this.bag) {
      this.bag.setSubmitting(false);
    }

    if (signedUp) {
      this.props.history.push('/');
    }
  }

  _handleFormSubmit(values, bag) {
    this.props.signUp(values);
    this.bag = bag;
  }

  render() {
    const { error } = this.props;
    return (
      <div style={{ padding: 20 }}>
        <h3 style={{ textAlign: 'center' }}>Create new account</h3>
        {error && <Alert color="danger">{error}</Alert>}
        <hr />
        <Row>
         <Col sm="12" md={{ size: 6, offset: 3 }}>
           <Formik
            initialValues={{ username: '', email: '', password: '', firstname: '', lastname: '' }}
            onSubmit={this._handleFormSubmit}
            validationSchema={Yup.object().shape({
                name: Yup.string().required(),
                email: Yup.string()
                .email()
                .required(),
                password: Yup.string()
                .min(6)
                .required(),
                fisrtname:Yup.string().required(),
                lastname: Yup.string().required()
            })}>
            {({
            handleChange,
            handleSubmit,
            isValid,
            isSubmitting,
            handleBlur,
            errors,
            touched,
            }) => (
            <div>
              <FormGroup>
                <Label>Username</Label>
                <Input
                  invalid={errors.username && touched.username}
                  name="name"
                  type="string"
                  placeholder="Your Username"
                  onChange={handleChange}
                  onBlur={handleBlur}
                />

                {errors.name && touched.name && (
                  <FormFeedback>{errors.name}</FormFeedback>
                )}
              </FormGroup>
              <FormGroup>
                <Label>Email</Label>
                <Input
                  invalid={errors.email && touched.email}
                  name="email"
                  type="email"
                  placeholder="someone@abolkog.ca"
                  onChange={handleChange}
                  onBlur={handleBlur}
                />

                {errors.email && touched.email && (
                  <FormFeedback>{errors.email}</FormFeedback>
                )}
              </FormGroup>
              <FormGroup>
                <Label>Password</Label>
                <Input
                  invalid={errors.password && touched.password}
                  name="password"
                  type="password"
                  placeholder="Your Password"
                  onChange={handleChange}
                  onBlur={handleBlur}
                />
                {errors.password && touched.password && (
                  <FormFeedback>{errors.password}</FormFeedback>
                )}
              </FormGroup>
              <FormGroup>
                <Label>FirstName</Label>
                <Input
                  invalid={errors.firstname && touched.firstname}
                  name="fisrtname"
                  type="string"
                  placeholder="Your FirstName"
                  onChange={handleChange}
                  onBlur={handleBlur}
                />

                {errors.firstname && touched.firstname && (
                  <FormFeedback>{errors.firstname}</FormFeedback>
                )}
              </FormGroup>
              <FormGroup>
                <Label>LastName</Label>
                <Input
                  invalid={errors.lastname && touched.lastname}
                  name="lastname"
                  type="string"
                  placeholder="Your LastName"
                  onChange={handleChange}
                  onBlur={handleBlur}
                />

                {errors.lastname && touched.lastname && (
                  <FormFeedback>{errors.lastname}</FormFeedback>
                )}
              </FormGroup>
              <Button
                color="primary"
                block
                onClick={handleSubmit}
                disabled={!isValid || isSubmitting}
              >
                Create Account
              </Button>
            </div>
          )}
          </Formik>
            Have an account?
            <Link to="/"> 
                <Badge color="info">Sign In</Badge>
            </Link>
         </Col>
        </Row>
      </div>
    );
  }
}


const mapStateToProps = ({ auth }) => {
  return {
    error: auth.error,
    signedUp: auth.signedUp,
  };
};

const Logout = connect(
  mapStateToProps,
  { signUp }
)(SignupPage);
 
export {Logout};