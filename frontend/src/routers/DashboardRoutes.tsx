import { useRef, useState } from 'react'
import { Routes, Route, Navigate } from 'react-router-dom'
import { Box, CssBaseline, Paper } from '@mui/material'

import { 
    HomeScreen,
    NotFoundScreen,
    ProfileScreen,
    StatsScreen,
    TestScreen
} from '../screens'
import { BottomBarNav } from '../components/nav/BottomBarNav'
import { AppBar } from '../components/appbar/AppBar'

export const DashboardRoutes = () => {
    const ref = useRef<HTMLDivElement>(null)
    const pathname = window.location.pathname
    const [value, setValue] = useState(pathname)

    const handleChange = (_ : React.SyntheticEvent, newValue : any) => {
      setValue(newValue)
    }

    return (
        <>
            <Box sx={{ pb: 7 }} ref={ref}>
                <AppBar />
                
                <CssBaseline />
                <Routes>
                    <Route path='' element={<HomeScreen />} />
                    <Route path='profile' element={<ProfileScreen />} />
                    <Route path='test/:id' element={<TestScreen />} />
                    <Route path='stats' element={<StatsScreen />} />
                    <Route path='404' element={<NotFoundScreen />} />

                    <Route path='*' element={<Navigate to='404' />} />
                </Routes>

                <Paper sx={{ position: 'fixed', bottom: 0, left: 0, right: 0 }} elevation={3}>
                    <BottomBarNav value={value} onChange={handleChange}/>
                </Paper>
            </Box>
        </>
    )
}