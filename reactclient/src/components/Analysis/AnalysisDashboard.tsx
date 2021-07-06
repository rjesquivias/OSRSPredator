import { Grid, Container } from "semantic-ui-react"
import SimpleItemAnalysisList from "./SimpleItemAnalysisList"

export default function AnalysisDashboard() {
    return (
        <Container>
            <Grid>
                <Grid.Column width='6' floated='right'>
                    A
                </Grid.Column>
                <Grid.Column width='10' floated='right'>
                    <SimpleItemAnalysisList />
                </Grid.Column>
            </Grid>
        </Container>
    )
}