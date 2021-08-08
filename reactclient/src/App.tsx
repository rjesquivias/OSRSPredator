import NavBar from "./components/NavBar";
import AnalysisDashboard from "./components/Analysis/AnalysisDashboard";
import { useEffect } from "react";
import { useStore } from "./stores/store";
import { observer } from "mobx-react-lite";

function App() {
  const { itemStore } = useStore();

  useEffect(() => {
      itemStore.loadSimpleItemAnalysisList();
  }, [itemStore]);

  return (
    <div className="container">
      <NavBar />
      <AnalysisDashboard />
    </div>
  );
}

export default observer(App);
