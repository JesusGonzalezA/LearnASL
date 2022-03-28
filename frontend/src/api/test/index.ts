import getBaseUrl from '../helpers/getBaseUrl'
import { TestQueryFilter } from '../../models/queryFilters/'
import { PersistenceService } from '../../services/persistenceService'
import { TestCreate } from '../../models/test'
import { TestGet } from '../../models/test/testGet'

const baseURL = getBaseUrl()
const getToken = () => {
    return new PersistenceService().get('user')?.token
} 
const baseEndpoint = `${baseURL}/test`

const setToUrlSearchParamsIfDefined = (params: URLSearchParams, key: string, value: string | undefined) => {
    if (value === undefined) return;

    params.set(key, value)
}

const testQueryFilterToUrlSearchParams = (filter: TestQueryFilter) : URLSearchParams => {
    const params = new URLSearchParams()

    setToUrlSearchParamsIfDefined(params, 'PageNumber', filter.pageNumber.toString())
    setToUrlSearchParamsIfDefined(params, 'PageSize', filter.pageSize.toString())
    setToUrlSearchParamsIfDefined(params, 'TestType', filter.testType?.toString())
    setToUrlSearchParamsIfDefined(params, 'Difficulty', filter.difficulty?.toString())
    setToUrlSearchParamsIfDefined(params, 'PageNumber', filter.toDate?.toString())
    setToUrlSearchParamsIfDefined(params, 'PageNumber', filter.fromDate?.toString())

    return params
}

const getTests = (filter : TestQueryFilter, abortController: AbortController) => {
    const params = testQueryFilterToUrlSearchParams(filter)

    return fetch(`${baseEndpoint}?${params.toString()}`, {
        signal: abortController.signal,
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    })
}

const createTest = (test: TestCreate) => {
    return fetch(`${baseEndpoint}?NumberOfQuestions=${test.numberOfQuestions.toString()}&TestType=${test.testType}&Difficulty=${test.difficulty}`, {
        method: 'POST',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    })
}

const getTest = ({id, populated} : TestGet) => {
    return fetch(`${baseEndpoint}/${id}?populated=${populated}`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    })
}

export {
    getTest,
    getTests,
    createTest
}