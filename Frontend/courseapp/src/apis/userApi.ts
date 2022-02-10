import { PostWithData } from './baseApi';
import dataType from "../utilitys/dataTypeForTransmit";
import {IUserAddType} from '../models/userModel';
import cookie from 'react-cookies';

const userAddApi = async (user:IUserAddType) => {
    //url for api
    const url:string = `/api/users`;
    //set headers
    const headers:HeadersInit = {
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Authorization':`Bearer ${cookie.load('jwtToken')}`
    };
    
    try {
        //call method of Get by async in order to get an instance of promise which are response.
        const result:any = await PostWithData(url,headers,user,dataType.dataJson);
        return result;
    }catch (error:any) {
       throw new Error(error);
    }
};

export {userAddApi};