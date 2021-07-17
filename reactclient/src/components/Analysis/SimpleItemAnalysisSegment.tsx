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
    prediction: number
}

export default function SimpleItemAnalysisSegment({id, name, delta, examine, high, highTime, low, lowTime, prediction}: Props) {
    return (
        <Segment>
            <Grid>
                <Grid.Column width='1'>
                <Checkbox />
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