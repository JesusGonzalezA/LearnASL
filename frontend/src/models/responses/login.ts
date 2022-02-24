import { BaseResponse } from './base'

interface LoginData {
    email: string,
    access_Token: string
}

export interface LoginResponse extends BaseResponse {
    data: LoginData,
    errors: undefined
}