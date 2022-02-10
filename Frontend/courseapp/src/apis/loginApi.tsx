import {PostWithData} from './baseApi';
import dataType from "../utilitys/dataTypeForTransmit";
import cookie from 'react-cookies';

const loginApi = async (email:string,password:string,role:string) => {
    //url for api
    const url:string = '/auth/login';
    //set headers
    const headers:HeadersInit = {
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*'
    };

    //set body of login
    const body = {
        'email':email,
        'password':password,
        'role':role
    };

    try {
        //call method of postwithdata by async in order to get an instance of promise which are response. It should be jwtToken.
        const result:any = await PostWithData(url,headers,body,dataType.dataJson);
        //save jwtToke in cookie
        cookie.save('jwtToken',result.jwtToken,{path:'/'});
        return result;
    }catch (error:any) {
       throw new Error(error);
    }
};

export {loginApi};