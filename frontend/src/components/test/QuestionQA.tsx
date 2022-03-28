import { Typography } from '@mui/material'
import { useEffect, useRef } from 'react'
import { fetchVideoAndSet } from '../../helpers/test'
import { QuestionQA as QuestionModel } from '../../models/test'
import { useAppSelector } from '../../redux/hooks'

const width = '320'
const height = '240'

interface QuestionQAProps {
  question: QuestionModel,
  editable: boolean
}

export const QuestionQA = ({
  question, editable
} : QuestionQAProps) => {
    const { token } = useAppSelector(state => state.auth.user)
    const refVideo = useRef<HTMLVideoElement>(null)

    useEffect(() => {
        fetchVideoAndSet(question.videoUser ?? '', token ?? '', refVideo)
    },[question, token])

    return (
        <>
            <Typography variant='h2' component='h2'>
                Guess: '{ question?.wordToGuess ?? '' }'
            </Typography>

            {
              (editable) 
              ? (
                <></>
              )
              : (
                <>
                  <video width={width} height={height} ref={refVideo} controls />

                  <p>The sign is { (question.isCorrect) ? 'correct' : 'incorrect' }</p>
                </>
              )
            }
            
        </>
    )
}
