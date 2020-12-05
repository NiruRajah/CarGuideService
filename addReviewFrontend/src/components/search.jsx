import React, { Component } from 'react';
import { Formik } from 'formik';
import { Link } from 'react-router-dom';
import { Alert, Button, Col, FormFeedback, FormGroup, Input, Label, Row} from 'reactstrap';
import * as Yup from 'yup';
import { apiBestVehicle } from '../api/cars';

class search extends Component {

   // eslint-disable-next-line no-useless-constructor
   constructor(props){
       super(props);
       this._handleFormSubmit = this._handleFormSubmit.bind(this);
       this.state = {
           bestVehicle : [],
           message: '',
           isVide: true,
           id: null
       }
   }

   _handleFormSubmit(values) {
        // console.log(values)
        apiBestVehicle(values)
        .then(res => {
            console.log(res)
            this.setState({
                bestVehicle: res.data,
                message: 'Your search was successful',
                isVide : false,
                id: res.data.id
            })
        })
        .catch(err => {
            console.log(err)
        });
   }
    _renderErrorIfAny(){
        if (!this.state.isVide || this.state.id !== null) {
            return (
                <Alert color="success" style={{ textAlign: 'center' }}>
                    {this.state.message}
                </Alert>
            );
        }
        return (
            <Alert color="warning" style={{ textAlign: 'center' }}>
              Start Your Search
            </Alert>
        );
    }

   render() {
       const DataBestVehicle = this.state.bestVehicle
    //    console.log(DataBestVehicle)
        return (
        <div style={{ padding: 20 }}>
            <h3 style={{ textAlign: 'center' }}>Finding The Best Vehicle</h3>
            <hr />
            { this._renderErrorIfAny() }
            <div xs="2" className="mt-4">
                <Formik
                  initialValues={{  year: 0, luxuryLevel: 0, size: 0, priceRange: 0, type: 0, fuelEfficiency: 0,  power: 0 , handling: 0, safety: 0, reliability: 0, steeringFeelAndResponse: 0, comfortLevel: 0,
                                   rideQuality: 0, buildQuality: 0, technology: 0, styling: 0, resaleValue: 0 }}
                    onSubmit={this._handleFormSubmit}
                    validationSchema={Yup.object().shape({
                        year: Yup.number(),
                        luxuryLevel: Yup.number(),
                        size: Yup.number(),
                        priceRange: Yup.number(),
                        type: Yup.number(),
                        fuelEfficiency: Yup.number(),
                        power: Yup.number(),
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
                                <Label>Luxury Level</Label>
                                <Input
                                invalid={errors.luxuryLevel && touched.luxuryLevel}
                                name="luxuryLevel"
                                type="number"
                                placeholder="From 0 to 2"
                                onChange={handleChange}
                                >
                                </Input>
                                {errors.luxuryLevel && touched.luxuryLevel && (
                                <FormFeedback>{errors.luxuryLevel}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Size</Label>
                                <Input
                                invalid={errors.size && touched.size}
                                name="size"
                                type="number"
                                placeholder="From 0 to 3"
                                onChange={handleChange}
                                />
                                {errors.size && touched.size && (
                                <FormFeedback>{errors.size}</FormFeedback>
                                )}
                            </FormGroup>
                              <FormGroup>
                                <Label>Price Range</Label>
                                <Input
                                invalid={errors.priceRange && touched.priceRange}
                                name="priceRange"
                                type="number"
                                placeholder="Form 0 to 4"
                                onChange={handleChange}
                                />
                                {errors.priceRange && touched.priceRange && (
                                <FormFeedback>{errors.priceRange}</FormFeedback>
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
                                <Label>Type</Label>
                                <Input
                                invalid={errors.type && touched.type}
                                name="type"
                                type="number"
                                placeholder="From 0 to 5"
                                onChange={handleChange}
                                />
                                {errors.type && touched.type && (
                                <FormFeedback>{errors.type}</FormFeedback>
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
                           </Col>
                           <Col>
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
                                    Search Your Best Vehicle
                                </Button>
                                <Link to="/home"><Button color="info" size="lg" style={{ marginTop: 10 }} block>Return to Home</Button></Link>
                                <Link to={ !this.state.isVide ? { pathname: "/SeeYourBestVehicle", state: {DataBestVehicle} } : '/home' }><Button size="lg" style={{ marginTop: 10 }} block>See Your Best Vehicle</Button></Link>
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
 
export default search;



