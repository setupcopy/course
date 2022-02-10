import React from 'react';
import {configureStore} from '@reduxjs/toolkit';
import loginSlice from './slices/loginSlice';

const store = configureStore({
    reducer:{
        login:loginSlice
    }
});

//redux with typescript should define the type of RootState which is used by useSelector
export type RootState = ReturnType<typeof store.getState>;
export default store;