import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store';
import {onLogingSignIn} from './actions';


store.dispatch(onLogingSignIn());

ReactDOM.render(
  <BrowserRouter>
    <Provider store={store}>
      <React.Fragment>
        <App />
      </React.Fragment>
    </Provider>
  </BrowserRouter>,
  document.getElementById('root')
);

reportWebVitals();
