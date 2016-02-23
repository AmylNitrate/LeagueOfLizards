using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuizQuestion : MonoBehaviour
{
    [SerializeField]
    QuizBrain.answers correctAnswer;

    [TextArea(3, 10)]
    public string question, answerOne, answerTwo, answerThree, answerFour, answerFive;
    List<string> answers = new List<string>();
    List<Text> answerTextBoxes = new List<Text>();
    
    void Awake()
    {
        PopulateAnswers();
        PopulateText();
    }

    public void CheckAnswer(QuizAnswer answer)
    {
        QuizBrain.answers thisAnswer = answer.myAnswer;
        if (thisAnswer == correctAnswer)
        {
            QuizBrain.instance.CheckAnswer(true);
        }
        else
        {
            QuizBrain.instance.CheckAnswer(false);
        }
    }

    void PopulateAnswers()
    {
        answers.Add(question);
        answers.Add(answerOne);
        answers.Add(answerTwo);
        answers.Add(answerThree);
        answers.Add(answerFour);
        answers.Add(answerFive);
    }

    void PopulateText()
    {
        GetComponent<Text>().text = question;
        answerTextBoxes.AddRange(GetComponentsInChildren<Text>());
        for (int i = 0; i < 6; i++)
        {
            answerTextBoxes[i].text = answers[i];
        }
    }
}
