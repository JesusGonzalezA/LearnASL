import { useCallback, useEffect, useState } from 'react'
import { Typography, TablePagination } from '@mui/material'
import { Test } from '../models/test'
import { useAppSelector, useAppDispatch } from '../redux/hooks'
import * as TestApi from '../api/test'
import { CardTest } from '../components/test/CardTest'
import { setRecentFilter, setRecentPageNumber, setTotalTests } from '../redux/test/testSlice'
import { setErrors } from '../redux/dashboard/dashboardSlice'

interface Metadata {
  TotalCount: number
}

export const HomeScreen = () => {
  const { filters, totalTests } = useAppSelector(state => state.test)
  const { id } = useAppSelector(state => state.auth.user)
  const dispatch = useAppDispatch()
  const [recentTests, setRecentTests] = useState<Test[]>([])

  const getTests = useCallback(async (abortController: AbortController) => {
    const response = await TestApi.getTests
    (
      {
        pageSize: filters.recent.pageSize, 
        pageNumber: filters.recent.pageNumber + 1,
        userId: id ?? '',
      },
      abortController
    )
    return response
  }, [id, filters.recent])

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    dispatch(setRecentFilter({
      pageSize: parseInt(event.target.value, 10), 
      pageNumber: 0
    }))
  }

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number,
  ) => {
    dispatch(setRecentPageNumber(newPage))
  }

  useEffect(() => {
    const abortController = new AbortController()

    const fetchAndSet = async () => {
      getTests(abortController)
        .then( async (result) => {
          if (!result.ok)
          {
            const error = (result.status === 401) 
              ? 'Your session has expired. Login again.'
              : 'Something went wrong'
            dispatch(setErrors([error]))
            return
          }

          const body = await result.json() as Test[]
          const pagination = JSON.parse(result.headers.get('X-Pagination') ?? '') as Metadata
          
          dispatch(setTotalTests(pagination.TotalCount))
          setRecentTests(body)
        })
        .catch( (err) => {
          dispatch(setErrors(['Something went wrong']))
        })
    }

    fetchAndSet()

    return () => {
      abortController.abort()
    }
  }, [dispatch, getTests])

  return (
    <div>
      <Typography variant='h1' component='h1'>Home</Typography>
      <Typography variant='h2' component='h2'>Recent quizs</Typography>

      {
        recentTests.map(t => (
          <CardTest test={t} key={t.id} />
        ))
      }

      <TablePagination
        component="div"
        count={totalTests}
        page={filters.recent.pageNumber}
        onPageChange={handleChangePage}
        rowsPerPage={filters.recent.pageSize}
        onRowsPerPageChange={handleChangeRowsPerPage}
        rowsPerPageOptions={[5, 10]}
      />

    </div>
  )
}
