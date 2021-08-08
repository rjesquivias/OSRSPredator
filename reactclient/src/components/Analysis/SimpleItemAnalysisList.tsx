import { observer } from 'mobx-react-lite';
import { Segment, Image, Checkbox, Grid, Container, Header, List } from "semantic-ui-react"
import { useStore } from '../../stores/store';

const changeHandler = (e: any, data: any, id: any, setCheckedItems: (checkedItems: any[]) => void, checkedItems: any[]) => {
    if(data.checked) {
        setCheckedItems([...checkedItems, id])
    } else {
        setCheckedItems(checkedItems.filter(item => item !== id));
    }
}

export default observer(function SimpleItemAnalysisList() {
    
    const { itemStore } = useStore();

    return (
        <List>
            {itemStore.simpleItemAnalysisList && itemStore.simpleItemAnalysisList.map((simpleItemAnalysis: any) => (
                <List.Item key={simpleItemAnalysis.itemDetails.id}>
                    <Segment>
                        <Grid>
                            <Grid.Column width='1'>
                            <Checkbox checked={itemStore.checkedItems.find(checkedId => checkedId === (simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.id : "0")) != null} onChange={(e, data) => changeHandler(e, data, simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.id : "0", itemStore.setCheckedItems, itemStore.checkedItems)} />
                            </Grid.Column>
                            <Grid.Column width='1'>
                                <Image avatar src={`https://services.runescape.com/m=itemdb_oldschool/obj_big.gif?id=${simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.id : "0"}`} />
                            </Grid.Column>
                            <Grid.Column width='4'>
                                <Container>
                                    <Header as='h4'>{simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.name: 'null itemDetails'}</Header>
                                    {simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.examine: 'null itemDetails'}
                                </Container>
                            </Grid.Column>
                            <Grid.Column width='3'>
                                <Container>
                                    <Header as='h4'>{simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.high : 0}</Header>
                                    {new Date((simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.highTime: 0) * 1000).toLocaleTimeString("en-US")}
                                </Container>
                            </Grid.Column>
                            <Grid.Column width='3'>
                                <Container>
                                    <Header as='h4'>{simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.low : 0}</Header>
                                    {new Date((simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.lowTime : 0) * 1000).toLocaleTimeString("en-US")}
                                </Container>
                            </Grid.Column>
                            <Grid.Column width='2'>
                                {simpleItemAnalysis.delta}
                            </Grid.Column>
                            <Grid.Column width='1'>
                                {simpleItemAnalysis.prediction}
                            </Grid.Column>
                        </Grid>
                    </Segment>
                </List.Item>
            ))}
        </List>
    )
})
