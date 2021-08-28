import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button} from "semantic-ui-react";
import { useStore } from "../../stores/store";
import LoginForm from "../Users/LoginForm";
import RegisterForm from "../Users/RegisterForm";

export default observer(function HomePage() {
    const {userStore, modalStore} = useStore();

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
                    <>
                        <Button onClick={() => modalStore.openModal(<LoginForm />)} size='huge' inverted>Login</Button>
                        <Button onClick={() => modalStore.openModal(<RegisterForm />)} size='huge' inverted>Register</Button>
                    </>
                )}

            </Container>
        </Segment>
    )
})