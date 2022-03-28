import { 
    TestType,
    QuestionQA, 
    Test, 
    QuestionOptionVideoToWord,
    QuestionOptionWordToVideo,
    QuestionMimic
} from '../../models/test'

export const getNumberOfCorrectAnswersFromTest = (test? : Test) => {
    if (!test) return 0
    
    switch(test.testType){
        case TestType.Mimic || TestType.Mimic_Error:
            return (test.questions as QuestionMimic[]).filter(q => q.isCorrect).length
        case TestType.QA || TestType.QA_Error:
            return (test.questions as QuestionQA[]).filter(q => q.isCorrect).length
        case TestType.OptionVideoToWord || TestType.OptionVideoToWord_Error:
            return (test.questions as QuestionOptionVideoToWord[]).filter(q => q.correctAnswer === q.userAnswer).length
        case TestType.OptionWordToVideo || TestType.OptionWordToVideo_Error:
            return (test.questions as QuestionOptionWordToVideo[]).filter(q => q.correctAnswer === q.userAnswer).length
        default: 
            return 0
    }
}