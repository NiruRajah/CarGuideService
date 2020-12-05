import { Formik } from 'formik';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Col, FormFeedback, FormGroup, Input, Label, Row } from 'reactstrap';
import * as Yup from 'yup';
import axios from 'axios';

class AddCar extends Component {

  constructor(props){
      super(props);
      this._handleFormSubmit = this._handleFormSubmit.bind(this);
  }


  _handleFormSubmit (values) {
      axios.post('https://carguideserviceapi20201105030004.azurewebsites.net/CarGuideServiceAPI/vehicle', values)
      .then(res => {
          console.log(res)
          console.log(res.data)
      })
  }

  render(){
    return (
    <>
        <Row style={{ marginTop: 50 }}>
            <Col>
                <Formik 
                    initialValues={{ model: '', make: '', year: 0, size: 0, power: 0 , type: 0}}
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
                    ({handleChange,handleSubmit, isValid, isSubmitting, errors, touched}) => 
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
                                Add Car
                            </Button>
                            <Link to="/home"><Button color="info" style={{ marginTop: 10 }} block>Return to Home</Button></Link>
                        </div>
                    )
                }
                </Formik>
            </Col>
        </Row>
    </>
  );
  }
}


export default AddCar;