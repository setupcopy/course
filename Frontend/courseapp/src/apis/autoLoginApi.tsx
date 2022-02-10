import { Get } from './baseApi';
import dataType from "../utilitys/dataTypeForTransmit";
import cookie from 'react-cookies';

const autoLoginApi = async (email:string) => {
    //url for api
    const url:string = `/auth/login?email=${email}`;
    //set headers
    const headers:HeadersInit = {
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Authorization':`Bearer ${cookie.load('jwtToken')}`
    };
    
    try {
        //call method of Get by async in order to get an instance of promise which are response.
        const result:any = await Get(url,headers,dataType.dataJson);
        return result;
    }catch (error:any) {
       throw new Error(error);
    }
};

export {autoLoginApi};