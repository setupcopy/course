import React, { Fragment, useEffect } from "react";
import { Route, Link, Switch } from "react-router-dom";
import HomeHeader from "./header";
import { useHome } from "./hooks/useHome";
import { Drawer } from "@material-ui/core";
import {Course} from '../course/index';

const Home = () => {
  const { menus, openState, openHandle, onClose, menuList } = useHome();
  return (
    <Fragment>
      <HomeHeader openHandle={openHandle} />
      <Drawer anchor="left" open={openState} onClose={onClose}>
        {menuList()}
      </Drawer>
      <Switch>
        <Route path="/home/course" component={Course} />
      </Switch>
    </Fragment>
  );
};

export { Home };
