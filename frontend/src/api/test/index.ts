import getBaseUrl from '../helpers/getBaseUrl'
import { TestQueryFilter } from '../../models/queryFilters/'
import { PersistenceService } from '../../services/persistenceService'

const baseURL = getBaseUrl()
const token = new PersistenceService().get('user')?.token
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

const getTests = (filter : TestQueryFilter) => {
    const params = testQueryFilterToUrlSearchParams(filter)

    return fetch(`${baseEndpoint}?${params.toString()}`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
}

export {
    getTests
}