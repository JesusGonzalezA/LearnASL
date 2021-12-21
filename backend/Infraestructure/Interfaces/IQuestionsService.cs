using System;
using System.Collections.Generic;
using Core.Entities.Tests;

namespace Infraestructure.Interfaces
{
    public interface IQuestionsService
    {
        ICollection<QuestionOptionWordToVideoEntity> GenerateQuestionsOptionWordToVideoEntity(Guid testId, int numberOfQuestions, bool isErrorTest);
    }
}
