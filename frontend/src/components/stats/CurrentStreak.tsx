import { useCallback, useEffect, useState } from 'react'
import { Typography } from '@mui/material'
import { useAppDispatch } from '../../redux/hooks'
import * as StatsApi from '../../api/stats'
import { setErrors } from '../../redux/dashboard/dashboardSlice'

export const CurrentStreak = () => {
  const dispatch = useAppDispatch()
  const [stat, setStat] = useState<number>(0)

  const getStat = useCallback(async (abortController: AbortController) => {
    const response = await StatsApi.getCurrentStreak(abortController)
    return response
  }, [])

  useEffect(() => {
    const abortController = new AbortController()

    const fetchStat = async () => {
      getStat(abortController)
        .then( async (result) => {
          if (!result.ok)
          {
            const error = (result.status === 401) 
              ? 'Your session has expired. Login again.'
              : 'Something went wrong'
            dispatch(setErrors([error]))
            return
          }

          const body = await result.json()
          setStat(body.stat)
        })
        .catch( (err) => {
          dispatch(setErrors(['Something went wrong']))
        })
    }

    fetchStat()

    return () => {
      abortController.abort()
    }
  }, [dispatch, getStat, stat])

  return (
    <div>
      <Typography variant='h2' component='h2'>Current streak: { stat }</Typography>
    </div>
  )
}
