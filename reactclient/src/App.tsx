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
import { store, useStore } from "./stores/store";
import ServerError from "./errors/ServerError";
import LoginForm from "./components/Users/LoginForm";
import HomePage from "./components/Home/HomePage";
import { Container } from "semantic-ui-react";
import { useEffect } from "react";
import LoadingComponent from "./components/LoadingComponent";

axios.interceptors.request.use(config => {
  const token = store.commonStore.token;
  if(token) config.headers.Authorization = `Bearer ${token}`;
  return config;
})

axios.interceptors.response.use(async response => {
    return response
}, (error: AxiosError) => {
    const {data, status, config} = error.response!;
    switch(status) {
      case 400: 
        if(typeof data === 'string') {
          toast.error(data);
        }
        if(config.method === 'get' && data.errors.hasOwnProperty('id')) {
          history.push('/not-found');
        }
        if(data.errors) {
          const modalStateErrors = [];
          for(const key in data.errors) {
            if(data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          throw modalStateErrors.flat();
        }
        break;
      case 401: 
        toast.error('unauthorised');
        break;
      case 404: 
        history.push('/not-found');
        break;
      case 500: 
        store.commonStore.setServerError(data);
        history.push('/server-error');
        break;
    }

    return Promise.reject(error);
})

function App() {
  const location = useLocation();
  const {commonStore, userStore} = useStore();

  useEffect(() => {
    if(commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore])

  if(!commonStore.appLoaded) return <LoadingComponent isActive={true}/>

  return (
    <>
    <ToastContainer position='bottom-right' hideProgressBar />
      <Route exact path='/' component={HomePage} />
      <Route
        path={'/(.+)'}
        render={() => (
          <> 
            <NavBar />
            <Container style={{ marginTop: '7em'}}>
              <Switch>
                <Route exact path='/itemDashboard' component={AnalysisDashboard} key={location.key} />
                <Route exact path='/itemDashboard/:id' component={AnalysisDetails} />
                <Route exact path='/watchList' component={AnalysisDashboard} key={location.key} />
                <Route exact path='/errors' component={TestErrors} />
                <Route exact path='/server-error' component={ServerError} />
                <Route exact path='/login' component={LoginForm} />
                
                <Route component={NotFound} />
              </Switch>
            </Container>
          </>
        )}
      />
    </>
  );
}

export default observer(App);
