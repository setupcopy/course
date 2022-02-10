import React from "react";
import { render, fireEvent, waitFor, screen } from "@testing-library/react";
import { renderHook, act, cleanup } from "@testing-library/react-hooks";
import "@testing-library/jest-dom";
import Login from "../../../pages/login/index";
import { BrowserRouter as Router } from "react-router-dom";
import { Provider } from "react-redux";
import store from "../../../store/store";
import reducer, { SaveUserInfo } from "../../../store/slices/loginSlice";
import { useLogin } from "../../../pages/login/hooks/useLogin";
import { ILoginEntity } from "../../../pages/login/models/models";
import * as Api from "../../../apis/loginApi";
import { ErrorMessageNotification } from "../../../components/ErrorMessageNotification";
import cookie from "react-cookies";
import { LoginWraper } from "./LoginWraper";
import * as redux from "react-redux";

beforeEach(() => {
  jest.clearAllMocks();
  cleanup();
});

const mockDispatch = jest.fn();
jest.mock("react-redux", () => ({
  useDispatch: () => mockDispatch,
}));

const mockHistoryPush = jest.fn();
jest.mock("react-router-dom", () => ({
  ...jest.requireActual("react-router-dom"),
  useHistory: () => ({
    push: mockHistoryPush,
  }),
}));

jest.mock("../../../components/ErrorMessageNotification");
describe("test of login", () => {
  test("if success,call useDiapatch and useHistory", async () => {
    const user: ILoginEntity = {
      email: "123@153.com",
      password: "123456",
    };

    const resInfo = {
      user: {
        id: "501",
        email: "setup@163.com",
        Role: "Student",
      },
      jwtToken: "asdfjkl",
    };

    const spy = jest.spyOn(Api, "loginApi").mockImplementation(() => {
      return Promise.resolve(resInfo);
    });

    const { result } = renderHook(() => useLogin());
    await act(() => {
      result.current.onClickLogin(user);
    });

    expect(mockDispatch).toHaveBeenLastCalledWith({
      payload: { Role: "Student", email: "setup@163.com", id: "501" },
      type: "login/SaveUserInfo",
    });
    expect(mockHistoryPush).toHaveBeenCalledWith("/home");

    spy.mockRestore();
  });

  test("if failed,call notification", async () => {
    const mockShowNotification = jest.fn();
    ErrorMessageNotification.mockImplementation(() => {
      return {
        ShowNotification: mockShowNotification,
      };
    });

    const user: ILoginEntity = {
      email: "123@153.com",
      password: "123456",
    };

    const { result } = renderHook(() => useLogin());
    const spy = jest.spyOn(Api, "loginApi").mockImplementation(() => {
      return Promise.reject("mistake");
    });
    //const spyNo = jest.spyOn(ErrorMessageNotification.prototype,'ShowNotification').mockImplementation();
    await act(() => {
      result.current.onClickLogin(user);
    });

    expect(mockShowNotification).toHaveBeenCalled();

    //spyNo.mockRestore();
    spy.mockRestore();
  });
});

//test the redux of login
describe("test of redux of login", () => {
  test("testing onClickLogin", () => {
    const initialState = {
      user: {
        id: "1",
        email: "1111p@163.com",
        nickName: "111",
        role: "1111",
      },
    };
    const setState = {
      id: "501",
      email: "setup@163.com",
      nickName: "miaoduo",
      role: "student",
    };
    const loginReducer = reducer(initialState, SaveUserInfo(setState));

    expect(loginReducer.user).toEqual(setState);
  });
});

//test useEffect
describe("test of useEffect", () => {
  test("get value from cookie", () => {
    jest.mock("react-cookies");
    cookie.save("email", "abc@gmail.com", { path: "/" });
    const { result } = renderHook(() => useLogin());

    waitFor(() => {
      expect(result.current.loginEntity.email).toBe("abc@gmail.com");
    });
  });
});

//test rememberMe
describe("test of rememberMe", () => {
  test("test of remember", () => {
    jest.mock("react-cookies");
    const { result } = renderHook(() => useLogin());
    act(() => {
      result.current.rememberMe(true, "save me");
    });

    const res = cookie.load("email");
    expect(res).toBe("save me");
  });

  test("test: not remember", () => {
    jest.mock("react-cookies");
    const { result } = renderHook(() => useLogin());
    act(() => {
      result.current.rememberMe(false, "save me");
    });

    const res = cookie.load("email");
    expect(res).toBeUndefined();
  });
});
//test element
describe("test of element", () => {
  test("test of ButtonGroup click", () => {
    const login = render(<LoginWraper render={() => <Login />} />);
    const buttonGroup = login.getByLabelText("outlined primary button group");

    fireEvent.click(buttonGroup, {
      target: {
        name: "Student",
      },
    });

    const buttonStudent = login.getByText("Student");
    const buttonTeacher = login.getByText("Teacher");
    const buttonManager = login.getByText("Manager");
    expect(buttonStudent).toHaveClass("MuiButton-contained");
    expect(buttonTeacher).toHaveClass("MuiButton-outlined");
    expect(buttonManager).toHaveClass("MuiButton-outlined");
  });

  test("test of email textField required", async () => {
    const login = render(<LoginWraper render={() => <Login />} />);
    const button = login.getByText("Sign In");

    fireEvent.click(button);

    await waitFor(() => login.getByText("Email required"));

    expect(login.getByText("Email required")).toBeInTheDocument();
  });

  test("test of password textField required", async () => {
    const login = render(<LoginWraper render={() => <Login />} />);
    const button = login.getByText("Sign In");

    fireEvent.click(button);

    await waitFor(() => login.getByText("Password required"));

    expect(login.getByText("Password required")).toBeInTheDocument();
  });

  test("test of remember checkBox", () => {
    const login = render(<LoginWraper render={() => <Login />} />);
    const checkBox = login
      .getByTestId("checkRemember")
      .querySelector('input[type="checkbox"]');

    fireEvent.click(checkBox);

    expect(checkBox).toHaveProperty("checked", true);
  });
});
