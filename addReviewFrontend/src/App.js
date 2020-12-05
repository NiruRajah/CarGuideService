import React, { Component, Fragment } from 'react';
import { Container} from 'reactstrap';
import { NavBar } from './components/Navbar';
import RouteApp from './routers/RouteApp';

class App extends Component {
  render() {
    return (
      <Fragment>
        <NavBar />
        <Container>
          <RouteApp />
        </Container>
      </Fragment>
    );
  }
}

export default App;
