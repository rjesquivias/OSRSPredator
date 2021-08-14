import NavBar from "./components/NavBar";
import AnalysisDashboard from "./components/Analysis/AnalysisDashboard";
import { observer } from "mobx-react-lite";
import { Route } from 'react-router-dom';

function App() {
  return (
    <div className="container">
      <NavBar />
      <Route exact path='/itemDashboard' component={AnalysisDashboard} />
      <Route exact path='/watchList' component={AnalysisDashboard} />
    </div>
  );
}

export default observer(App);
