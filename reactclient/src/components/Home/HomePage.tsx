import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button} from "semantic-ui-react";

export default function HomePage() {
    return (
        <Segment inverted textAlighn='center' vertical className='masthead'>
            <Container textAlign='center'>
                <Header as='h1' inverted>
                    <Image size='massive' src='/logo512.png' alt='logo' style={{marginBottom: 12}}/>
                    OSRSPredator
                </Header>
                <Header as='h2' inverted content='Welcome to OSRSPredator' />
                <Button as={Link} to='/login' size='huge' inverted>Login</Button>
            </Container>
        </Segment>
    )
}