import React, { useEffect, useState } from 'react'
import { Grid, List } from 'semantic-ui-react';
import axios, { AxiosResponse } from 'axios';
import { Card, Icon, Image } from 'semantic-ui-react'

export default function AnalysisDashboard() {

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
        <Grid>
            <Grid.Column width='10'>
            <List>
                {simpleItemAnalysisList.map((simpleItemAnalysis: any) => (
                    <List.Item key={simpleItemAnalysis.id}>
                        <Card>
                            <Image src={"https://oldschool.runescape.wiki/images/5/53/Elysian_spirit_shield.png?e6bb3"} wrapped ui={false} />
                            <Card.Content>
                                <Card.Header>{simpleItemAnalysis.itemDetails.name}</Card.Header>
                                <Card.Meta>
                                    <span className='date'>Delta: {formatter.format(simpleItemAnalysis.delta)}</span>
                                </Card.Meta>
                                <Card.Description>
                                    {simpleItemAnalysis.itemDetails.examine}
                                </Card.Description>
                                <Card.Description>
                                    Instant Buy: {formatter.format(simpleItemAnalysis.mostRecentSnapshot.high)}
                                </Card.Description>
                                <Card.Description>
                                    {new Date(simpleItemAnalysis.mostRecentSnapshot.highTime * 1000).toLocaleTimeString("en-US")}
                                </Card.Description>
                                <Card.Description>
                                    Instant Sell: {formatter.format(simpleItemAnalysis.mostRecentSnapshot.low)}
                                </Card.Description>
                                <Card.Description>
                                    {new Date(simpleItemAnalysis.mostRecentSnapshot.lowTime * 1000).toLocaleTimeString("en-US")}
                                </Card.Description>
                            </Card.Content>
                            <Card.Content extra>
                            <a>
                                <Icon name='user' />
                                Prediction (To Be Implemented): {simpleItemAnalysis.prediction}
                            </a>
                            </Card.Content>
                        </Card>
                    </List.Item>
                ))}
            </List>
            </Grid.Column>
        </Grid>
    )
}