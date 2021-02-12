using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Quiz : MonoBehaviour
{
    public GameObject quizContainer;
    public TextMeshProUGUI questionText, questionWithImage;
    Text ansA, ansB, ansC, ansD;
    QuestionDataStructure questionDataParent;
    QuestionData questionData;
    string questionKey;
    public Image questionImage;
    public GameObject loadingScreen, resultWindow, popupWindow;
    public Text scoreText, trueAndFalseCountText;

    int score, trueAns, falseAns;
    int indexQuestion, limitQuestion;
    
    // Use this for initialization
    void Start()
    {
        questionDataParent = ReadData();
        limitQuestion = questionDataParent.data.Length;
        ansA = quizContainer.transform.Find("ButtonA").Find("Desc").GetComponent<Text>();
        ansB = quizContainer.transform.Find("ButtonB").Find("Desc").GetComponent<Text>();
        ansC = quizContainer.transform.Find("ButtonC").Find("Desc").GetComponent<Text>();
        ansD = quizContainer.transform.Find("ButtonD").Find("Desc").GetComponent<Text>();

        GetQuestion(indexQuestion);
    }

    public void GetQuestion(int number)
    {
        questionText.gameObject.SetActive(false);
        questionWithImage.gameObject.SetActive(false);
        questionImage.gameObject.SetActive(false);

        if (number < limitQuestion)
        {
            questionData = questionDataParent.data[number];
            

            ansA.text = questionData.ans_a;
            ansB.text = questionData.ans_b;
            ansC.text = questionData.ans_c;
            ansD.text = questionData.ans_d;
            questionKey = questionData.key;

            if (questionData.image != "")
            {
                StartCoroutine(GettingSprite(Application.streamingAssetsPath + "/soal_img/" + questionData.image));
                questionWithImage.gameObject.SetActive(true);
                questionImage.gameObject.SetActive(true);
                questionWithImage.text = questionData.question;
            }
            else {
                questionText.gameObject.SetActive(true);
                questionText.text = questionData.question;
            }
 
        }
        else
        {
            ShowResult();
        }
    }

    QuestionDataStructure ReadData()
    {
        string path = Application.streamingAssetsPath + "/soal.json";
        string jsonString;

        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        jsonString = reader.text;
        QuestionDataStructure questionDataStructure = JsonUtility.FromJson<QuestionDataStructure>(jsonString);
        return questionDataStructure;
    }

    IEnumerator GettingSprite(string url)
    {
        WWW texture = new WWW(url);
        yield return texture;
        //Texture2D texSprite = new Texture2D(500, 500);
        //texture.LoadImageIntoTexture(texSprite);
        Sprite sprite = Sprite.Create(texture.texture, new Rect(0, 0, texture.texture.width, texture.texture.height), new Vector2());
        questionImage.sprite = sprite;
        Debug.Log("called");
    }

    public void Answer(string ans)
    {
        indexQuestion++;
        if (ans == questionKey)
        {
            score++;
            trueAns++;

        }
        else
        {
            falseAns++;
            Debug.Log("false");
        }
        GetQuestion(indexQuestion);
    }

    void ShowResult()
    {
        resultWindow.SetActive(true);
        scoreText.text = score.ToString();
        trueAndFalseCountText.text = "Jawaban Benar : " + trueAns + "\nJawaban Salah : " + falseAns;

    }

    public void ShowPopupMenu(bool show) {
        if (show)
            popupWindow.SetActive(true);
        else
            popupWindow.SetActive(false);
    }

    public void RepeatQuiz()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Quiz");
    }

    public void BackToMainMenu()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Title");
    }
}
