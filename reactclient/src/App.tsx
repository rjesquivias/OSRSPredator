import NavBar from "./components/NavBar";
import AnalysisDashboard from "./components/Analysis/AnalysisDashboard";
import { observer } from "mobx-react-lite";
import { Route, Switch, useLocation } from 'react-router-dom';
import AnalysisDetails from "./components/Analysis/AnalysisDetails";
import TestErrors from "./errors/TestError";
import { toast, ToastContainer } from "react-toastify";
import axios, { AxiosError } from "axios";
import NotFound from "./errors/NotFound";
import { history } from ".";

axios.interceptors.response.use(async response => {
    return response
}, (error: AxiosError) => {
    const {data, status} = error.response!;
    switch(status) {
      case 400: 
        toast.error('bad request');
        break;
      case 401: 
        toast.error('unauthorised');
        break;
      case 404: 
        history.push('/not-found');
        break;
      case 500: 
        toast.error('server error');
        break;
    }

    return Promise.reject(error);
})

function App() {
  const location = useLocation();

  return (
    <>
    <ToastContainer position='bottom-right' hideProgressBar />
    <div className="container">
      <NavBar />
        <Switch>
          <Route exact path='/itemDashboard' component={AnalysisDashboard} key={location.key} />
          <Route exact path='/itemDashboard/:id' component={AnalysisDetails} />
          <Route exact path='/watchList' component={AnalysisDashboard} key={location.key} />
          <Route exact path='/errors' component={TestErrors} />
          <Route component={NotFound} />
        </Switch>
    </div>
    </>
  );
}

export default observer(App);
