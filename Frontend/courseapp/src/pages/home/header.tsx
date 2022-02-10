import React, { Fragment } from "react";
import { AppBar,Toolbar,Typography,Button,IconButton } from "@material-ui/core";
import {MenuRounded} from "@material-ui/icons";
import { Link } from "react-router-dom";
import cookie from 'react-cookies';
import {useSelector,useDispatch} from 'react-redux';
import {RootState} from '../../store/store';
import {SaveUserInfo} from '../../store/slices/loginSlice'
import {Props} from './models/models'

const HomeHeader = ({openHandle}:Props) => {
  const userState = useSelector((state:RootState) => state.login );
  const dispatch = useDispatch();

  const onClickLogout = () => {
    //remove jwtToke from cookie
    cookie.remove('jwtToken');
    //remove info of user from redux
    dispatch(SaveUserInfo({}));
  }
  
  //open the menu
  const buttonOnclick = () => {
    openHandle(true);
  };

  return (
    <AppBar
      position="static"
      color="primary"
      elevation={0}
      sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}` }}
    >
      <Toolbar sx={{ flexWrap: "wrap" }}>
        <IconButton edge="start" color="inherit" sx={{marginRight:2}} onClick={buttonOnclick}>
          <MenuRounded />
        </IconButton>     
        <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
          Course
        </Typography>
        {userState.user.id &&
          <Fragment>
            <Typography>
              welcome {userState.user.nickName}
            </Typography>
            <Link to="/login" style={{textDecoration:"none"}}>
              <Button variant="outlined" sx={{ my: 1, mx: 1 }} onClick={onClickLogout} id='logout'>
                Logout
              </Button>
            </Link>
          </Fragment>
        }
      </Toolbar>
    </AppBar>
  );
};

export default HomeHeader;
