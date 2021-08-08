import { observer } from "mobx-react-lite";
import { Grid, Container, Button } from "semantic-ui-react"
import { useStore } from "../../stores/store";
import PaginationCompact from "../Pagination";
import AnalysisFilters from "./AnalysisFilters"
import SimpleItemAnalysisList from "./SimpleItemAnalysisList"
import SimpleItemAnalysisListHeader from "./SimpleItemAnalysisListHeader"

export default observer(function AnalysisDashboard() {

    const { itemStore } = useStore();
    return (
        <Container>
            <Grid>
                <Grid.Column width='3' floated='right'>
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    <Button onClick={(e, data) => { itemStore.navState === itemStore.ALL_ITEMS ? itemStore.watchItems() : itemStore.unwatchItems()}} content={itemStore.navState === itemStore.ALL_ITEMS ? itemStore.WATCH_ITEMS : itemStore.UNWATCH_ITEMS} />
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
                    {itemStore.navState === itemStore.ALL_ITEMS && <PaginationCompact />}
                </Grid.Column>
            </Grid>
        </Container>
    )
})