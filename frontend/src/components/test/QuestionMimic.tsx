import { Typography } from '@mui/material'
import { useEffect, useRef } from 'react'
import { fetchVideoAndSet } from '../../helpers/test'
import { QuestionMimic as QuestionModel } from '../../models/test'
import { useAppSelector } from '../../redux/hooks'

const width = '320'
const height = '240'

interface QuestionMimicProps {
  question: QuestionModel,
  editable: boolean
}

export const QuestionMimic = ({
  question, editable
} : QuestionMimicProps) => {
    const { token } = useAppSelector(state => state.auth.user)
    const refVideoHelp = useRef<HTMLVideoElement>(null)
    const refVideoUser = useRef<HTMLVideoElement>(null)

    useEffect(() => {
        fetchVideoAndSet(question.videoHelp ?? '', token ?? '', refVideoHelp)
        fetchVideoAndSet(question.videoUser ?? '', token ?? '', refVideoUser)
    },[question, token])

    return (
        <>
            <Typography variant='h2' component='h2'>
                Try signing: '{ question?.wordToGuess ?? '' }'
            </Typography>

            <p>Help video</p>
            <video width={width} height={height} ref={refVideoHelp} controls />

            {
              (editable) 
              ? (
                <></>
              )
              : (
                <>
                  <video width={width} height={height} ref={refVideoUser} controls />

                  <p>The sign is { (question.isCorrect) ? 'correct' : 'incorrect' }</p>
                </>
              )
            }
            
        </>
    )
}
