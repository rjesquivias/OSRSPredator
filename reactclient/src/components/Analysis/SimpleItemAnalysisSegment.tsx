import { Segment, Image, Checkbox, Grid, Container, Header } from "semantic-ui-react"

interface Props {
    id: string,
    name: string,
    delta: number,
    examine: string,
    high: number,
    highTime: number,
    low: number,
    lowTime: number,
    prediction: number,
    setCheckedItems: (checkedItems: any[]) => void
    checkedItems: any[]
}

const changeHandler = (e: any, data: any, id: any, setCheckedItems: (checkedItems: any[]) => void, checkedItems: any[]) => {
    if(data.checked) {
        setCheckedItems([...checkedItems, id])
    } else {
        setCheckedItems(checkedItems.filter(item => item !== id));
    }
}

export default function SimpleItemAnalysisSegment({id, name, delta, examine, high, highTime, low, lowTime, prediction, setCheckedItems, checkedItems}: Props) {

    return (
        <Segment>
            <Grid>
                <Grid.Column width='1'>
                <Checkbox checked={checkedItems.find(checkedId => checkedId === id) != null} onChange={(e, data) => changeHandler(e, data, id, setCheckedItems, checkedItems)} />
                </Grid.Column>
                <Grid.Column width='1'>
                    <Image avatar src={`https://services.runescape.com/m=itemdb_oldschool/obj_big.gif?id=${id}`} />
                </Grid.Column>
                <Grid.Column width='4'>
                    <Container>
                        <Header as='h4'>{name}</Header>
                        {examine}
                    </Container>
                </Grid.Column>
                <Grid.Column width='3'>
                    <Container>
                        <Header as='h4'>{high}</Header>
                        {new Date(highTime * 1000).toLocaleTimeString("en-US")}
                    </Container>
                </Grid.Column>
                <Grid.Column width='3'>
                    <Container>
                        <Header as='h4'>{low}</Header>
                        {new Date(lowTime * 1000).toLocaleTimeString("en-US")}
                    </Container>
                </Grid.Column>
                <Grid.Column width='2'>
                    {delta}
                </Grid.Column>
                <Grid.Column width='1'>
                    {prediction}
                </Grid.Column>
            </Grid>
        </Segment>
    )
}