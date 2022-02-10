import React from "react";
import { render, fireEvent, waitFor, screen } from "@testing-library/react";
import { renderHook, act, cleanup } from "@testing-library/react-hooks";
import "@testing-library/jest-dom";
import { BrowserRouter as Router } from "react-router-dom";
import { Provider } from "react-redux";
import * as MenuApi from "../../../apis/menusApi";
import { ErrorMessageNotification } from "../../../components/ErrorMessageNotification";
import cookie from "react-cookies";
import { useHome } from "../../../pages/home/hooks/useHome";
import * as redux from "react-redux";
import store from "../../../store/store";
import { Home } from "../../../pages/home/index";

beforeEach(() => {
  jest.clearAllMocks();
});

describe("test of useEffect", () => {
  test("get menus", async () => {
    const resInfo = [
      {
        id: 1,
        role: "student",
        menuName: "course",
      },
    ];
    const spy = jest.spyOn(MenuApi, "getMenus").mockImplementation(() => {
      return Promise.resolve(resInfo);
    });

    const { result } = renderHook(() => useHome());

    await waitFor(() => {
      expect(result.current.menus[0].id).toBe(1);
      expect(result.current.menus[0].role).toBe("student");
      expect(result.current.menus[0].menuName).toBe("course");
    });
  });
});

describe("test of useEffect", () => {
    test("get menus", async () => {
      const resInfo = [
        {
          id: 1,
          role: "student",
          menuName: "course",
        },
      ];
      const spy = jest.spyOn(MenuApi, "getMenus").mockImplementation(() => {
        return Promise.resolve(resInfo);
      });
  
      const { result } = renderHook(() => useHome());
  
      await waitFor(() => {
        expect(result.current.menus[0].id).toBe(1);
        expect(result.current.menus[0].role).toBe("student");
        expect(result.current.menus[0].menuName).toBe("course");
      });
    });
  });