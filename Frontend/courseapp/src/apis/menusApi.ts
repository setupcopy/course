import {Get} from './baseApi';
import dataType from '../utilitys/dataTypeForTransmit';
import cookie from 'react-cookies';

const getMenus = async (roleName:string) => {
    //url
    const url:string = `/api/menu?rolename=${roleName}`;
    //headers
    const headers:HeadersInit = {
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Authorization':`Bearer ${cookie.load('jwtToken')}`
    };

    try {
        //call method of get by async in order to get an instance of promise which are response. It should be list of menu.
        const result:any = await Get(url,headers,dataType.dataJson);
        return result;
    }catch(error){
        throw new Error(error as string);
    }
};

export {getMenus}