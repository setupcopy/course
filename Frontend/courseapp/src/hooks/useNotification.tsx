import React from 'react';
import { useSnackbar, VariantType } from 'notistack';

export const messageType = {
    MESSAGE_ERROR:'error',
    MESSAGE_SUCCESS:'success',
    MESSAGE_WARNING:'warning'
};

export const errorMessage = {
    SIGNUP_FAILED:'Signup is failed',
};

export const useNotification = () => {
    const snackbarQueue = useSnackbar();
    
    const getVariantType = (type:string) => {
        let variantResult:VariantType;
        switch (type) {
            case 'error':
                return variantResult = 'error';
            case 'success':
                return variantResult = 'success';
            case 'warning':
                return variantResult = 'warning';
            default:
                return variantResult = 'info';
        }
    }

    const ShowNotification = (message:string,type:string) => {
        const variantResult = getVariantType (type);
        snackbarQueue.enqueueSnackbar(message,{variant:variantResult});
    };

    return {ShowNotification}
};