import {render,fireEvent,waitFor,screen} from '@testing-library/react';
import {loginApi} from '../../apis/loginApi';
import * as Api from '../../apis/baseApi';
import { renderHook, act, cleanup } from '@testing-library/react-hooks'
import cookie from "react-cookies";

beforeEach(() => {
    jest.clearAllMocks();
});

jest.mock('react-cookies');
describe('test of loginApi',() => {
    test('testing the success when calling loginApi',async() => {
        const email:string = 'setup@163.com';
        const password:string = '123456';
        const role:string = 'Student';

        const resInfo = {
            user:{
                 id:'501',
                 email:'setup@163.com',
                 Role:'Student'
            },
            jwtToken:'asdfjkl'
         }

        const spy =  jest.spyOn(Api,'PostWithData').mockImplementation(() => {
            return Promise.resolve(resInfo);
        });
        //const {result} = renderHook(() => loginApi(email,password,role));
        const result = await loginApi(email,password,role);
        const res = cookie.load('jwtToken');
        expect(result).toBe(resInfo);
        expect(res).toBeUndefined();
        spy.mockClear();
    });

    test('testing the failed when calling loginApi',async() => {
        const email:string = 'setup@163.com';
        const password:string = '123456';
        const role:string = 'Student';

        const resInfo = {
            user:{
                 id:'501',
                 email:'setup@163.com',
                 Role:'Student'
            },
            jwtToken:'asdfjkl'
         }

        const spy =  jest.spyOn(Api,'PostWithData').mockImplementation(() => {
            return Promise.reject('401');
        });
          
        try {
            const result = await loginApi(email,password,role);
        }catch (error){
            expect(error.message).toBe('401');
        }

        spy.mockClear();
    });
});