import axios from "axios";
import { Grid, Container, Segment } from "semantic-ui-react"
import PaginationCompact from "../Pagination";
import AnalysisFilters from "./AnalysisFilters"
import SimpleItemAnalysisList from "./SimpleItemAnalysisList"
import SimpleItemAnalysisListHeader from "./SimpleItemAnalysisListHeader"

interface Props {
    simpleItemAnalysisList: any[]
    pageSize: number
    setSimpleItemAnalysisList: (itemList: any[]) => void
}

export default function AnalysisDashboard({simpleItemAnalysisList, pageSize, setSimpleItemAnalysisList} : Props) {
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
                    <SimpleItemAnalysisList simpleItemAnalysisList={simpleItemAnalysisList}/>
                </Grid.Column>
            </Grid>

            <Grid>
                <Grid.Column width='6' floated='right'>
                </Grid.Column>
                <Grid.Column width='10' floated='right'>
                    <PaginationCompact page={1} totalPages={3800/pageSize} updatePage={(page) => {
                        axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=${pageSize}&page=${page}`).then(response => {
                            console.log(response);
                            setSimpleItemAnalysisList(response.data);
                        });
                    }} />
                </Grid.Column>
            </Grid>
        </Container>
    )
}