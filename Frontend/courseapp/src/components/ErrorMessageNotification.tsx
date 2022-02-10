import { ProviderContext,useSnackbar } from 'notistack';

export const ErrorMessageDescrible = {
    EMAIL_PASSWORD_NOTMATCH:'Email and Password are not matched',
    GET_MENUS_FAILED:'Geting menus is failed'
};

class ErrorMessageNotification {
    
    snackbarQueue:ProviderContext;
    
    constructor() {
        this.snackbarQueue = useSnackbar();
    }

    // pop up notification
    ShowNotification(message:string) {
        this.snackbarQueue.enqueueSnackbar(message,{variant:'error',});
    }
}

export {ErrorMessageNotification};
