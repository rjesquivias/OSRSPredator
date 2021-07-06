import { useState } from "react";
import { Grid, Container, Segment } from "semantic-ui-react"
import AnalysisFilters from "./AnalysisFilters"
import SimpleItemAnalysisList from "./SimpleItemAnalysisList"
import SimpleItemAnalysisListHeader from "./SimpleItemAnalysisListHeader"

export default function AnalysisDashboard() {
    return (
        <Container>
            <Grid>
                <Grid.Column width='6' floated='right'>
                </Grid.Column>
                <Grid.Column width='10' floated='right'>
                    <SimpleItemAnalysisListHeader />
                </Grid.Column>
            </Grid>

            <Grid>
                <Grid.Column width='6' floated='right'>
                    <AnalysisFilters />
                </Grid.Column>
                <Grid.Column width='10' floated='right'>
                    <SimpleItemAnalysisList />
                </Grid.Column>
            </Grid>
        </Container>
    )
}