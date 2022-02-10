import React from 'react';
import {createSlice,createAsyncThunk,createEntityAdapter} from '@reduxjs/toolkit';

 //create slice of login, saving useid,role,nickname
 const loginSlice = createSlice({
     name: 'login',
     initialState:{
         user:{
            id:'',
            email:'',
            nickName:'',
            role:''
         }
     },
     reducers:{
         SaveUserInfo: (state,action) => {
            state.user = {...action.payload};
         }
     }
 });

 export const {SaveUserInfo} = loginSlice.actions;
 export default loginSlice.reducer;