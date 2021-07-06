import React, { useEffect, useState } from 'react'
import { Grid, List } from 'semantic-ui-react';
import axios, { AxiosResponse } from 'axios';
import { Card, Icon, Image } from 'semantic-ui-react'
import SimpleItemAnalysisSegment from './SimpleItemAnalysisSegment';

export default function SimpleItemAnalysisList() {

    const [simpleItemAnalysisList, setSimpleItemAnalysisList] = useState([]);
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
      
        // These options are needed to round to whole numbers if that's what you want.
        //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
        //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
      });

    useEffect(() => {
        axios.get('https://localhost:44313/api/v1/Analytics').then(response => {
            console.log(response);
            setSimpleItemAnalysisList(response.data);
        });
    }, []);

    return (
        <List>
            {simpleItemAnalysisList.map((simpleItemAnalysis: any) => (
                <List.Item key={simpleItemAnalysis.id}>
                    <SimpleItemAnalysisSegment 
                        name={simpleItemAnalysis.itemDetails.name} 
                        delta={simpleItemAnalysis.delta} 
                        examine={simpleItemAnalysis.itemDetails.examine} 
                        high={simpleItemAnalysis.mostRecentSnapshot.high} 
                        highTime={simpleItemAnalysis.mostRecentSnapshot.highTime} 
                        low={simpleItemAnalysis.mostRecentSnapshot.low} 
                        lowTime={simpleItemAnalysis.mostRecentSnapshot.lowTime} 
                        prediction={simpleItemAnalysis.prediction}
                    />
                </List.Item>
            ))}
        </List>
    )
}