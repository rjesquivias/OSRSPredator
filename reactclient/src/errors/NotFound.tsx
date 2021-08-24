import { Link } from "react-router-dom";
import { Button, Header, Icon, Segment } from "semantic-ui-react";

export default function NotFound() {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                Woah!! Couldn't find whatever that was.
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/itemDashboard' primary>
                    Dashboard
                </Button>
            </Segment.Inline>
        </Segment>
    )
}