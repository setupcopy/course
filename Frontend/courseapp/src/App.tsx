import React from 'react';
import Login from './pages/login'; 
import { Route,Switch,Redirect } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import {AutoLogin} from './components/AutoLogin';
import {Home} from './pages/home/index';
import {SignUp} from './pages/SignUp'

function App() {
  AutoLogin();
  return (
    <SnackbarProvider maxSnack={3} autoHideDuration={5000}>  
      <Switch>
        <Route path="/login" component={Login}/>
        <Route path="/home" component={Home}/>
        <Route path='/signup' component={SignUp} />
      </Switch> 
      <Redirect to="/login" />         
    </SnackbarProvider>
  );
}

export default App;
