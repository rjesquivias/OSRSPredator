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
    navState: string
    setCheckedItems: (checkedItems: any[]) => void
    checkedItems: any[]
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

const renderPagination = (setSimpleItemAnalysisList: any, pageSize: any, navState: any) => {
    if(navState === "All Items")
    {
        return <PaginationCompact page={1} totalPages={3800/pageSize} updatePage={(page) => {
            if(navState === "All Items") {
                axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=${pageSize}&page=${page}`).then(response => {
                    console.log(response);
                    setSimpleItemAnalysisList(response.data);
                });
            } else {
                // TODO: Implement frontend pagination
            }
        }} />
    }
}

export default function AnalysisDashboard({simpleItemAnalysisList, pageSize, setSimpleItemAnalysisList, navState, setCheckedItems, checkedItems} : Props) {

    return (
        <Container>
            <Grid>
                <Grid.Column width='3' floated='right'>
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    <Button onClick={(e, data) => { navState === "All Items" ? watchItems(checkedItems) : unwatchItems(checkedItems)}} content={navState === "All Items" ? "Watch Items" : "Unwatch Items"} />
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
                    <SimpleItemAnalysisList simpleItemAnalysisList={simpleItemAnalysisList} setCheckedItems={setCheckedItems} checkedItems={checkedItems}/>
                </Grid.Column>
            </Grid>

            <Grid>
                <Grid.Column width='3' floated='right'>
                </Grid.Column>
                <Grid.Column width='13' floated='right'>
                    {renderPagination(setSimpleItemAnalysisList, pageSize, navState)}
                </Grid.Column>
            </Grid>
        </Container>
    )
}