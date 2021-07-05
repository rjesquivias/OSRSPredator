import React, { useEffect, useState } from 'react'
import { Grid, List } from 'semantic-ui-react';
import axios, { AxiosResponse } from 'axios';

export default function AnalysisDashboard() {

    const [simpleItemAnalysisList, setSimpleItemAnalysisList] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:44313/api/v1/Analytics').then(response => {
            console.log(response);
            setSimpleItemAnalysisList(response.data);
        });
    }, []);

    return (
        <Grid>
            <Grid.Column width='10'>
            <List>
                {simpleItemAnalysisList.map((simpleItemAnalysis: any) => (
                    <List.Item key={simpleItemAnalysis.id}>
                        Delta: {simpleItemAnalysis.delta}
                        High Price: {simpleItemAnalysis.mostRecentSnapshot.high}
                        High Time: {simpleItemAnalysis.mostRecentSnapshot.highTime}
                        Low Price: {simpleItemAnalysis.mostRecentSnapshot.low}
                        Low Time: {simpleItemAnalysis.mostRecentSnapshot.lowTime}
                        Examine Text: {simpleItemAnalysis.itemDetails.examine}
                        Item Id: {simpleItemAnalysis.itemDetails.id}
                        Members: {simpleItemAnalysis.itemDetails.members}
                        Lowalch price: {simpleItemAnalysis.itemDetails.lowalch}
                        Buy limit: {simpleItemAnalysis.itemDetails.limit}
                        Value: {simpleItemAnalysis.itemDetails.value}
                        Highalch limit: {simpleItemAnalysis.itemDetails.highalch}
                        Icon: {simpleItemAnalysis.itemDetails.icon}
                        Name: {simpleItemAnalysis.itemDetails.name}
                        Prediction: {simpleItemAnalysis.prediction}
                    </List.Item>
                ))}
            </List>
            </Grid.Column>
        </Grid>
    )
}