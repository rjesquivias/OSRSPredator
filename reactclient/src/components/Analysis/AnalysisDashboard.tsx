import axios from "axios";
import { useState } from "react";
import { Grid, Container, Segment, Button } from "semantic-ui-react"
import PaginationCompact from "../Pagination";
import AnalysisFilters from "./AnalysisFilters"
import SimpleItemAnalysisList from "./SimpleItemAnalysisList"
import SimpleItemAnalysisListHeader from "./SimpleItemAnalysisListHeader"

interface Props {
    simpleItemAnalysisList: any[]
    pageSize: number
    setSimpleItemAnalysisList: (itemList: any[]) => void
}

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

export default function AnalysisDashboard({simpleItemAnalysisList, pageSize, setSimpleItemAnalysisList} : Props) {
    const [checkedItems, setCheckedItems] = useState<any>([]);

    return (
        <Container>
            <Grid>
                <Grid.Column width='6' floated='right'>
                </Grid.Column>
                <Grid.Column width='10' floated='right'>
                    <Button onClick={(e, data) => watchItems(checkedItems)}>Watch Items</Button>
                </Grid.Column>
            </Grid>
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
                    <SimpleItemAnalysisList simpleItemAnalysisList={simpleItemAnalysisList} setCheckedItems={setCheckedItems} checkedItems={checkedItems}/>
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