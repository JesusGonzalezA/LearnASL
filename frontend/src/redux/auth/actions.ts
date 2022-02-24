import { createAsyncThunk } from '@reduxjs/toolkit'
import * as AuthApi from '../../api/auth'
import { LoginResponse, RegisterResponse } from '../../models/responses'
import { Login, Register, ConfirmEmail, ChangePassword } from '../../models/auth'
import { ErrorResponse } from '../../models/responses/error';
import { PersistenceService } from '../../services/persistenceService'

const loginAsync = createAsyncThunk<
    LoginResponse, 
    Login, 
    {
        rejectValue: ErrorResponse
    }>('auth/login', async(login, thunkApi) => {
        return await AuthApi.loginAsync(login.email, login.password)
            .then(async res => {
                const data = await res.json()
                if (res.status < 200 || res.status >= 300)
                {
                    return thunkApi.rejectWithValue({
                        errors: data.errors
                    } as ErrorResponse) 
                }

                const persistenceService = new PersistenceService()
                persistenceService.set('user', { email: data.email, token: data.access_Token })

                return {
                    data
                } as LoginResponse
            }) 
    }
)

const registerAsync = createAsyncThunk<
    RegisterResponse, 
    Register, 
    {
        rejectValue: ErrorResponse
    }>('auth/register', async(register, thunkApi) => {
        return await AuthApi.registerAsync(register.email, register.password)
            .then(async res => {
                const data = await res.json()
                if (res.status < 200 || res.status >= 300)
                {
                    return thunkApi.rejectWithValue({
                        errors: data.errors
                    } as ErrorResponse) 
                }

                return {
                    data
                } as RegisterResponse
            })
    }
)

const resendConfirmationEmailAsync = createAsyncThunk<
    void, 
    string, 
    {
        rejectValue: ErrorResponse
    }>('auth/resend-confirmation-email', async(email, thunkApi) => {
        return await AuthApi.startEmailConfirmationAsync(email)
            .then(async res => {
                const data = await res.json()
                if (res.status < 200 || res.status >= 300)
                {
                    return thunkApi.rejectWithValue({
                        errors: data.errors
                    } as ErrorResponse) 
                }
            })
    }
)

const confirmEmailAsync = createAsyncThunk<
    void, 
    ConfirmEmail,
    {
        rejectValue: ErrorResponse
    }>('auth/confirm-email', async(confirmEmail, thunkApi) => {
        return await AuthApi.confirmEmailAsync(confirmEmail.email, confirmEmail.token)
        .then(async res => {
            const data = await res.json()
            if (res.status < 200 || res.status >= 300)
            {
                return thunkApi.rejectWithValue({
                    errors: data.errors
                } as ErrorResponse) 
            }
        })
    }
)

const startChangePasswordAsync = createAsyncThunk<
    void,
    string,
    {
        rejectValue: ErrorResponse
    }>('auth/start-change-password', async(email, thunkApi) => {
        return await AuthApi.startPasswordRecoveryAsync(email)
        .then(async res => {
            const data = await res.json()
            if (res.status < 200 || res.status >= 300)
            {
                return thunkApi.rejectWithValue({
                    errors: data.errors
                } as ErrorResponse) 
            }
        })
    }
)

const changePasswordAsync = createAsyncThunk<
    void,
    ChangePassword,
    {
        rejectValue: ErrorResponse
    }>('auth/change-password', async(changePassword, thunkApi) => {
        return await AuthApi.recoverPasswordAsync(
            changePassword.token, 
            changePassword.email,
            changePassword.password ?? ''
        ).then(async res => {
            const data = await res.json()
            if (res.status < 200 || res.status >= 300)
            {
                return thunkApi.rejectWithValue({
                    errors: data.errors
                } as ErrorResponse) 
            }
        })
    }
)

export {
    loginAsync,
    registerAsync,
    resendConfirmationEmailAsync,
    confirmEmailAsync,
    startChangePasswordAsync,
    changePasswordAsync
}