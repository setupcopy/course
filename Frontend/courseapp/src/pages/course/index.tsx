import React from 'react';
import {useSelector} from 'react-redux';
import {RootState} from '../../store/store';

const Course = () => {
    const userState = useSelector((state:RootState) => state.login );
    return (
        <h1>Welcome Course</h1>
    );
};

export {Course};