import getBaseUrl from '../helpers/getBaseUrl'

const baseURL = getBaseUrl()
const baseEndpoint = `${baseURL}/auth`

const loginAsync = async (email : string, password : string) => {
    return fetch(`${baseEndpoint}/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email, password
        })
    })
}

const registerAsync = async (email: string, password : string) => {
    return fetch(`${baseEndpoint}/register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email, password
        })
    })
}

const startPasswordRecoveryAsync = (email : string) => {
    return fetch(`${baseEndpoint}/password-recovery/start?email=${email}`, {
        method: 'PUT'
    })
}

const startEmailConfirmationAsync = (email : string) => {
    return fetch(`${baseEndpoint}/email-confirmation/start?email=${email}`, {
        method: 'PUT'
    })
}

const recoverPasswordAsync = (token : string, email : string, password : string) => {
    return fetch(`${baseEndpoint}/password-recovery?token=${token}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email, password
        })
    })
}

const confirmEmailAsync = (email : string, token : string) => {
    return fetch(`${baseEndpoint}/email-confirmation?email=${email}&token=${token}`, {
        method: 'PUT'
    })
}

export {
    loginAsync,
    registerAsync,
    startPasswordRecoveryAsync,
    startEmailConfirmationAsync,
    recoverPasswordAsync,
    confirmEmailAsync
}
