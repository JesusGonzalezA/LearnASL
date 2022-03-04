import { useAppSelector, useAppDispatch } from '../../redux/hooks';
import { useState } from 'react';
import { 
    AppBar as AppBarMui, 
    Container,
    Box,
    Toolbar, 
    Typography, 
    Tooltip, 
    IconButton, 
    Avatar, 
    Menu,
    MenuItem,
    ListItemIcon,
    ListItemText
} from '@mui/material'
import LogoutIcon from '@mui/icons-material/Logout'
import { thunkLogout } from '../../redux/auth/authSlice'

export const AppBar = () => {
    const { email } = useAppSelector(state => state.auth.user)
    const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(null)
    const dispatch = useAppDispatch()

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget) 
    }

    const handleCloseUserMenu = () => {
        setAnchorElUser(null) 
    } 

    const handleLogout = () => {
        dispatch(thunkLogout())
    }

    return (
        <AppBarMui position='sticky'>
            <Container maxWidth='xl'>
                <Toolbar disableGutters>
                    <Typography
                        variant='h6'
                        noWrap
                        component='div'
                        sx={{ flexGrow: 1 }}
                    >
                        Learn ASL
                    </Typography>
                    <Box sx={{ flexGrow: 0 }}>
                        <Tooltip title='Pofile settings' onClick={handleOpenUserMenu}>
                            <IconButton sx={{ p: 0 }}>
                                <Avatar>
                                    { email.charAt(0).toLocaleUpperCase() }
                                </Avatar>
                            </IconButton>
                        </Tooltip>

                        <Menu
                            sx={{ mt: '45px' }}
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >
                            <MenuItem onClick={handleLogout}>
                                <ListItemIcon>
                                    <LogoutIcon/>
                                </ListItemIcon>
                                <ListItemText>Logout</ListItemText>
                            </MenuItem>
                        </Menu>
                    </Box>
                </Toolbar>
            </Container>
        </AppBarMui>
    )
}
