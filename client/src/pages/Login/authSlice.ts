import { createSlice, Action, Middleware, Dispatch, createAction } from "@reduxjs/toolkit";
import { RootState } from "../../app/store";
import { ApiResponse } from "../../utils/apiFetch";
// import { getUserTodosAsync } from '../../components/todos/todosSlice';
import { setUser } from "../Profile/profileSlice";
// import { getPublicTodosAsync, getUserTodosAsync } from '../../components/todos/todosSlice'; TODO: Find type-safe fix
export interface AuthState {
  loggedIn: boolean;
}

const initialState: AuthState = {
  loggedIn: localStorage.getItem("accessToken") ? true : false,
};

export const login = createAction("auth/login", function prepare(response) {
  console.log(response);
  const token = response["tokenId"];
  return { payload: { token } };
});

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginSuccess: (state) => {
      state.loggedIn = true;
    },
    loginFail: (state) => {
      state.loggedIn = false;
    },
    logout: (state) => {
      state.loggedIn = false;
    },
    logoutFail: (state) => {
      state.loggedIn = true;
    },
  },
});

export const authMiddleware: Middleware<any, any, Dispatch<Action>> = (store) => (next) => (action) => {
  if (login.match(action)) {
    const body = { IdToken: action.payload.token };
    console.log(body);
    fetch(`${process.env.REACT_APP_API_BASE_URL}/User/authenticate`, {
      method: "POST",
      headers: {
        "X-Requested-With": "XMLHttpRequest",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    })
      .then((res) => res.json())
      .then((res: ApiResponse) => {
        console.log(res);
        if (res.error === false) {
          localStorage.setItem("accessToken", res.data["accessToken"]);
          store.dispatch(setUser(res["data"]["user"]));
          store.dispatch(loginSuccess());
        } else {
          store.dispatch(loginFail());
        }
      })
      .catch((error) => {
        console.error(error);
      });
  } else if (authSlice.actions.loginFail.match(action)) {
    console.log("login failed");
    if (localStorage.getItem("accessToken")) {
      localStorage.removeItem("accessToken");
    }
  } else if (authSlice.actions.logout.match(action)) {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("profile_picture");
  } else if (authSlice.actions.logoutFail.match(action)) {
    console.warn("Failed to logout");
  }
  next(action);
};

export const { loginSuccess, loginFail, logout, logoutFail } = authSlice.actions;
export const selectLoggedIn = (state: RootState) => state.auth.loggedIn;
export default authSlice.reducer;
