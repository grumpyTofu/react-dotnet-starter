import Todos from "../todos/Todos";
import DashboardIcon from "@material-ui/icons/Dashboard";
import ListIcon from "@material-ui/icons/List";
import Dashboard from "../dashboard/Dashboard";

export interface RouteConfig {
  path: string;
  component: any;
  icon: any;
  title: string;
  routes?: RouteConfig[];
}

export const routes: RouteConfig[] = [
  {
    path: "/",
    component: <Dashboard />,
    icon: <DashboardIcon />,
    title: "Dashboard",
  },
  {
    path: "todos",
    component: <Todos />,
    icon: <ListIcon />,
    title: "Todos",
  },
];
