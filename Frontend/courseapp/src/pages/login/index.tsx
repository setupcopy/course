import React from "react";
import {
  Container,
  Typography,
  TextField as MaterialTextField,
  FormControlLabel,
  Button,
  Grid,
  Checkbox,
  Box,
  ButtonGroup,
} from "@material-ui/core";
import { useFormik } from "formik";
import * as Yup from "yup";
import { Link, withRouter } from "react-router-dom";
import { student, teacher, manager } from "../../utilitys/constVariables";
import { useLogin } from "./hooks/useLogin";
import { useSelector } from "react-redux";

const Login = () => {
  const {
    loginEntity,
    onClickLogin,
    onClickButtonGroup,
    onTextChanged,
    returnTypeofGroupButton,
    rememberMeValue,
    onCheckChanged,
  } = useLogin();

  //creating formik,initial values,validation and on submit.
  const formik = useFormik({
    initialValues: loginEntity,
    validationSchema: Yup.object({
      email: Yup.string()
        .email("Invalid email address")
        .required("Email required"),
      password: Yup.string()
        .min(6, "Should be of minimum 6 characters length")
        .required("Password required"),
    }),
    onSubmit: (values, { setSubmitting }) => {
      onClickLogin(values);
      setTimeout(() => {
        setSubmitting(false);
      }, 0);
    },
  });

  return (
    <Container disableGutters maxWidth="sm" sx={{ pt: 8, pb: 6 }}>
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          width: "100%",
        }}
      >
        <Typography
          variant="h3"
          sx={{ fontFamily: "Impact", fontSize: "42px" }}
        >
          COURSE MANAGEMENT ASSISTANT
        </Typography>
        <form onSubmit={formik.handleSubmit}>
          <Box component="main" sx={{ mt: 6 }}>
            <ButtonGroup
              variant="outlined"
              aria-label="outlined primary button group"
              size="large"
              onClick={onClickButtonGroup}
            >
              <Button
                sx={{ fontWeight: "bold" }}
                name={student}
                onClick={onTextChanged}
                value="Student"
                variant={returnTypeofGroupButton(student)}
              >
                Student
              </Button>
              <Button
                sx={{ fontWeight: "bold" }}
                name={teacher}
                onClick={onTextChanged}
                value="Teacher"
                variant={returnTypeofGroupButton(teacher)}
              >
                Teacher
              </Button>
              <Button
                sx={{ fontWeight: "bold" }}
                name={manager}
                onClick={onTextChanged}
                value="Manager"
                variant={returnTypeofGroupButton(manager)}
              >
                Manager
              </Button>
            </ButtonGroup>
            <MaterialTextField
              margin="normal"
              fullWidth
              id="email"
              label="email"
              autoComplete="email"
              error={formik.touched.email && Boolean(formik.errors.email)}
              helperText={
                formik.touched.email && formik.errors.email
                  ? formik.errors.email
                  : ""
              }
              {...formik.getFieldProps("email")}
            />
            <MaterialTextField
              margin="normal"
              required
              fullWidth
              label="password"
              error={formik.touched.password && Boolean(formik.errors.password)}
              helperText={
                formik.touched.password && formik.errors.password
                  ? formik.errors.password
                  : ""
              }
              type="password"
              id="password"
              autoComplete="current-password"
              {...formik.getFieldProps("password")}
            />
            <FormControlLabel
              control={
                <Checkbox
                  color="primary"
                  checked={rememberMeValue}
                  onChange={onCheckChanged}
                  data-testid="checkRemember"
                />
              }
              label="Remember me"
            />
            <Button
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              type="submit"
            >
              Sign In
            </Button>
          </Box>
        </form>
        <Grid container>
          <Grid item>
            <Typography>
              No account?
              <Link to="/signup"> Sign Up</Link>
            </Typography>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
};

export default withRouter(Login);
