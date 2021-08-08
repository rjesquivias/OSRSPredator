import { Dimmer, Loader } from "semantic-ui-react";

interface Props {
    isActive: boolean
}

export default function LoadingComponent({ isActive } : Props) {
    return (
        <Dimmer active={isActive} inverted={true}>
            <Loader content={"Loading..."}/>
        </Dimmer>
    )
}