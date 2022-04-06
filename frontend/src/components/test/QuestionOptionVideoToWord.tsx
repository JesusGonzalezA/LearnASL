import { FormControl, FormControlLabel, Paper, Radio, RadioGroup, Typography } from '@mui/material'
import { useEffect, useMemo, useRef, useState } from 'react'
import { fetchVideoAndSet } from '../../helpers/test'
import { QuestionOptionVideoToWord as QuestionModel } from '../../models/test'
import { useAppSelector } from '../../redux/hooks'
import { computeTypeOptionVideoToWord as computeType } from '../../helpers/test'
import { typeToColor } from '../../helpers/test/typeToColor';

const width = '320'
const height = '240'

interface QuestionOptionVideoToWordProps {
  question: QuestionModel,
  editable: boolean,
  setCurrentAnswer: Function
}

export const QuestionOptionVideoToWord = ({
  question, editable, setCurrentAnswer
} : QuestionOptionVideoToWordProps) => {
    const { token } = useAppSelector(state => state.auth.user)
    const [value, setValue] = useState<string>(question.userAnswer ?? '')
    const questions = useMemo(() => (
        [
            question.possibleAnswer0, 
            question.possibleAnswer1, 
            question.possibleAnswer2, 
            question.possibleAnswer3
        ])
    , [question])
    const refVideo = useRef<HTMLVideoElement>(null)

    useEffect(() => {
        fetchVideoAndSet(question.videoToGuess, token ?? '', refVideo)
    },[question, token])

    useEffect(() => {
      setValue(question.userAnswer ?? '')
  }, [question, questions])

    const handleOnChange = (ev : any, value : string) => {
        if (editable) {
          setValue(value)
          setCurrentAnswer(value)
        }
    }

    const getColor = (label : string) => {
      const type = computeType(label, editable, question, questions)
      const color = typeToColor(type)
      return color
    }

    return (
        <>
            <Typography variant='h2' component='h2'>
                To guess:
            </Typography>

            <video width={width} height={height} ref={refVideo} controls />

            <FormControl>
              <RadioGroup value={value} onChange={handleOnChange}>
                <Paper variant='outlined' sx={{borderColor: getColor(question.possibleAnswer0)}}>
                  <FormControlLabel
                    value={question.possibleAnswer0} 
                    control={<Radio />} 
                    label={question.possibleAnswer0} 
                  />
                </Paper>
                <Paper variant='outlined' sx={{borderColor: getColor(question.possibleAnswer1)}}>
                  <FormControlLabel 
                    value={question.possibleAnswer1} 
                    control={<Radio />} 
                    label={question.possibleAnswer1} 
                  />
                </Paper>
                <Paper variant='outlined' sx={{borderColor: getColor(question.possibleAnswer2)}}>
                  <FormControlLabel 
                    value={question.possibleAnswer2} 
                    control={<Radio />} 
                    label={question.possibleAnswer2} 
                  />
                </Paper>
                <Paper variant='outlined' sx={{borderColor: getColor(question.possibleAnswer3)}}>
                  <FormControlLabel 
                    value={question.possibleAnswer3} 
                    control={<Radio />} 
                    label={question.possibleAnswer3} 
                  />
                </Paper>
              </RadioGroup>
            </FormControl>
        </>
    )
}
