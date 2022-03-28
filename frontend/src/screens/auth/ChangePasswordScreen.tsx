import { Formik, Form } from 'formik'
import { useParams } from 'react-router-dom'
import { Typography, Button } from '@mui/material'

import * as AuthActions from '../../redux/auth/actions'
import { useAppDispatch } from '../../redux/hooks'
import { ChangePassword, ChangePasswordForm, ChangePasswordParams } from '../../models/auth'
import { initialValues, changePasswordSchema } from '../../helpers/change-password'
import { FormikField } from '../../components/formik'

export const ChangePasswordScreen = () => {
    const dispatch = useAppDispatch()
    const { email, token } = useParams<ChangePasswordParams>()
    initialValues.email = email ?? ''

    const handleSubmit = (values: ChangePasswordForm) => {
        dispatch(AuthActions.changePasswordAsync({
            email: values.email,
            password: values.password,
            token
        } as ChangePassword))
    }

    return (
        <div>
            <Typography variant='h1' component='h1'>Recover password</Typography>
            
            <Formik
                initialValues={initialValues}
                onSubmit={handleSubmit}
                validationSchema={changePasswordSchema}
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
                            Change password
                        </Button>
                    </Form>
                )}
            </Formik>
        </div>
    )
}
