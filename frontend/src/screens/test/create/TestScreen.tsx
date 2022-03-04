import { Button,Typography } from '@mui/material'
import { Form, Formik } from 'formik'
import { FormikSelect } from '../../../components/formik/FormikSelect'
import { initialValues, testSchema as validationSchema } from '../../../helpers/create-test'
import { testTypeToString } from '../../../helpers/testType'
import { TestType, Difficulty } from '../../../models/test'
import { difficultyToString } from '../../../helpers/difficulty'

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

export const TestScreen = () => {

  const handleSubmit = () => {

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
