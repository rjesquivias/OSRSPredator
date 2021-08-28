import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { Button, Container, Grid, Label } from "semantic-ui-react";
import { useStore } from "../../stores/store";
import TextInput from "../TextInput";

export default observer(function LoginForm() {
    const {userStore} = useStore();

    return (
        <Container>
            <Grid columns={3}>
                <Grid.Row>
                <Grid.Column>
                </Grid.Column>
                <Grid.Column>
                    <Formik 
                    initialValues={{email: '', password: '', error: null}}
                    onSubmit={(values, {setErrors}) => userStore.login(values).catch(error => 
                        setErrors({error: 'Invalid email or password'}))}
                    >
                        {({handleSubmit, isSubmitting, errors}) => (
                            <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                <TextInput name='email' placeholder='Email' />
                                <TextInput name='password' placeholder='Password' type='password'/>
                                <ErrorMessage 
                                    name='error' render={() => <Label style={{marginBottom: 10}} basic color='red' content={errors.error} />}    
                                />
                                <Button loading={isSubmitting} positive content='Login' type='submit' fluid />
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
})