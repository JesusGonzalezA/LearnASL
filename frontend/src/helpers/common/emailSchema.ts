import * as Yup from 'yup'

export const emailSchema = Yup.string()
                                .email()
                                .required()