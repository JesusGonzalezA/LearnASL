import { Formik, Form } from 'formik'
import { Link } from 'react-router-dom'
import { Button, Typography } from '@mui/material'

import { useAppDispatch } from '../../redux/hooks'
import { Login } from '../../models/auth'
import { FormikField } from '../../components/formik'
import { Dialog } from '../../components/dialog/Dialog'
import { loginSchema, initialValues } from '../../helpers/login'
import { ResetPasswordForm } from '../../components/reset-password/ResetPasswordForm'
import * as AuthActions from '../../redux/auth/actions'

export const LoginScreen = () => {
    const dispatch = useAppDispatch()
    
    const handleSubmit = (values: Login) : void => {
        dispatch(AuthActions.loginAsync(values))
    }

    return (
        <div>
            <Typography variant='h1' component='h1'>Login</Typography>
            <Formik 
                initialValues={initialValues}
                onSubmit={handleSubmit}
                validationSchema={loginSchema}
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

                        <Button 
                            variant='contained'
                            disabled={!dirty || !isValid}
                            type='submit'
                        >
                            Login
                        </Button>
                    </Form> 
                )}
            </Formik>

            <div>
                <Link 
                    to="/auth/register"
                >
                    Don't have an account?
                </Link>
            </div>  

            <Dialog
                messageButton='Reset password'
                title='Reset password'
                content='Enter your email to remember the password' 
                component={
                    <ResetPasswordForm />
                }
            /> 
        </div>
    )
}
