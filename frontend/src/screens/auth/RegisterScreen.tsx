import { Link } from 'react-router-dom'
import { Formik, Form } from 'formik'
import { Button, Typography } from '@mui/material'

import { useAppDispatch } from '../../redux/hooks'
import * as AuthActions from '../../redux/auth/actions'
import { Register } from '../../models/auth'
import { FormikField } from '../../components/formik'
import { initialValues, registerSchema } from '../../helpers/register'
import { Dialog } from '../../components/dialog/Dialog'
import { ResendConfirmationEmailForm } from '../../components/resend-confirmation-email/ResendConfirmationEmailForm'

export const RegisterScreen = () => {
    const dispatch = useAppDispatch()
    
    const handleSubmit = (values: Register) : void => {
        dispatch(AuthActions.registerAsync(values))
    }

    return (
        <div>
            <Typography variant='h1' component='h1'>Register</Typography>
            <Formik 
                initialValues={initialValues}
                onSubmit={handleSubmit}
                validationSchema={registerSchema}
                validateOnMount
            >
                {({isValid, dirty, errors, touched}) => (
                    <Form>
                        <div>
                            <FormikField 
                                name='email'
                                type='input' 
                                label='Email' 
                                variant='standard' 
                                errorText={errors.email} 
                                touched={touched.email}
                                required
                            />
                        </div>

                        <div>
                            <FormikField 
                                name='password' 
                                type='password'
                                label='Password' 
                                variant='standard' 
                                errorText={errors.password}
                                touched={touched.password}
                                required 
                            />
                        </div>

                        <div>
                            <FormikField 
                                name='repeatPassword'
                                type='password'
                                label='Repeat the password' 
                                variant='standard'
                                errorText={errors.repeatPassword}
                                touched={touched.repeatPassword}
                                required
                            />
                        </div>                        
                   
                        <Button 
                            variant='contained'
                            disabled={!dirty || !isValid}
                            type='submit'
                        >
                            Register
                        </Button>
                    </Form>
                )}
            </Formik>

            <div>
                <Link 
                    to='/auth/login'
                >
                    Already have an account?
                </Link>
            </div>

            <Dialog
                messageButton='Resend confirmation email'
                title='Resend confirmation email'
                content='Enter the email to resend the confirmation email' 
                component={
                    <ResendConfirmationEmailForm />
                }
            />
        </div>
    )
}
