import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { User } from '../../models/User'

export interface AuthState {
  user: User
}

const invalidUser : User = {
    username: null,
    password: ''
}

export const initialState: AuthState = {
  user: invalidUser
}

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    login: (state, action: PayloadAction<User>) => {
        state.user = action.payload
    },
    logout: state => {
      state.user = invalidUser
    }
  }
})

export const { login, logout } = authSlice.actions
export default authSlice.reducer