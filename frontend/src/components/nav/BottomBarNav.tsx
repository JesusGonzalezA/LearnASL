import { BottomNavigation, BottomNavigationAction } from '@mui/material'
import { Link } from 'react-router-dom'
import ShowChartIcon from '@mui/icons-material/ShowChart'
import HomeIcon from '@mui/icons-material/Home'
import AddCircleIcon from '@mui/icons-material/AddCircle'
import PersonIcon from '@mui/icons-material/Person'

interface BottomBarNavProps {
    onChange?: (event: React.SyntheticEvent, value: any) => void,
    value?: any
}
export const BottomBarNav = ({value, onChange}: BottomBarNavProps) => {
  return (
    <BottomNavigation value={value} onChange={onChange} showLabels>
        <BottomNavigationAction 
            component={Link}
            to='/'
            value='/'
            label='Home' 
            icon={<HomeIcon />} 
        />
        <BottomNavigationAction 
            label='New test'
            component={Link}
            to='/test/new'
            value='/test/new'
            icon={<AddCircleIcon />} 
        />
        <BottomNavigationAction 
            label='Stats' 
            component={Link}
            to='/stats'
            value='/stats'
            icon={<ShowChartIcon />} 
        />
        <BottomNavigationAction 
            label='Profile' 
            component={Link}
            to='/profile'
            value='/profile'
            icon={<PersonIcon />} 
        />
    </BottomNavigation>
  )
}
