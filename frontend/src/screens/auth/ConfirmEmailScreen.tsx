import { Formik, Form } from 'formik'
import { useParams } from 'react-router-dom'
import { Typography, Button } from '@mui/material'

import * as AuthActions from '../../redux/auth/actions'
import { ConfirmEmail } from '../../models/auth'
import { useAppDispatch } from '../../redux/hooks'
import { confirmEmailSchema } from '../../helpers/confirm-email'
import { FormikField } from '../../components/formik/FormikField';

export const ConfirmEmailScreen = () => {
    const dispatch = useAppDispatch()
    const params = useParams<ConfirmEmail>()

    const confirmEmail = (values: ConfirmEmail) => {
        dispatch(AuthActions.confirmEmailAsync(values))
    }

    return (
        <div>
            <Typography variant='h1' component='h1'>Confirm email</Typography>
            
            <Formik
                initialValues={{
                    email: params.email ?? '',
                    token: params.token ?? ''
                }}
                onSubmit={confirmEmail}
                validationSchema={confirmEmailSchema}
                validateOnMount
            >
                {({errors, isValid}) => (
                    <Form>
                        <div>
                            <FormikField 
                                name='email'
                                type='input'
                                label='Email'
                                variant='standard'
                                errorText={errors.email}
                                touched={true}
                                required
                            />
                        </div>

                        <div>
                            <FormikField
                                name='token'
                                type='input'
                                label='Token'
                                variant='standard'
                                errorText={errors.token}
                                touched={true}
                                required
                            />
                        </div>

                        <div>
                            <Button 
                                variant='contained'
                                disabled={!isValid}
                                type='submit'
                            >
                                Confirm email
                            </Button>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
    )
}
