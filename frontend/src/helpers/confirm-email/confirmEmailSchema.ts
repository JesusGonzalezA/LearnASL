import * as Yup from 'yup'
import { emailSchema } from '../common/emailSchema'

export const confirmEmailSchema = Yup.object().shape({
    email: emailSchema,
    token: Yup.string()
})