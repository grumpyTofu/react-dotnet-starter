import React, { useEffect, useState } from "react";
import { Grid, Card, CardContent, CardActions, Button, Typography, createStyles, makeStyles, Theme } from "@material-ui/core";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    title: {
      textAlign: "center",
    },
  })
);

interface DashboardProps {}

const Dashboard: React.FC<DashboardProps> = ({}) => {
  const classes = useStyles();
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Card>
          <CardContent>
            <Typography variant="h4" className={classes.title}>
              Welcome to CAPRI
            </Typography>
          </CardContent>
        </Card>
      </Grid>
    </Grid>
  );
};

export default Dashboard;
