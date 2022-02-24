import { Provider } from 'react-redux'
import { store } from './redux/store'
import { AppRouter } from './routers/AppRouter'

const LearnASLApp = () => {
    return (
        <Provider store={store}>
            <AppRouter />
        </Provider>
    )
}

export default LearnASLApp