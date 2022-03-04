import { createSlice } from '@reduxjs/toolkit'
import { Test } from '../../models/test'
import * as TestActions from './actions'

interface Filter {
  pageSize: number,
  pageNumber: number
}

export interface TestState {
  filters: {
    recent: Filter
  },
  currentTest: Test | null
}

export const initialState: TestState = {
  filters: {
    recent: {
      pageSize: 10,
      pageNumber: 0
    }
  },
  currentTest: null
}

export const testSlice = createSlice({
  name: 'test',
  initialState,
  reducers: {
    setRecentPageSize: (state, action) => {
      state.filters.recent.pageSize = action.payload
    },
    setRecentPageNumber: (state, action) => {
      state.filters.recent.pageNumber = action.payload
    },
    setRecentFilter: (state, action) => {
      state.filters.recent = action.payload
    }
  },
  extraReducers: (builder) => {      
  }
})

export const {
  setRecentPageSize,
  setRecentPageNumber,
  setRecentFilter
} = testSlice.actions
export default testSlice.reducer