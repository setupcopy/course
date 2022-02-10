import React, { useState, useEffect } from "react";
import { loginApi } from "../../../apis/loginApi";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import {
  rememberEmail,
  student
} from "../../../utilitys/constVariables";
import {
  ErrorMessageNotification,
  ErrorMessageDescrible,
} from "../../../components/ErrorMessageNotification";
import cookie from "react-cookies";
import { SaveUserInfo } from "../../../store/slices/loginSlice";
import { ILoginEntity, CustomerText } from "../models/models";

export const useLogin = () => {
  const loginEntity: ILoginEntity = {
    email: "",
    password: "",
  };

  const history = useHistory();

  //bind check of remember me
  const [rememberMeValue, setRememberMeValue] = useState(false);
  //this variable is for saving the status of role button
  const [buttonStatus, setButtonStatus] = useState(student);
  //this variable is for saving role
  const [role, setRole] = useState(student);
  // an instance of Sanckbar
  const errorNotification = new ErrorMessageNotification();
  const dispatch = useDispatch();

  //first loading  email from cookie
  useEffect(() => {
    //if cookie is not empty or undefined,it implement the function of remember me
    if (cookie.load(rememberEmail)) {
      loginEntity.email = cookie.load(rememberEmail);
      setRememberMeValue(true);
    } else {
      setRememberMeValue(false);
    }
  }, []);

  const onClickLogin = (values: ILoginEntity) => {
    const result = loginApi(values.email, values.password, role);
    result
      .then((res) => {
        rememberMe(rememberMeValue, values.email);
        //save info of user in redux
        dispatch(SaveUserInfo(res.user));
        //jump the the page of course
        history.push("/home");
      })
      .catch((err) => {
        errorNotification.ShowNotification(
          ErrorMessageDescrible.EMAIL_PASSWORD_NOTMATCH
        );
      });
  };

  const onTextChanged = (e: any) => {
    setRole(e.target.name);
  };

  //save email in cookie
  const rememberMe = (isRememberMe: boolean, fieldEmail: string) => {
    if (isRememberMe) {
      //save email in cookie
      cookie.save(rememberEmail, fieldEmail, { path: "/" });
    } else {
      cookie.remove(rememberEmail);
    }
  };

  const onCheckChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
    setRememberMeValue(e.target.checked);
  };

  //style of button through clicking different buttons
  const onClickButtonGroup = (e: React.MouseEvent) => {
    setButtonStatus((e.target as any).name);
  };

  //set the type of groupButton
  function returnTypeofGroupButton(buttonName: string): CustomerText {
    return buttonStatus === buttonName ? "contained" : "outlined";
  }

  return {loginEntity,onClickLogin,
        onClickButtonGroup,onTextChanged,
        returnTypeofGroupButton,rememberMeValue,
        onCheckChanged,rememberMe,errorNotification
    };
};
