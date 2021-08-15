import { observer } from 'mobx-react-lite';
import { Segment, Image, Checkbox, Grid, Container, Header, List, Dimmer, Loader } from "semantic-ui-react"
import { useStore } from '../../stores/store';
import LoadingComponent from '../LoadingComponent';
import { Link } from 'react-router-dom';

export default observer(function SimpleItemAnalysisList() {
    
    const { itemStore } = useStore();

    if(itemStore.getIsListLoading())
        return <LoadingComponent isActive={true} />

    return (
        <List>
            {itemStore.simpleItemAnalysisList && itemStore.simpleItemAnalysisList.map((itemDetails: any) => (
                <List.Item key={itemDetails.id}>
                    <Segment>
                        <Grid>
                            <Grid.Column width='1'>
                            <Checkbox checked={itemStore.checkedItems.find(checkedItem => checkedItem.id === itemDetails.id)} onChange={(e, data) => {
                                if(data.checked) {
                                    itemStore.setCheckedItems([...itemStore.checkedItems, itemDetails])
                                } else {
                                    itemStore.setCheckedItems(itemStore.checkedItems.filter(item => item.id !== itemDetails.id));
                                }
                            }} />
                            </Grid.Column>
                            <Grid.Column width='1'>
                                <Image dimmer={<LoadingComponent isActive={!itemStore.isImageLoaded(itemDetails.id)} />} avatar src={itemStore.ITEM_AVATAR_URL + `${itemDetails.id }`} onLoad={() => {itemStore.setImageLoaded(itemDetails.id, true)}} />
                            </Grid.Column>
                            <Grid.Column width='4'>
                                <Container>
                                    <Header as={Link} to={`/itemDashboard/${itemDetails.id}`}>{itemDetails.name}</Header>
                                    {itemDetails.examine}
                                </Container>
                            </Grid.Column>
                            <Grid.Column width='3'>
                                <Container>
                                    <Header as='h4'>{itemDetails.mostRecentSnapshot ? itemDetails.mostRecentSnapshot.high : 0}</Header>
                                    {new Date((itemDetails.mostRecentSnapshot ? itemDetails.mostRecentSnapshot.highTime: 0) * 1000).toLocaleTimeString("en-US")}
                                </Container>
                            </Grid.Column>
                            <Grid.Column width='3'>
                                <Container>
                                    <Header as='h4'>{itemDetails.mostRecentSnapshot ? itemDetails.mostRecentSnapshot.low : 0}</Header>
                                    {new Date((itemDetails.mostRecentSnapshot ? itemDetails.mostRecentSnapshot.lowTime : 0) * 1000).toLocaleTimeString("en-US")}
                                </Container>
                            </Grid.Column>
                            <Grid.Column width='2'>
                                {itemDetails.delta ? itemDetails.delta : 0}
                            </Grid.Column>
                            <Grid.Column width='1'>
                                {itemDetails.prediction}
                            </Grid.Column>
                        </Grid>
                    </Segment>
                </List.Item>
            ))}
        </List>
    )
})
