import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { 
  Difficulty, 
  QuestionMimic as QuestionMimicModel, 
  QuestionOptionVideoToWord as QuestionOptionVideoToWordModel, 
  QuestionOptionWordToVideo as QuestionOptionWordToVideoModel, 
  QuestionQA as QuestionQAModel, 
  Test 
} from '../../models/test'
import { setErrors } from '../../redux/dashboard/dashboardSlice'
import { useAppDispatch, useAppSelector } from '../../redux/hooks'
import { getTestAsync } from '../../redux/test/actions'
import { PersistenceService } from '../../services/persistenceService'
import Pagination from '@mui/material/Pagination'
import Stack from '@mui/material/Stack'
import { Button, Chip, PaginationItem, Typography } from '@mui/material'
import { thunkDeleteCurrentTest, thunkSetPage } from '../../redux/test/testSlice'
import { testTypeToString } from '../../helpers/testType'
import { TestType } from '../../models/test'
import { difficultyToColor } from '../../helpers/difficulty'
import { QuestionOptionWordToVideo } from './QuestionOptionWordToVideo'
import { BaseQuestion } from '../../models/test/question';
import { QuestionOptionVideoToWord } from './QuestionOptionVideoToWord';
import { QuestionMimic } from './QuestionMimic'
import { QuestionQA } from './QuestionQA'
import InfoIcon from '@mui/icons-material/Info'
import { ResultsModal } from './ResultsModal'

interface TestInPersistence {
  state: 'loading' | 'error' | 'idle' | 'success',
  test: Test,
  page: number
}

interface ITestProps {
  editable: boolean
} 

export const TestComponent = ({editable} : ITestProps) => {
  const navigate = useNavigate()
  const { id } = useParams()
  const dispatch = useAppDispatch()
  const { currentTest } = useAppSelector(state => state.test)
  const [page, setPage] = useState<number>(currentTest.page ?? 1)
  const [open, setOpen] = useState<boolean>(false)
  
  useEffect(() => {
    // Only get if neccessary
    const currentInPersistence = new PersistenceService().get('currentTest') as TestInPersistence
    if (currentInPersistence?.test?.id === id) return

    dispatch(getTestAsync({ id: id ?? '', populated: true }))
  }, [dispatch, id])

  useEffect(() => {
    if (currentTest?.errors !== [])
      dispatch(setErrors(currentTest?.errors))
  }, [dispatch, currentTest?.errors])
  
  const handleOpenModal = () => { setOpen(true) }
  const handleCloseModal = () => { setOpen(false) }

  const handleStop = () => {
    dispatch(thunkDeleteCurrentTest())
    navigate(-1)
  }

  const handleOnPageChange = (e : any, page : number) => {
    dispatch(thunkSetPage(page))
    setPage(page)
  }

  const renderQuestion = (page : number) => {
    const question = currentTest.test?.questions[page-1] as BaseQuestion
    if (!question) return

    switch(currentTest.test?.testType) {
      case TestType.OptionWordToVideo || TestType.OptionWordToVideo_Error:
        return (
          <QuestionOptionWordToVideo question={question as QuestionOptionWordToVideoModel} editable={editable} />
        )
      case TestType.OptionVideoToWord || TestType.OptionVideoToWord_Error:
        return (
          <QuestionOptionVideoToWord question={question as QuestionOptionVideoToWordModel} editable={editable} />
        )
      case TestType.Mimic || TestType.Mimic_Error:
        return (
          <QuestionMimic question={question as QuestionMimicModel} editable={editable} />
        )
      case TestType.QA || TestType.QA_Error:
        return (
          <QuestionQA question={question as QuestionQAModel} editable={editable} />
        )
    }
  }

  return (
    <>
      <Stack spacing={2}>
        <Pagination 
          page={page}
          onChange={handleOnPageChange}
          count={currentTest.test?.questions.length}
          renderItem={ (item) => 
            <PaginationItem 
              {...item}
            />
          } 
        />
      </Stack>

      <Typography variant='h1' component='h1'>
        { testTypeToString(currentTest.test?.testType as TestType) }
      </Typography>

      <Chip 
        label={ currentTest.test?.difficulty as Difficulty } 
        color={difficultyToColor(currentTest.test?.difficulty as Difficulty)} 
      />

      <Button variant="outlined" startIcon={<InfoIcon />} onClick={ handleOpenModal }>
        Show test results
      </Button>

      <ResultsModal open={open} onClose={handleCloseModal} test={currentTest.test} />

      { renderQuestion(page) }

      <div>
          <Button 
            variant='outlined'
            onClick={() => { handleOnPageChange({}, page-1) }}
            disabled={(page)===1}
          >
            Previous
          </Button>

          <Button 
            variant='contained'
            onClick={() => { handleOnPageChange({}, page+1) }}
            disabled={(page)===currentTest.test?.questions.length}
          >
            Next
          </Button>

          <Button 
            variant='contained'
            color='warning'
            onClick={handleStop}
          >
            Stop
          </Button>
      </div>
    </>
  )
}
