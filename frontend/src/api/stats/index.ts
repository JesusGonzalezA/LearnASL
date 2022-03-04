import getBaseUrl from '../helpers/getBaseUrl'
import { 
    TestQueryFilter,
    NumberOfLearntWordsQueryFilter,
    UseOfTheAppQueryFilter,
    SuccessRateQueryFilter
} from '../../models/queryFilters'
import { PersistenceService } from '../../services/persistenceService'

const baseURL = getBaseUrl()
const token = new PersistenceService().get('user')?.token
const baseEndpoint = `${baseURL}/stats`

const getUseOfTheApp = (filter : TestQueryFilter) => {
    return fetch(`${baseEndpoint}/use-of-the-app`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

const getBestStreak = () => {
    return fetch(`${baseEndpoint}/best-streak`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

const getCurrentStreak = () => {
    return fetch(`${baseEndpoint}/current-streak`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

const getNumberOfLearntWords = (filter: NumberOfLearntWordsQueryFilter) => {
    return fetch(`${baseEndpoint}/number-of-learnt-words`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

const getPercentLearnt = () => {
    return fetch(`${baseEndpoint}/percent-learnt`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

const getSuccessRate = () => {
    return fetch(`${baseEndpoint}/success-rate`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

export {
    getUseOfTheApp,
    getBestStreak,
    getCurrentStreak,
    getNumberOfLearntWords,
    getPercentLearnt,
    getSuccessRate
}