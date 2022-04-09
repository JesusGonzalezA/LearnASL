import { Box, Typography } from '@mui/material'
import { useEffect, useRef } from 'react'
import { fetchVideoAndSet } from '../../helpers/test'
import { QuestionQA as QuestionModel } from '../../models/test'
import { useAppSelector } from '../../redux/hooks'

const width = '320'
const height = '240'

interface QuestionQAProps {
  question: QuestionModel,
  editable: boolean,
  setCurrentAnswer: Function
}

export const QuestionQA = ({
  question, editable, setCurrentAnswer
} : QuestionQAProps) => {
    const { token } = useAppSelector(state => state.auth.user)
    const refVideo = useRef<HTMLVideoElement>(null)

    useEffect(() => {
      fetchVideoAndSet(question.videoUser ?? '', token ?? '', refVideo)
    },[question, token])

    const handleFileChosen = (e : any) => {
      const file = e.target.files[0]
      setCurrentAnswer(file)
      if(refVideo.current !== null) {
        const src = URL.createObjectURL(file)
        refVideo.current.src = src
      }
    }

    return (
        <>
            
            <Typography variant='h5' component='h2' sx={{ alignSelf: 'flex-start', marginBottom: 3}}>
                Guess: '{ question?.wordToGuess ?? '' }'
            </Typography>

            <Box sx={{ marginBottom: 3 }}>
              <video width={width} height={height} ref={refVideo} controls />
            </Box>
            
            {
              (editable) 
              ? (
                <>
                  <input type='file' onChange={handleFileChosen} value='' />
                </>
              )
              : (
                <>
                  <p>The sign is { (question.isCorrect) ? 'correct' : 'incorrect' }</p>
                </>
              )
            }
            
        </>
    )
}
