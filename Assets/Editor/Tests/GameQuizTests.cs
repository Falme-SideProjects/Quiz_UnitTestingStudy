using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameQuizTests
    {
		GameQuiz gameQuiz;

		[SetUp]
		public void Setup()
		{
			gameQuiz = new GameObject().AddComponent<GameQuiz>();

		}

		#region GetLocalQuestions

		[Test]
        public void GetLocalQuestions_ExistsFromStart_NotNull()
        {
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Assert.NotNull(_questions);
		}

		[Test]
		public void GetLocalQuestions_FirstFieldOfArray_NotNull()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Assert.NotNull(_questions[0]);
		}

		[Test]
		public void GetLocalQuestions_LengthMoreThan3_True()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Assert.Greater(_questions.Length, 3);
		}

		#endregion //GetLocalQuestions

		#region GetQuestion

		[Test]
		public void GetQuestion_CallItOneTime_ReturnFirstQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Spiders have 8 legs", _question.question);
		}

		[Test]
		public void GetQuestion_CallItTwoTimes_ReturnSecondQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question;

			_question = gameQuiz.GetQuestion(_questions);
			_question = gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Spiders have 4 legs", _question.question);
		}

		[Test]
		public void GetQuestion_CallItThreeTimes_ReturnThirdQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for(int a=0; a<3; a++)
				_question = gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Dogs can bark", _question.question);
		}


		[Test]
		public void GetQuestion_CallItSixTimes_ReturnFirstQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for (int a = 0; a < 5; a++)
				_question = gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Spiders have 8 legs", _question.question);
		}

		#endregion //GetQuestion

		#region GetCurrentQuestion

		[Test]
		public void GetCurrentQuestion_CallQuestionOneTime_CurrentFirstQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			//for (int a = 0; a < 5; a++)
			gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Spiders have 8 legs", gameQuiz.GetCurrentQuestion().question);
		}

		[Test]
		public void GetCurrentQuestion_CallQuestionTwoTimes_CurrentSecondQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for (int a = 0; a < 2; a++)
				gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Spiders have 4 legs", gameQuiz.GetCurrentQuestion().question);
		}

		[Test]
		public void GetCurrentQuestion_CallQuestionFiveTimes_CurrentFirstQuestion()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for (int a = 0; a < 5; a++)
				gameQuiz.GetQuestion(_questions);

			Assert.AreEqual("Spiders have 8 legs", gameQuiz.GetCurrentQuestion().question);
		}

		#endregion //GetCurrentQuestion

		#region VerifyIfIsCorrect

		[Test]
		public void VerifyIfIsCorrect_AnswerQuestionOneTrue_True()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;
			
			gameQuiz.GetQuestion(_questions);

			Assert.AreEqual(true, gameQuiz.VerifyIfIsCorrect(true));
		}

		[Test]
		public void VerifyIfIsCorrect_AnswerQuestionTwoTrue_False()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for (int a = 0; a < 2; a++)
				gameQuiz.GetQuestion(_questions);

			Assert.AreEqual(false, gameQuiz.VerifyIfIsCorrect(true));
		}

		[Test]
		public void VerifyIfIsCorrect_AnswerQuestionThreeFalse_False()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for (int a = 0; a < 3; a++)
				gameQuiz.GetQuestion(_questions);

			Assert.AreEqual(false, gameQuiz.VerifyIfIsCorrect(false));
		}

		[Test]
		public void VerifyIfIsCorrect_AnswerQuestionFourFalse_True()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			Question _question = null;

			for (int a = 0; a < 4; a++)
				gameQuiz.GetQuestion(_questions);

			Assert.AreEqual(true, gameQuiz.VerifyIfIsCorrect(false));
		}

		#endregion //VerifyIfIsCorrect

		#region CanAnswer

		[Test]
		public void CanAnswer_OnStartOfGame_True()
		{
			Assert.AreEqual(true, gameQuiz.CanAnswer());
		}

		[Test]
		public void CanAnswer_OnAnswerQuestion_False()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			gameQuiz.GetQuestion(_questions);
			gameQuiz.VerifyIfIsCorrect(false);

			Assert.AreEqual(false, gameQuiz.CanAnswer());
		}

		[Test]
		public void CanAnswer_OnAnswerQuestionAndgenerateNewOne_True()
		{
			Question[] _questions = gameQuiz.GetLocalQuestions();

			gameQuiz.GetQuestion(_questions);
			gameQuiz.VerifyIfIsCorrect(false);
			gameQuiz.GetQuestion(_questions);

			Assert.AreEqual(true, gameQuiz.CanAnswer());
		}

		#endregion //CanAnswer

	}
}
