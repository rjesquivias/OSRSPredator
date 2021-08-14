import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useStore } from "../../stores/store";
import LoadingComponent from "../LoadingComponent";

export default observer(function AnalysisDetails() {
    const { itemStore } = useStore();
    const {id} = useParams<{id: string}>();

    useEffect(() => {
        itemStore.getItemAnalytics(id)
    }, [id, itemStore]);
    
    if(itemStore.getIsDetailsLoading())
        return <LoadingComponent isActive={true} />

    return (
        <h1>{JSON.stringify(itemStore.getSelectedDetailsItem())}</h1>
    )
})