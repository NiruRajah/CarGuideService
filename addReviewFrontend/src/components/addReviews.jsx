import { Formik } from 'formik';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Alert, Button, Col, FormFeedback, FormGroup, Input, Label, Row} from 'reactstrap';
import * as Yup from 'yup';
import { apiPostViewsVehicule } from '../api/cars';

class addReviews extends Component {
    /**
     * username
     * input year make model all options
     * passed object {username, year, make, ...options}
     * return ...options not (username, type)
     */
    // eslint-disable-next-line no-useless-constructor
    constructor(props) {
        super(props);
        this._handleFormSubmit = this._handleFormSubmit.bind(this);
        this.state = {
            addVehiculeReview : {},
            message: ''
        }
    }

    _handleFormSubmit(values)
    {
        let username = this.props.match.params.id;
        values.userName = username;
        console.log(values);
        apiPostViewsVehicule(values)
        .then(res => {
            console.log(res.config.data)
            console.log(res.data)
            this.setState({
                addVehiculeReview: res.config.data,
                message: res.data
            });
        })
        .catch(err => {
            console.log(err);
        });
    }

    _renderErrorIfAny() {
        const message = this.state.message;
        if (message) {
            return (
                <Alert color="info" style={{ textAlign: 'center' }}>
                    {message}
                </Alert>
            );
        }
    }

    render() { 
        const  DataReview = this.state.addVehiculeReview;
        return (
        <div style={{ padding: 20 }}>
            <h3 style={{ textAlign: 'center' }}>Adding Vehicule Review by <strong>{this.props.match.params.id}</strong></h3>
            <hr />
            { this._renderErrorIfAny() }
            <div xs="2" className="mt-4">
                <Formik
                  initialValues={{ model: '', make: '', year: 0, power: 0 ,
                                   fuelEfficiency: 0, handling: 0, safety: 0,
                                   reliability: 0, steeringFeelAndResponse: 0, comfortLevel: 0,
                                   rideQuality: 0, buildQuality: 0, technology: 0, styling: 0, resaleValue: 0
                                     }}
                    onSubmit={this._handleFormSubmit}
                    validationSchema={Yup.object().shape({
                        model: Yup.string().required(),
                        make: Yup.string().required(),
                        year: Yup.number(),
                        power: Yup.number(),
                        fuelEfficiency: Yup.number(),
                        handling: Yup.number(),
                        safety: Yup.number(),
                        reliability: Yup.number(),
                        steeringFeelAndResponse: Yup.number(),
                        comfortLevel: Yup.number(),
                        rideQuality: Yup.number(),
                        buildQuality: Yup.number(),
                        technology: Yup.number(),
                        styling: Yup.number(),
                        resaleValue: Yup.number()
                    })}
                >
                { 
                    ({handleChange,handleSubmit, isValid, isSubmitting, errors, touched}) => 
                    (
                        <div>
                        <Row xs="3" className="mt-5">
                            <Col>
                             <FormGroup>
                                <Label>Year</Label>
                                <Input
                                invalid={errors.year && touched.year}
                                name="year"
                                type="number"
                                placeholder="Year"
                                onChange={handleChange}
                                />
                                {errors.year && touched.year && (
                                <FormFeedback>{errors.year}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Make</Label>
                                <Input
                                invalid={errors.make && touched.make}
                                name="make"
                                type="string"
                                placeholder="Make"
                                onChange={handleChange}
                                />
                                {errors.make && touched.make && (
                                <FormFeedback>{errors.make}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Model</Label>
                                <Input
                                invalid={errors.model && touched.model}
                                name="model"
                                type="string"
                                placeholder="Model"
                                onChange={handleChange}
                                />
                                {errors.model && touched.model && (
                                <FormFeedback>{errors.model}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Power</Label>
                                <Input
                                invalid={errors.power && touched.power}
                                name="power"
                                type="number"
                                placeholder="Power"
                                onChange={handleChange}
                                />
                                {errors.power && touched.power && (
                                <FormFeedback>{errors.power}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Fuel Efficiency</Label>
                                <Input
                                invalid={errors.fuelEfficiency && touched.fuelEfficiency}
                                name="fuelEfficiency"
                                type="number"
                                placeholder="Fuel Efficiency"
                                onChange={handleChange}
                                />
                                {errors.fuelEfficiency && touched.fuelEfficiency && (
                                <FormFeedback>{errors.fuelEfficiency}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Handling</Label>
                                <Input
                                invalid={errors.handling && touched.handling}
                                name="handling"
                                type="number"
                                placeholder="Handlingy"
                                onChange={handleChange}
                                />
                                {errors.handling && touched.handling && (
                                <FormFeedback>{errors.handling}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Safety</Label>
                                <Input
                                invalid={errors.safety && touched.safety}
                                name="safety"
                                type="number"
                                placeholder="Safety"
                                onChange={handleChange}
                                />
                                {errors.safety && touched.safety && (
                                <FormFeedback>{errors.safety}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Reliability</Label>
                                <Input
                                invalid={errors.reliability && touched.reliability}
                                name="reliability"
                                type="number"
                                placeholder="Reliability"
                                onChange={handleChange}
                                />
                                {errors.reliability && touched.reliability && (
                                <FormFeedback>{errors.reliability}</FormFeedback>
                                )}
                             </FormGroup>
                           </Col>
                           <Col>        
                            <FormGroup>
                                <Label>Steering Feel And Response</Label>
                                <Input
                                invalid={errors.steeringFeelAndResponse && touched.steeringFeelAndResponse}
                                name="steeringFeelAndResponse"
                                type="number"
                                placeholder="Steering Feel And Response"
                                onChange={handleChange}
                                />
                                {errors.steeringFeelAndResponse && touched.steeringFeelAndResponse && (
                                <FormFeedback>{errors.steeringFeelAndResponse}</FormFeedback>
                                )}
                            </FormGroup>
                            <FormGroup>
                                <Label>Comfort Level</Label>
                                <Input
                                invalid={errors.comfortLevel && touched.comfortLevel}
                                name="comfortLevel"
                                type="number"
                                placeholder="Comfort Level"
                                onChange={handleChange}
                                />
                                {errors.comfortLevel && touched.comfortLevel && (
                                <FormFeedback>{errors.comfortLevel}</FormFeedback>
                                )}
                            </FormGroup>
                            <FormGroup>
                                <Label>Ride Quality</Label>
                                <Input
                                invalid={errors.rideQuality && touched.rideQuality}
                                name="rideQuality"
                                type="number"
                                placeholder="Ride Quality"
                                onChange={handleChange}
                                />
                                {errors.rideQuality && touched.rideQuality && (
                                <FormFeedback>{errors.rideQuality}</FormFeedback>
                                )}
                            </FormGroup>
                            <FormGroup>
                                <Label>Build Quality</Label>
                                <Input
                                invalid={errors.buildQuality && touched.buildQuality}
                                name="buildQuality"
                                type="number"
                                placeholder="Build Quality"
                                onChange={handleChange}
                                />
                                {errors.buildQuality && touched.buildQuality && (
                                <FormFeedback>{errors.buildQuality}</FormFeedback>
                                )}
                            </FormGroup>
                            <FormGroup>
                                <Label>Technology</Label>
                                <Input
                                invalid={errors.technology && touched.technology}
                                name="technology"
                                type="number"
                                placeholder="Technology"
                                onChange={handleChange}
                                />
                                {errors.technology && touched.technology && (
                                <FormFeedback>{errors.technology}</FormFeedback>
                                )}
                            </FormGroup>
                            <FormGroup>
                                <Label>Styling</Label>
                                <Input
                                invalid={errors.styling && touched.styling}
                                name="styling"
                                type="number"
                                placeholder="Styling"
                                onChange={handleChange}
                                />
                                {errors.styling && touched.styling && (
                                <FormFeedback>{errors.styling}</FormFeedback>
                                )}
                            </FormGroup>
                            <FormGroup>
                                <Label>Resale Value</Label>
                                <Input
                                invalid={errors.resaleValue && touched.resaleValue}
                                name="resaleValue"
                                type="number"
                                placeholder="Resale Value"
                                onChange={handleChange}
                                />
                                {errors.resaleValue && touched.resaleValue && (
                                <FormFeedback>{errors.resaleValue}</FormFeedback>
                                )}
                            </FormGroup> 
                           </Col>
                           <Col>
                                <Button
                                className="mt-5"
                                onClick={handleSubmit}
                                style={{ marginTop: 20 }}
                                color="primary"
                                size="lg"
                                block
                                >
                                    Add Your Review
                                </Button>
                                <Link to="/home"><Button color="info" size="lg" style={{ marginTop: 10 }} block>Return to Home</Button></Link>
                                <Link to={ DataReview.length > 0 ? { pathname: "/SeeYourAddReview", state: {DataReview} } : '/home' }><Button size="lg" style={{ marginTop: 10 }} block>See Your Data Review</Button></Link>
                           </Col>
                        </Row>
                        </div>
                    )
                }
                </Formik>
            </div>
        </div>
        );
    }
}
 

export {addReviews};