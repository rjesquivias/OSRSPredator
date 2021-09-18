import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useStore } from "../../stores/store";
import LoadingComponent from "../LoadingComponent";
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Snapshot } from "../../models/snapshot";

export default observer(function AnalysisDetails() {
    const { itemStore } = useStore();
    const { id } = useParams<{id: string}>();
    const [isBusy, setBusy] = useState(true)

    useEffect(() => {
        async function fetchMyAPI() {
            setBusy(true);
            await itemStore.getItemAnalytics(id)
            var snapshotList: any[] = itemStore.getSelectedDetailsItem().data.itemHistoricalList;
            itemStore.snapshotList = [];

            snapshotList.forEach(snapshot => {
                var obj = {} as Snapshot;

                obj.high = snapshot.high;
                obj.highTime = snapshot.highTime;
                obj.low = snapshot.low;
                obj.lowTime = snapshot.lowTime;

                itemStore.snapshotList.push(obj);
            });
            setBusy(false);
        }
        
        fetchMyAPI();
    }, [id, itemStore]);
    
    if(itemStore.getIsDetailsLoading())
        return <LoadingComponent isActive={true} />

    return (
        <>
        <h1>{JSON.stringify(itemStore.snapshotList)}</h1>
        
        {isBusy == false ? 
        <LineChart width={600} height={300} data={itemStore.snapshotList} margin={{ top: 5, right: 20, bottom: 5, left: 0 }}>
            <Line type="monotone" dataKey="high" stroke="#8884d8" />
            <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
            <XAxis dataKey="highTime" />
            <YAxis />
            <Tooltip />
        </LineChart> : <h1>empty</h1>
        }

        </>
    )
})