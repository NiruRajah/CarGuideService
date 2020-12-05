import { Formik } from 'formik';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Alert, Button, Col, FormFeedback, FormGroup, Input, Row, Table } from 'reactstrap';
import * as Yup from 'yup'; 
import { apiMaintenance } from '../api/cars';



class Maintenance extends Component {

    constructor(props){
      super(props);
      this._handleFormSubmit = this._handleFormSubmit.bind(this);
      this.state = {
          Maintenance: [],
          messageError: ''
      }
  }


  _handleFormSubmit (values) {
    //   console.log(values)
      apiMaintenance(values)
      .then(res => {
        //   console.log(res.data.data)
        //   console.log(res.data.data)
        //   console.log(res.data.message.message)
    
          this.setState({
              Maintenance: res.data.data,
              messageError: res.data.message.message
          })
      })
      .catch(err => {
          console.log(err)
      })
  }
 
  _renderErrorIfAny() {
    if (this.state.messageError === 'Data Invaild') {
         return (
                <Alert color="danger" style={{ textAlign: 'center' }}>
                    {this.state.messageError}
                </Alert>
            );
    }
    if (this.state.messageError === 'ok')
    {
        return (
        <Alert color="success" style={{ textAlign: 'center' }}>
                    {this.state.messageError}
        </Alert>
    );
    }
  }

  render(){
    // console.log(this.state.Maintenance, this.state.messageError)
    const data = this.state.Maintenance;
    return (
    <div className="mt-5">
        <h3 style={{ textAlign: 'center' }}>Maintenance of Vehicle</h3>
        <hr />
            { this._renderErrorIfAny() }
        <Formik 
            initialValues={{ model: '', make: '', year: 0}}
            onSubmit={this._handleFormSubmit}
            validationSchema={Yup.object().shape({
                model: Yup.string().required(),
                make: Yup.string().required(),
                year: Yup.number().positive().required(),
            })}
        >
        { 
            ({handleChange,handleSubmit, isValid, isSubmitting, errors, touched}) => 
                    (
                       <>
                            <Row xs="3">
                            <Col>
                              <FormGroup>
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
                            </Col>
                            <Col>
                              <FormGroup>
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
                            </Col>
                            <Col>
                             <FormGroup>
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
                            </Col>
                            <Col>
                             <Button
                                onClick={handleSubmit}
                                style={{ marginTop: 20 }}
                                color="primary"
                                block
                               >
                                Submit
                            </Button>
                            </Col>
                            <Col>
                                <Link to="/home"><Button color="info" style={{ marginTop: 20 }} block>Return to Home</Button></Link>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                {
                                    this.state.messageError !== 'ok'? '' :
                                        <Table striped className="mt-5">
                                        <thead>
                                                <tr>
                                                    <th>Description</th>
                                                    <th>Cycle Mileage</th>
                                                    <th>Due Mileage</th>
                                                    <th>Repair</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            {
                                                data.map((item, index )=> (
                                                    <tr key={item.index}>
                                                        <td>{item.desc}</td>
                                                        <td>{item.cycle_mileage}</td>
                                                        <td>{item.due_mileage}</td>
                                                        <td>{item.repair.total_cost}</td>
                                                        {/* <td>
                                                            <thead>
                                                                <th>Labor cost</th>
                                                                <th>Misc cost</th>
                                                                <th>Part cost</th>
                                                                <th>Repair Hours</th>
                                                                <th>Total cost</th>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                {
                                                                    item.repair.map(value => (
                                                                        <td>{value.labor_cost}</td>
                                                                    ))
                                                                }
                                                                </tr>
                                                            </tbody>
                                                        </td> */}
                                                    </tr>
                                                ))
                                            }
                                            </tbody>
                                        </Table>
                                }
                            </Col>
                        </Row>
                       </>
                    )
                }
        </Formik>
    </div>
  );
  }
}
 
export default Maintenance;