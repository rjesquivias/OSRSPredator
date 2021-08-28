import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button} from "semantic-ui-react";
import { useStore } from "../../stores/store";

export default observer(function HomePage() {
    const {userStore} = useStore();

    return (
        <Segment inverted textAlighn='center' vertical className='masthead'>
            <Container textAlign='center'>
                <Header as='h1' inverted>
                    <Image size='massive' src='/logo512.png' alt='logo' style={{marginBottom: 12}}/>
                    OSRSPredator
                </Header>
                {userStore.isLoggedIn ? (
                    <>
                        <Header as='h2' inverted content='Welcome to OSRSPredator' />
                        <Button as={Link} to='/itemDashboard' size='huge' inverted>Dashboard</Button>
                    </>
                ): (
                    <Button as={Link} to='/login' size='huge' inverted>Login</Button>
                )}

            </Container>
        </Segment>
    )
})