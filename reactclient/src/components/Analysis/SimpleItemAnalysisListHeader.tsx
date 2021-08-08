import { Grid, Icon, Item, List, Segment } from "semantic-ui-react";

function activateLasers() {
    alert("Todo: Implement sorting")
}

export default function SimpleItemAnalysisListHeader() {

    return (
        <Segment basic className="removePadding">
            <Grid>
                <Grid.Column width='1'>
                </Grid.Column>
                <Grid.Column width='1'>
                </Grid.Column>
                <Grid.Column width='4' onClick={activateLasers}>
                    <List horizontal className='hover-button'>
                    <Item>
                        Name
                    </Item>
                    <Item>
                        <Icon name="arrow up" />
                    </Item>
                    </List>
                </Grid.Column>
                <Grid.Column width='3' onClick={activateLasers}>
                <List horizontal className='hover-button'>
                    <Item>
                        High
                    </Item>
                    <Item>
                        <Icon name="arrow up" />
                    </Item>
                    </List>
                </Grid.Column>
                <Grid.Column width='3' onClick={activateLasers}>
                <List horizontal className='hover-button'>
                    <Item>
                        Low
                    </Item>
                    <Item>
                        <Icon name="arrow up" />
                    </Item>
                    </List>
                </Grid.Column>
                <Grid.Column width='2' onClick={activateLasers}>
                <List horizontal className='hover-button'>
                    <Item>
                        Delta
                    </Item>
                    <Item>
                        <Icon name="arrow up" />
                    </Item>
                    </List>
                </Grid.Column>
                <Grid.Column width='2' onClick={activateLasers}>
                <List horizontal className='hover-button'>
                    <Item>
                        Prediction
                    </Item>
                    <Item>
                        <Icon name="arrow up" />
                    </Item>
                    </List>
                </Grid.Column>
            </Grid>
        </Segment>
    )
}