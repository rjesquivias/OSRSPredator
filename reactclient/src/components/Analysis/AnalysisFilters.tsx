import { observer } from "mobx-react-lite";
import { Button, Grid, Icon, Segment } from "semantic-ui-react"
import { useStore } from "../../stores/store"

function loadOptionalSegment(namePressed: any) {
    if(namePressed)
        return <Segment>OPTIONAL</Segment>
}

export default observer(function AnalysisFilters() {

    const { itemStore } = useStore();

    return (
        <Segment.Group>
            <Segment>
                <Grid>
                    <Grid.Column width='12'>
                        Name 
                    </Grid.Column>
                    <Grid.Column width='4'>
                        <Button circular icon size='tiny' color='blue' onClick={() => itemStore.setNamePressed(!itemStore.namePressed)}>
                            <Icon name='plus'></Icon>
                        </Button>
                    </Grid.Column>
                </Grid>
            </Segment>
                {loadOptionalSegment(itemStore.namePressed)}
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
})