import { Formik } from 'formik';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Col, FormFeedback, FormGroup, Input, Label, Row, Table } from 'reactstrap';
import * as Yup from 'yup';
import axios from 'axios';

class updateCar extends Component {

    constructor(props){
      super(props);
      this._handleFormSubmit = this._handleFormSubmit.bind(this);
      this.state = {car: this.props.location.state.car}
    }

    _handleFormSubmit(values, bag) {
        console.log(values, this.state.car);
        let id = this.state.car.id;
        this.setState({
            car: values
        })
        console.log(values, this.state.car); 
        console.log(id);
        axios.put(`https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehicle/${id}`, {values})
        .then(res => {
            console.log(res)
        })
        .catch(err => {
            console.error(err); 
        })
    }

    render(){
        let car;
        try {
            car = this.props.location.state.car;
        } catch(e) {
            car = undefined;
        }
        if (!car) this.props.history.push('/home');
        const {model, make, year, size, power, type} = car;
        return (
            <Row style={{ marginTop: 50 }}>
              <Col>
                <h3 style={{ textAlign: 'center' }}>Edit Car</h3>
                <hr />
                <Table striped style={{ marginTop: 30 }}>
                    <thead>
                        <tr>
                            <th>Make</th>
                            <th>Model</th>
                            <th>Year</th>
                            <th>Size</th>
                            <th>Power</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr key={car.id}>
                            <th>{car.make}</th>
                            <td>{car.model}</td>
                            <td>{car.year}</td>
                            <td>{car.size}</td>
                            <td>{car.power}</td>
                            <td>{car.type}</td>
                        </tr>
                    </tbody>
                </Table>
                <Formik 
                    initialValues={{model, make, year, size, power, type}}
                    onSubmit={this._handleFormSubmit}
                    validationSchema={Yup.object().shape({
                        model: Yup.string().required(),
                        make: Yup.string().required(),
                        year: Yup.number().positive(),
                        size: Yup.number().positive(),
                        power: Yup.number().positive().integer(),
                        type: Yup.number().positive().integer()
                    })}
                >
                { 
                    ({handleChange,handleSubmit, isValid, isSubmitting, errors, touched, handleBlur, setFieldValue}) => 
                    (
                        <div>
                            <FormGroup>
                                <Label>Model</Label>
                                <Input
                                invalid={errors.model && touched.model}
                                name="model"
                                type="string"
                                placeholder="Model"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                />
                                {errors.model && touched.model && (
                                <FormFeedback>{errors.model}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Make</Label>
                                <Input
                                invalid={errors.make && touched.make}
                                name="make"
                                type="string"
                                placeholder="Make"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                />
                                {errors.make && touched.make && (
                                <FormFeedback>{errors.make}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Year</Label>
                                <Input
                                invalid={errors.year && touched.year}
                                name="year"
                                type="number"
                                placeholder="Year"
                                onChange={handleChange}
                            
                                onBlur={handleBlur}
                                />
                                {errors.year && touched.year && (
                                <FormFeedback>{errors.year}</FormFeedback>
                                )}
                            </FormGroup>
                             <FormGroup>
                                <Label>Size</Label>
                                <Input
                                invalid={errors.size && touched.size}
                                name="size"
                                type="number"
                                placeholder="Size"
                                onChange={handleChange}
                              
                                onBlur={handleBlur}
                                />
                                {errors.size && touched.size && (
                                <FormFeedback>{errors.size}</FormFeedback>
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
                            
                                onBlur={handleBlur}
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
                                placeholder="Type"
                                onChange={handleChange}
                               
                                onBlur={handleBlur}
                                />
                                {errors.type && touched.type && (
                                <FormFeedback>{errors.type}</FormFeedback>
                                )}
                            </FormGroup>
                            <Button
                                onClick={handleSubmit}
                                style={{ marginTop: 20 }}
                                color="primary"
                                block
                            >
                                Edit Car
                            </Button>
                            <Link to="/home"><Button color="info" style={{ marginTop: 10 }} block>Return to Home</Button></Link>
                        </div>
                    )
                }
                </Formik>
            </Col>
           </Row>
        );
    }
}
 
 
export default updateCar;