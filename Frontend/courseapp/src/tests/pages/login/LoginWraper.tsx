import React,{Component} from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import {Provider} from 'react-redux';
import store from '../../../store/store';
import { useHistory } from "react-router-dom";

export const LoginWraper = (props:any) => {
    const history = useHistory();
    return (
        <Router>{props.render()}</Router>
    );
};