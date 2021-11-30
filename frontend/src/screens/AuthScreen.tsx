import { BaseSyntheticEvent, useState } from "react"
import { User } from "../models/User"
import { login, logout } from "../redux/auth/authSlice";
import { useAppSelector, useAppDispatch } from '../redux/hooks';


export const AuthScreen = () => {
    const currentUser = useAppSelector(state => state.auth.user)
    const dispatch = useAppDispatch()

    const [user, setUser] = useState<User>({
        username: '',
        password: ''
    })
    
    const setUsername = (e: BaseSyntheticEvent) : void => {
        const username : string = e.target.value

        setUser({
            ...user,
            username
        })
    }

    const setPassword = (e: BaseSyntheticEvent) : void => {
        const password : string = e.target.value
        setUser({
            ...user,
            password
        })
    }

    const handleLogin = (e: BaseSyntheticEvent) => {
        e.preventDefault()
        
        dispatch(login(user))
    }

    const handleRegister = (e: BaseSyntheticEvent) => {
        e.preventDefault()
        
        dispatch(login(user))
    }

    const handleLogout = () => {
        dispatch(logout())
    }

    return (
        <div>
            Current user: {JSON.stringify(currentUser)}
            <hr />

            <h1>Login</h1>
            <form onSubmit={handleLogin}>
                <label>Username:</label>
                <input value={user.username ?? ''} onChange={setUsername} />
                <br />
                <label>Password:</label>
                <input value={user.password} onChange={setPassword} />
                <br />
                <button>
                    Login
                </button>
            </form>
            <button onClick={handleLogout}>Logout</button>

            <h1>Register</h1>
            <form onSubmit={handleRegister}>
                <label>Username:</label>
                <input value={user.username ?? ''} onChange={setUsername} />
                <br />
                <label>Password:</label>
                <input value={user.password} onChange={setPassword} />
                <br />
                <button>
                    Register
                </button>
            </form>
        </div>
    )
}
