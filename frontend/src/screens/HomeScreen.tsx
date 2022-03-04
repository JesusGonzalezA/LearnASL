import { useCallback, useEffect, useState } from 'react'
import { Typography, TablePagination } from '@mui/material'
import { Test } from '../models/test'
import { useAppSelector, useAppDispatch } from '../redux/hooks'
import * as TestApi from '../api/test'
import { CardTest } from '../components/test/CardTest'
import { setRecentFilter, setRecentPageNumber } from '../redux/test/testSlice'

interface Metadata {
  TotalCount: number
}

export const HomeScreen = () => {
  const [totalTests, setTotalTests] = useState<number>(10)
  const { id } = useAppSelector(state => state.auth.user)
  const { filters } = useAppSelector(state => state.test) 
  const dispatch = useAppDispatch()
  const [recentTests, setRecentTests] = useState<Test[]>([])

  const getTests = useCallback(async () => {
    const response = await TestApi.getTests({
      pageSize: filters.recent.pageSize, 
      pageNumber: filters.recent.pageNumber + 1,
      userId: id ?? ''
    })
    if (!response.ok) return 

    const body = await response.json() as Test[]
    const pagination = JSON.parse(response.headers.get('X-Pagination') ?? '') as Metadata
    
    setTotalTests(pagination.TotalCount)
    setRecentTests(body)
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
    getTests()
  }, [filters, getTests])

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
