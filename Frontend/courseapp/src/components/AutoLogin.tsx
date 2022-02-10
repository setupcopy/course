import React,{useEffect} from 'react';
import { autoLoginApi } from "../apis/autoLoginApi";
import cookie from "react-cookies";
import { rememberEmail,jwtToken } from '../utilitys/constVariables'
import {useDispatch} from 'react-redux';
import {SaveUserInfo} from '../store/slices/loginSlice'

const AutoLogin = () => {
    const dispatch = useDispatch();
    useEffect (() => {
        if (cookie.load(rememberEmail) && cookie.load(jwtToken)) {
            const email = cookie.load(rememberEmail);
            const result = autoLoginApi(email);
            result.then((res) => {
                //save jwtToke in cookie
                cookie.save(jwtToken,res.jwtToken,{path:'/'});
                dispatch(SaveUserInfo(res.user));             
            })
            .catch((err) => {
                console.log('AutoLogin is failed:',err);
            });
        }
    },[])
};

export {AutoLogin};