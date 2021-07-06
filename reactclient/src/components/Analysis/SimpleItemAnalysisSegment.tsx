import { Segment, Image, Checkbox, Grid, Container, Header } from "semantic-ui-react"

interface Props {
    name: string,
    delta: number,
    examine: string,
    high: number,
    highTime: number,
    low: number,
    lowTime: number,
    prediction: number
}

export default function SimpleItemAnalysisSegment({name, delta, examine, high, highTime, low, lowTime, prediction}: Props) {
    return (
        <Segment>
            <Grid>
                <Grid.Column width='1'>
                <Checkbox />
                </Grid.Column>
                <Grid.Column width='2'>
                    <Image avatar src='https://oldschool.runescape.wiki/images/5/53/Elysian_spirit_shield.png?e6bb3' />
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