using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuizBrain : MonoBehaviour
{
    public static QuizBrain instance;

    public Text results;

    public enum answers { answerOne, answerTwo, answerThree, answerFour, answerFive };

    int correctAnswers;
    int incorrectAnswers;

    int question = 1;

    Dictionary<int, bool> gotCorrect = new Dictionary<int, bool>();

    void Start()
    {
        instance = this;
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswers++;
        }
        else
        {
            incorrectAnswers++;
        }
        gotCorrect.Add(question, isCorrect);
        question++;
    }

    public void ShowResults()
    {
        Debug.Log("fired");
        results.text = "\nYou got " + correctAnswers + " correct, and " + incorrectAnswers + " incorrect.";
    }
}