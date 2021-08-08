import NavBar from "./components/NavBar";
import AnalysisDashboard from "./components/Analysis/AnalysisDashboard";
import { useEffect } from "react";
import axios from "axios";
import { useStore } from "./stores/store";
import { observer } from "mobx-react-lite";

function App() {
  const { itemStore } = useStore();

  useEffect(() => {
      axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=${itemStore.pageSize}&page=1`).then(response => {
          console.log(response);
          itemStore.setSimpleItemAnalysisList(response.data);
      });
  }, []);

  return (
    <div className="container">
      <NavBar setSimpleItemAnalysisList={itemStore.setSimpleItemAnalysisList} setNavState={itemStore.setNavState} navState={itemStore.navState} setCheckedItems={itemStore.setCheckedItems} />
      <AnalysisDashboard simpleItemAnalysisList={itemStore.simpleItemAnalysisList} pageSize={itemStore.pageSize} setSimpleItemAnalysisList={itemStore.setSimpleItemAnalysisList} navState={itemStore.navState} setCheckedItems={itemStore.setCheckedItems} checkedItems={itemStore.checkedItems} namePressed={itemStore.namePressed} setNamePressed={itemStore.setNamePressed} />
    </div>
  );
}

export default observer(App);
