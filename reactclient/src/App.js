import NavBar from "./components/NavBar";
import AnalysisDashboard from "./components/Analysis/AnalysisDashboard";
import { useEffect, useState } from "react";
import axios from "axios";

function App() {
  const [simpleItemAnalysisList, setSimpleItemAnalysisList] = useState([]);
  const [pageSize, setPageSize] = useState(20);
  useEffect(() => {
      axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=${pageSize}&page=1`).then(response => {
          console.log(response);
          setSimpleItemAnalysisList(response.data);
      });
  }, []);

  return (
    <div className="container">
      <NavBar setSimpleItemAnalysisList={setSimpleItemAnalysisList} />
      <AnalysisDashboard simpleItemAnalysisList={simpleItemAnalysisList} pageSize={pageSize} setSimpleItemAnalysisList={setSimpleItemAnalysisList} />
    </div>
  );
}

export default App;
