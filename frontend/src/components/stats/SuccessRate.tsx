import { useCallback, useEffect, useState } from 'react'
import { FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Typography } from '@mui/material'
import { useAppDispatch } from '../../redux/hooks'
import * as StatsApi from '../../api/stats'
import { setErrors } from '../../redux/dashboard/dashboardSlice'
import { SuccessRateQueryFilter } from '../../models/queryFilters'
import { Difficulty } from '../../models/test'
import { PieChart } from 'react-minimal-pie-chart'

interface Filter {
  temporal: 'year' | 'month' | 'day',
  value: SuccessRateQueryFilter
}
export const SuccessRate = () => {
  const dispatch = useAppDispatch()
  const [stat, setStat] = useState<number>(0)
  const [apiFilter, setApiFilter] = useState<Filter>({
    temporal: 'year',
    value: { year: new Date().getFullYear(), difficulty: Difficulty.NOTDEFINED }
  })

  const onChangeFilterTemporal = (event: React.ChangeEvent<HTMLInputElement>, value : any) => {
    let newApiFilter : Filter = {
      temporal: 'year',
      value: { year: new Date().getFullYear() }
    }

    switch(value) {
      case 'day': 
          newApiFilter = {
              temporal: 'day',
              value: {
                  ...newApiFilter.value,
                  difficulty: apiFilter.value.difficulty,
                  month: new Date().getMonth() + 1,
                  day: new Date().getDate()
              }
          }
          break;
      case 'month': 
          newApiFilter = {
              temporal: 'month',
              value: {
                  ...newApiFilter.value,
                  difficulty: apiFilter.value.difficulty,
                  month: new Date().getMonth() + 1
              }
          }
          break;
      case 'year':
          newApiFilter = {
              temporal: 'year',
              value: {
                  ...newApiFilter.value,
                  difficulty: apiFilter.value.difficulty,
              }
          }
          break;
  }

    setApiFilter(newApiFilter)
  }

  const onChangeFilterDifficulty = (event: React.ChangeEvent<HTMLInputElement>, value: string) => {
    setApiFilter({
      ...apiFilter,
      value: {
        ...apiFilter.value,
        difficulty: value as Difficulty
      }
    })
  }

  const getStat = useCallback(async (abortController: AbortController) => {
    const response = await StatsApi.getSuccessRate(apiFilter.value, abortController)
    return response
  }, [apiFilter])

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
  }, [dispatch, getStat])

    return (
        <div>
          <Typography variant='h2' component='h2'>Success Rate: { stat }</Typography>

          <FormControl>
              <FormLabel>Filter by difficulty</FormLabel>
              <RadioGroup value={apiFilter.value.difficulty} onChange={onChangeFilterDifficulty}>
                  <FormControlLabel value={Difficulty.NOTDEFINED} control={<Radio />} label='None' />
                  <FormControlLabel value={Difficulty.EASY} control={<Radio />} label='Easy' />
                  <FormControlLabel value={Difficulty.MEDIUM} control={<Radio />} label='Medium' />
                  <FormControlLabel value={Difficulty.HARD} control={<Radio />} label='Hard' />
              </RadioGroup>
          </FormControl>

          <FormControl>
              <FormLabel>Filter by time</FormLabel>
              <RadioGroup value={apiFilter.temporal} onChange={onChangeFilterTemporal}>
                  <FormControlLabel value='day' control={<Radio />} label='Day' />
                  <FormControlLabel value='month' control={<Radio />} label='Month' />
                  <FormControlLabel value='year' control={<Radio />} label='Year' />
              </RadioGroup>
          </FormControl>

          <PieChart
            style={{
              height: '200px'
            }}
            label={({ dataEntry }) => Math.round(dataEntry.percentage) + '%'}
            radius={42}
            labelPosition={112}
            data={[
              { title: 'Success', value: stat, color: 'green' },
              { title: 'Fail', value: (100.0 - stat), color: 'red' }
            ]}
          />
    </div>
  )
}
