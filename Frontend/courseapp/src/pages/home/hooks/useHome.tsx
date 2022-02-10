import React, { useEffect, useState, useCallback, Fragment } from "react";
import { getMenus } from "../../../apis/menusApi";
import {
  ErrorMessageNotification,
  ErrorMessageDescrible,
} from "../../../components/ErrorMessageNotification";
import { useSelector } from "react-redux";
import { RootState } from "../../../store/store";
import { List, ListItem, ListItemText,ListItemIcon } from "@material-ui/core";
import {ComputerOutlined} from "@material-ui/icons";
import { Link } from "react-router-dom";

const useHome = () => {
  //store list of menu
  const [menus, setMenus] = useState([{ id: 0, role: "", menuName: "" }]);
  //this state is for controlling drawer
  const [openState, setOpenState] = useState(false);
  //get information of login in order to have column of role
  //const loginInfo = useSelector((state: RootState) => state.login);
  const errorNotification = new ErrorMessageNotification();

  const openHandle = useCallback((state: boolean) => {
    setOpenState(state);
  }, []);

  //call menu api through http
  useEffect(() => {
    const result = getMenus("student"); //loginInfo.user.role
    result
      .then((res) => {
        setMenus(res);
      })
      .catch((error) => {
        errorNotification.ShowNotification(
          ErrorMessageDescrible.GET_MENUS_FAILED
        );
      });
  }, []);

  const onClose = () => {
    openHandle(false);
  };

  const menuOnClick = () => {
    openHandle(false);
  };

  const menuList = () => {
    return (
      <div onClick={menuOnClick}>
        <List sx={{ width: "250px" }}>
          {menus.map((item, index) => {
            return (
              <ListItem button key={item.id}>
                <ListItemIcon><ComputerOutlined /></ListItemIcon>
                <Link to="/home/course" style={{textDecoration:"none"}}>
                    <ListItemText primary={item.menuName} />
                </Link>
              </ListItem>
            );
          })}
        </List>    
      </div>
    );
  };

  return { menus, openState, openHandle, onClose, menuList };
};

export { useHome };
