import { createMuiTheme, withStyles } from "@material-ui/core/styles";

export const theme = createMuiTheme({
  palette: {
    primary: {
      main: "rgb(0, 98, 196)",
      light: "rgb(210, 233, 255)",
    },
    secondary: {
      main: "rgb(250, 250, 250)",
    },
  },
  overrides: {
    // MuiListItemIcon: {
    //   root: {
    //     color: "inherit !important",
    //   },
    // },
    MuiDivider: {
      root: {
        backgroundColor: "#BDBDBD",
        //backgroundColor: "rgb(0, 98, 196)",
      },
    },
    MuiCard: {
      root: {
        background: "linear-gradient(to bottom right, rgba(0, 131, 255, .12), rgba(0, 131, 255, .10))",
        backdropFilter: "blur(.75rem)",
        //color: "rgb(98, 104, 110)",
      },
    },
    MuiButton: {
      // outlined: {
      //   border: "1px solid rgba(110, 108, 110, .75)",
      //   color: "rgba(110, 108, 110, .75)",
      // },
      contained: {
        backgroundColor: "rgb(210, 233, 255)",
        color: "rgb(0, 98, 196)",
      },
    },
    MuiTypography: {
      colorTextPrimary: {
        color: "rgb(0, 98, 196)",
      },
      subtitle1: {
        fontWeight: "bold",
      },
    },
    MuiPaper: {
      root: {
        backgroundColor: "unset",
        //background: "linear-gradient(to bottom right, rgba(4, 91, 188, .18), rgba(4, 91, 188, .12))",
        background: "linear-gradient(to bottom right, rgba(0, 131, 255, .12), rgba(0, 131, 255, .10))",
        backdropFilter: "blur(.5rem)",
        //color: "rgb(0, 98, 196)",
      },
    },
  },
});

export const GlobalCss = withStyles({
  // @global is handled by jss-plugin-global.
  "@global": {
    // You should target [class*="MuiButton-root"] instead if you nest themes.
    body: {
      // backgroundColor: '#F5F6FA'
      //backgroundColor: 'rgba(156, 38, 176,0.1)'
      // background: 'linear-gradient(to bottom right, rgba(156, 38, 176,0.3), rgba(156, 38, 176,0.1))',
      // backgroundColor: "rgba(13,0,255,.1)",
      // background:
      //   "linear-gradient(121deg, rgba(13,0,255,0.4430147058823529) 0%, rgba(205,205,255,1) 32%, rgba(233,232,252,1) 52%, rgba(184,180,255,1) 72%, rgba(13,0,255,0.5214460784313726) 100%)",
      // backgroundRepeat: "no-repeat",
      minHeight: "100vh",
    },
  },
})(() => null);
