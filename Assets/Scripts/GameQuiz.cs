using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameQuiz : MonoBehaviour
{

	const string wrong = "WRONG";
	const string correct = "CORRECT";

	[SerializeField]
	private TextMeshProUGUI questionText;
	[SerializeField]
	private TextMeshProUGUI answerText;

	private Question[] questions;

	private Question currentQuestion;
	private int indexQuestion;
	private bool canAnswer = true;
	
	public void Awake()
	{
		this.questions = GetLocalQuestions();
	}

	public void Start()
	{
		SetResolution();
		WriteQuestion();
	}
	
	private void SetResolution()
	{
		Screen.SetResolution((int)(Screen.height* 0.5625f), Screen.height, true);
	}

	public Question[] GetLocalQuestions()
	{
		Question[] _questions =
		{
			new Question{ question = "Spiders have 8 legs", answer = true },
			new Question{ question = "Spiders have 4 legs", answer = false },
			new Question{ question = "Dogs can bark", answer = true },
			new Question{ question = "Rats can bark", answer = false },
		};
		return _questions;
	}

	public void WriteQuestion()
	{
		Question _question = GetQuestion(questions);
		this.questionText.text = _question.question;
		this.answerText.text = string.Empty;
	}

	public Question GetQuestion(Question[] _questions)
	{
		Question result = _questions[indexQuestion];

		indexQuestion = (indexQuestion == _questions.Length-1 ? 0: indexQuestion+1);

		currentQuestion = result;

		this.canAnswer = true;

		return result;
	}

	public Question GetCurrentQuestion()
	{
		return this.currentQuestion;
	}

	public void AnswerTrue()
	{
		if(CanAnswer())
		{
			WriteAnswer(VerifyIfIsCorrect(true));
			StartCoroutine(DelayForNextQuestion());
		}
	}

	public void AnswerFalse()
	{
		if (CanAnswer())
		{
			WriteAnswer(VerifyIfIsCorrect(false));
			StartCoroutine(DelayForNextQuestion());
		}
	}

	public void WriteAnswer(bool value)
	{
		this.answerText.text = (value ? correct : wrong);
	}

	public bool VerifyIfIsCorrect(bool value)
	{
		this.canAnswer = false;
		return value == this.GetCurrentQuestion().answer;
	}

	public bool CanAnswer()
	{
		return this.canAnswer;
	}

	private IEnumerator DelayForNextQuestion()
	{
		yield return new WaitForSeconds(2);
		this.WriteQuestion();
	}
}
