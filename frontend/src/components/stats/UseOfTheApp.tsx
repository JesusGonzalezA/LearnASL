import { useCallback, useEffect, useState } from 'react'
import { Typography } from '@mui/material'
import { useAppDispatch } from '../../redux/hooks'
import * as StatsApi from '../../api/stats'
import { setErrors } from '../../redux/dashboard/dashboardSlice'
import Calendar from 'react-calendar'
import { UseOfTheAppQueryFilter } from '../../models/queryFilters'
import 'react-calendar/dist/Calendar.css'
import './calendar.css'

export const UseOfTheApp = () => {
  const dispatch = useAppDispatch()
  const [stat, setStat] = useState<number[]>([])
  const [queryFilter, setQueryFilter] = useState<UseOfTheAppQueryFilter>({
      year: new Date().getFullYear(),
      month: new Date().getMonth() + 1
  })

  const handleViewChange = (e : any) => {
    const date = e.activeStartDate as Date
    const month = date.getMonth() + 1
    const year = date.getFullYear()
    setStat([])
    setQueryFilter({
        month, year
    })
  }

  const getStat = useCallback(async (queryFilter : UseOfTheAppQueryFilter, abortController: AbortController) => {
    const response = await StatsApi.getUseOfTheApp(queryFilter, abortController)
    return response
  }, [])

  useEffect(() => {
    const abortController = new AbortController()

    const fetchStat = async () => {
      getStat(queryFilter, abortController)
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
  }, [dispatch, getStat, queryFilter])

  return (
    <div>
        <Typography variant='h2' component='h2'>Use of the app</Typography>
        <Calendar 
            maxDate={new Date()}
            onActiveStartDateChange={handleViewChange}
            tileClassName={({ date, view }) => {
                return (
                    view === 'month' && 
                    stat.includes(date.getDate())
                ) ? 'calendar__day__used' : null 
            }}
        />
    </div>
  )
}
