import { useAppSelector } from '../../redux/hooks'

export const ProfileScreen = () => {
  const { user } = useAppSelector(state => state.auth)

  return (
    <div>
      <div>ProfileScreen</div>
      <div>
        <p>{user.email}</p>
      </div>
    </div>
  )
}
