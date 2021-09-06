import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { Button, Header, Label } from "semantic-ui-react";
import { useStore } from "../../stores/store";
import TextInput from "../TextInput";
import * as Yup from 'yup';
import ValidationErrors from "../../errors/ValidationErrors";

export default observer(function RegisterForm() {
    const {userStore} = useStore();

    return (
        <Formik 
        initialValues={{displayName: '', username: '', email: '', password: '', error: null}}
        onSubmit={(values, {setErrors}) => userStore.register(values).catch(error => 
            setErrors({error}))}
        validationSchema={Yup.object({
            email: Yup.string().required().email(),
            password: Yup.string().required(),
        })}
        >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className='ui form error' onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Sign up to OSRSPredator' color='teal' textAlign='center' />
                    <TextInput name='email' placeholder='Email' />
                    <TextInput name='password' placeholder='Password' type='password'/>
                    <ErrorMessage 
                        name='error' render={() => 
                            <ValidationErrors errors={errors.error} />
                        }    
                    />
                    <Button disabled={!isValid || !dirty || isSubmitting} loading={isSubmitting} positive content='Register' type='submit' fluid />
                </Form>
            )}
        </Formik>
    )
})