import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Typography from '@mui/material/Typography'
import { CardActionArea, Chip } from '@mui/material'
import { Test } from '../../models/test'
import { difficultyToColor } from '../../helpers/difficulty'
import { testTypeToString } from '../../helpers/testType'

type CardTestProps = {
  test: Test
}

export const CardTest = ({test}: CardTestProps) => {
  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardActionArea>
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
            { testTypeToString(test.testType) }
          </Typography>
          <Chip label={ test.difficulty } color={difficultyToColor(test.difficulty)} />
          <Typography variant="body1" color="text.secondary">
            Questions: { test.questions.length }
          </Typography>
          <Typography variant="body2" color="text.secondary">
            { new Date(test.createdOn).toLocaleDateString() } { new Date(test.createdOn).toLocaleTimeString() }
          </Typography>
        </CardContent>
      </CardActionArea>
    </Card>
  )
}
