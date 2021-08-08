import axios from "axios";
import { observer } from "mobx-react-lite";
import { Grid, Container, Button } from "semantic-ui-react"
import ItemStore from "../../stores/itemStore";
import { useStore } from "../../stores/store";
import PaginationCompact from "../Pagination";
import AnalysisFilters from "./AnalysisFilters"
import SimpleItemAnalysisList from "./SimpleItemAnalysisList"
import SimpleItemAnalysisListHeader from "./SimpleItemAnalysisListHeader"

const watchItems = async (checkedItems: number[]) => {
    console.log(checkedItems)
    // convert list of names into the proper post format
    for (const id of checkedItems)
    {
        axios.get(`https://localhost:5001/api/v1/Analytics/${id}`).then((response) => {
            console.log(response.data);
            axios.post(`https://localhost:5001/api/v1/Analytics/Watchlist`, response.data);
        })
    }
}

const unwatchItems = async (checkedItems: number[]) => {
    console.log(checkedItems)
    // convert list of names into the proper post format
    for (const id of checkedItems)
    {
        axios.get(`https://localhost:5001/api/v1/Analytics/${id}`).then((response) => {
            console.log(response.data);
            axios.post(`https://localhost:5001/api/v1/Analytics/Unwatchlist`, response.data);
        })
    }
}

const renderPagination = (itemStore: ItemStore) => {
    if(itemStore.navState === "All Items")
        return <PaginationCompact />
}

export default observer(function AnalysisDashboard() {

    const { itemStore } = useStore();

    return (
        <Container>
            <Grid>
                <Grid.Column width='3' floated='right'>
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    <Button onClick={(e, data) => { itemStore.navState === "All Items" ? watchItems(itemStore.checkedItems) : unwatchItems(itemStore.checkedItems)}} content={itemStore.navState === "All Items" ? "Watch Items" : "Unwatch Items"} />
                </Grid.Column>
            </Grid>
            <Grid>
                <Grid.Column width='3' floated='right'>
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    <SimpleItemAnalysisListHeader />
                </Grid.Column>
            </Grid>

            <Grid>
                <Grid.Column width='3' floated='right'>
                    <AnalysisFilters />
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    <SimpleItemAnalysisList />
                </Grid.Column>
            </Grid>

            <Grid>
                <Grid.Column width='3' floated='right'>
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    {renderPagination(itemStore)}
                </Grid.Column>
            </Grid>
        </Container>
    )
})