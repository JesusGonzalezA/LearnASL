import { Provider } from 'react-redux'
import { store } from './redux/store'
import { AuthScreen } from './screens/AuthScreen';

const LearnASLApp = () => {
    return (
        <Provider store={store}>
            <AuthScreen />
        </Provider>
    )
}

export default LearnASLApp