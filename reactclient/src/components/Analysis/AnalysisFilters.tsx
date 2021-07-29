import { useState } from "react";
import { Button, Grid, Icon, Item, Segment } from "semantic-ui-react"

function loadOptionalSegment(namePressed: any) {
    if(namePressed)
        return <Segment>OPTIONAL</Segment>
}

export default function AnalysisFilters() {
    const [namePressed, setNamePressed] = useState(false);

    return (
        <Segment.Group>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Name 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue' onClick={() => setNamePressed(!namePressed)}>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid>
            </Segment>
            {loadOptionalSegment(namePressed)}
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Buy Price 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue'>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Sell Price 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue'>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid></Segment>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Buy Time 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue'>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid></Segment>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Sell Time 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue'>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid></Segment>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Delta 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue'>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid></Segment>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Prediction 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue'>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid></Segment>
        </Segment.Group>
    ) 
}