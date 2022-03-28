import { Card, CardActionArea, SxProps } from '@mui/material'
import Radio from '@mui/material/Radio'
import FormControlLabel from '@mui/material/FormControlLabel'
import { AnswerType, typeToColor, typeToRadioColor } from '../../helpers/test'

const width = '320'
const height = '240'

interface CardProps {
    variant?: 'outlined',
    sx?: SxProps
}

export const VideoAnswer = ({handleOnChange, refAnswer, label, type} : any) => {
    const isOutlined = !(type === AnswerType.DEFAULT)
    const cardColor = typeToColor(type)
    const radioColor = typeToRadioColor(type)
    const props : CardProps = (isOutlined)
    ? {
        variant: 'outlined',
        sx: {border: '2px solid', borderColor: cardColor}
    }
    : {}

    return (
        <Card {...props}>
            <CardActionArea onClick={() => { handleOnChange({}, label) }}>
                <FormControlLabel  
                    value={label} label={label}
                    control={<Radio color={radioColor}/>} 
                />
            </CardActionArea>
            <video width={width} height={height} ref={refAnswer} controls />
        </Card>
    )
}