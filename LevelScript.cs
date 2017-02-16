using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//buttons and other
using System;
using System.IO;
using System.Xml; //Needed for XML functionality
using System.Collections.Generic; //List

[System.Serializable]
public class MyDctnry //dictionary
{
    public string word { get; set; } //root
    public string rusW { get; set; } //node
    public string engW { get; set; } //node
}

public class LevelScript : MonoBehaviour //main class
{
    public GameObject headPanel;
    public GameObject backPanel;

    public Text ScoreText;
    public Text qText;
    public Text[] answText;

    static List<MyDctnry> words = new List<MyDctnry>();

    int score = 0;
    string crrAnsw;

    public void ListGen() //generation dictionary List by using XML tree
    {
        XmlDocument xDoc = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load("Dictionary", typeof(TextAsset));

        xDoc.LoadXml(textAsset.text); //path to XML file
        XmlElement xRoot = xDoc.DocumentElement;

        foreach (XmlElement xnode in xRoot)
        {
            MyDctnry word = new MyDctnry();
            XmlNode attr = xnode.Attributes.GetNamedItem("word");
            if (attr != null)
            {
                word.word = attr.Value;
            }

            foreach (XmlNode childnode in xnode.ChildNodes)
            {
                if (childnode.Name == "rusW")
                {
                    word.rusW = childnode.InnerText;
                }

                if (childnode.Name == "engW")
                {
                    word.engW = childnode.InnerText;
                }
            }
            words.Add(word); //adding elements in to the list
        }

        System.Random rnd = new System.Random();

      for (int i = 0; i < words.Count; i++) //list shuffling
        {
            var tmp = words[i];
            words.RemoveAt(i);
            words.Insert(rnd.Next(words.Count), tmp);
        }

    }

    public void qGen() //question generate
    {
        System.Random rnd = new System.Random();

        int elNumb = 0;
        int j = 0;

        elNumb = rnd.Next(0, words.Count - 1);

        answText[0].text = words[elNumb].engW;
        crrAnsw = words[elNumb].engW; //correct answer
        qText.text = words[elNumb].rusW; //question

        for (int i = 1; i < 3 ; i++)
        {
            elNumb = rnd.Next(i, words.Count - 1);
            answText[i].text = words[elNumb].engW;
        }

       for (int i = 0; i < 3; i++) //shuffling answers array
        {
            j = rnd.Next(0, 3);
            string temp = answText[i].text;
            answText[i].text = answText[j].text;
            answText[j].text = temp;
        }
    }

    void Start() //start list generation + generate first questions ... instant calling
    {
        ListGen();
        qGen();
    }

    public void AnswBttns(int index)
    {

        if (crrAnsw == answText[index].text.ToString())
        {
            score += 10;
            ScoreText.text = "Score: " + Convert.ToString(score);
        }     
        else if (crrAnsw != answText[index].text.ToString())
        {
            score -= 10;
            ScoreText.text = "Score: " + Convert.ToString(score);
        }
        else if (index == 3) //NEED TO FIX
        {
            ScoreText.text = "Write answer" + crrAnsw;
        }

        qGen();
    }

    void Update()
    {
        if (backPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            backPanel.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            backPanel.SetActive(false);
        }        
    }

    public void OnClickExitMenu()
    {
        backPanel.SetActive(true);
    }

    public void OnClickExitN()
    {
        backPanel.SetActive(false);
    }

    public void OnClickExitY()
    {
        SceneManager.LoadScene(0);
    }
}
