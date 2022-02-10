import React from "react";
import { IUserAddType } from "../../../models/userModel";
import { userAddApi } from "../../../apis/userApi";
import {useNotification,messageType,errorMessage} from "../../../hooks/useNotification";

export const useSignup = () => {
  const {ShowNotification} = useNotification();
  const user: IUserAddType = {
    email: "",
    password: "",
    passwordConfirmed: "",
    role: "student",
    nickName: "",
  };

  const onClickSignup = async (values: IUserAddType) => {
    try {
      const result = await userAddApi(values);
      console.log(result);
    } catch (error) {
      ShowNotification(errorMessage.SIGNUP_FAILED,messageType.MESSAGE_ERROR);
    }
  };

  return { user, onClickSignup };
};
