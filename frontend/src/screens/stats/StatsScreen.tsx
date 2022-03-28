import { Typography } from '@mui/material'
import { BestStreak } from '../../components/stats/BestStreak'
import { CurrentStreak } from '../../components/stats/CurrentStreak'
import { NewWordsLearnt } from '../../components/stats/NewWordsLearnt'
import { PercentLearnt } from '../../components/stats/PercentLearnt'
import { SuccessRate } from '../../components/stats/SuccessRate'
import { UseOfTheApp } from '../../components/stats/UseOfTheApp'

export const StatsScreen = () => {
  return (
    <div>
      <Typography variant='h1' component='h1'>Stats</Typography>

      <PercentLearnt />
      <CurrentStreak />
      <BestStreak />
      <UseOfTheApp />
      <NewWordsLearnt />
      <SuccessRate />
    </div>
  )
}
