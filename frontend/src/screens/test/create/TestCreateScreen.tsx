import { Button,Typography } from '@mui/material'
import { Form, Formik } from 'formik'
import { FormikSelect } from '../../../components/formik/FormikSelect'
import { initialValues, testSchema as validationSchema } from '../../../helpers/create-test'
import { testTypeToString } from '../../../helpers/testType'
import { TestType, Difficulty } from '../../../models/test'
import { difficultyToString } from '../../../helpers/difficulty'
import { FormikField } from '../../../components/formik'
import { TestCreate } from '../../../models/test/testCreate'
import * as TestActions from '../../../api/test'
import { useAppDispatch } from '../../../redux/hooks'
import { setErrors } from '../../../redux/dashboard/dashboardSlice'
import { useNavigate } from 'react-router-dom'

const testTypes = Object.entries(TestType).map(([key]) => {
  const label = testTypeToString(key as TestType)
  return ({
    label,
    value: key
  })
})

const difficulties = Object.entries(Difficulty).map(([key]) => {
  const label = difficultyToString(key as Difficulty)
  return ({
    label,
    value: key
  })
})

export const TestCreateScreen = () => {
  const dispatch = useAppDispatch()
  const navigate = useNavigate()

  const handleSubmit = (values: TestCreate) : void => {
    TestActions.createTest(values)
      .then( async (result) => {
        const body = await result.json()

        if (!result.ok)
        {
          const errors = (result.status === 401) 
            ? ['Your session has expired. Login again.']
            : body.errors
          dispatch(setErrors(errors))
          return
        }

        navigate(`/test/do/${body.guid}`)
      })
      .catch((err) => {
        dispatch(setErrors(['Something went wrong']))
      })
  }

  return (
    <div>
      <Typography variant='h1' component='h1'>New Test</Typography>

      <Formik 
        initialValues={initialValues}
        onSubmit={handleSubmit}
        validationSchema={validationSchema}
        >
          {({isValid, dirty, errors, touched}) => (
            <Form>
              <div>
                <FormikSelect
                  name='testType'
                  label='Test type'
                  items={testTypes}
                  touched={touched.testType}
                  errorText={errors.testType}
                  required
                />
              </div>

              <div>
                <FormikSelect
                    name='difficulty'
                    label='Difficulty'
                    items={difficulties}
                    touched={touched.difficulty}
                    errorText={errors.difficulty}
                    required
                />
              </div>

              <div>
                <FormikField
                  fullWidth
                  name='numberOfQuestions'
                  label='Number of questions'
                  variant='standard'
                  touched={touched.numberOfQuestions}
                  errorText={errors.numberOfQuestions}
                  type='number'
                  InputProps={{
                    inputProps: {
                      min: 1,
                      max: 15,
                      step: 1
                    }
                  }}
                  required
                />
              </div>

              <Button 
                variant='contained'
                disabled={!dirty || !isValid}
                type='submit'
              >
                Create test
              </Button>
            </Form>
          )}
      </Formik>
    </div>
  )
}
