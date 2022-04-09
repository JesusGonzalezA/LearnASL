import { Box, Button } from "@mui/material"

interface NavButtonsProps {
    editable: boolean,
    page: number,
    handleOnPageChange: Function,
    testLength?: number,
    handleStop: Function,
    handleFinish: Function
}

export const NavButtons = ({
    editable,
    page,
    handleOnPageChange,
    testLength,
    handleStop,
    handleFinish
} : NavButtonsProps) => {
  return (
    <Box sx={{ marginTop: 3, display: 'flex', width: '80%', justifyContent: 'space-between'}}>
        <Button 
            variant='outlined'
            onClick={() => { handleOnPageChange({}, page-1) }}
            disabled={(page)===1}
        >
            Previous
        </Button>

        {
            (editable && (page)===testLength) 
            ? (
                <>
                    <Button
                        variant='contained'
                        onClick={() => handleFinish()}
                    >
                        Send test
                    </Button>
                </>
            )
            : (
                <>
                    <Button 
                        variant='contained'
                        onClick={() => { handleOnPageChange({}, page+1) }}
                        disabled={(page)===testLength}
                    >
                        Next
                    </Button>

                    <Button 
                        variant='contained'
                        color='warning'
                        onClick={() => { handleStop() }}
                    >
                        Stop
                    </Button>
                </>
            )
        }
        
    </Box>
  )
}
