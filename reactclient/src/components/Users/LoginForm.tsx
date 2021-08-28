import { Form, Formik } from "formik";
import { Button, Container, Grid } from "semantic-ui-react";
import TextInput from "../TextInput";

export default function LoginForm() {
    return (
        <Container>
            <Grid columns={3}>
                <Grid.Row>
                <Grid.Column>
                </Grid.Column>
                <Grid.Column>
                    <Formik 
                    initialValues={{email: '', password: ''}}
                    onSubmit={values => console.log(values)}
                    >
                        {({handleSubmit}) => (
                            <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                <TextInput name='email' placeholder='Email' />
                                <TextInput name='password' placeholder='Password' type='password'/>
                                <Button positive content='Login' type='submit' fluid />
                            </Form>
                        )}
                    </Formik>
                </Grid.Column>
                <Grid.Column>
                </Grid.Column>
                </Grid.Row>
            </Grid>
        </Container>
    )
}