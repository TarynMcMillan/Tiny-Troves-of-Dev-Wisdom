using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AdviceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI adviceText;
    [SerializeField] TextMeshProUGUI authorText;
    [SerializeField] GameObject splashScreen;
    [SerializeField] TextAsset adviceFile;
    Dictionary<string, List<string>> advice;
    private List<string> keyList;
   
    private int randomKey;
    private int oldKey;
    private string selectedAuthor;
    
    void Start()
    {
        advice = new Dictionary<string, List<string>>();
        PopulateDictionary();
        DisplaySplashScreen();
    }

    void DisplaySplashScreen()
    {
        splashScreen.SetActive(true);
    }

    void PopulateDictionary()
    {
        var splitFile = new string[] { "\r\n", "\r", "\n" };
        var splitLine = new char[] { '#' };
        var Lines = adviceFile.text.Split(splitFile, System.StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < Lines.Length; i++)
        {
            var line = Lines[i].Split(splitLine, System.StringSplitOptions.None);
            string author = line[0];
            string wisdom = line[1];

            if (wisdom.Length <= 99 && author.Length <= 25)
            {
                if(!advice.ContainsKey(author))
                {
                    advice[author] = new List<string>();
                }
                advice[author].Add(wisdom);
            }
            else
            {
                Debug.LogWarning($"Can't add {author}'s advice to the dictionary!");
            }
        }

        keyList = new List<string>(advice.Keys);
    }

    public void GenerateAdvice()
    {
        if(splashScreen.activeSelf)
        {
            splashScreen.SetActive(false);
        }

        SelectRandomKey();

        while (randomKey == oldKey)
        {

            SelectRandomKey();
        }

        StartCoroutine(OpenChest());
    }

    private void SelectRandomKey()
    {
        randomKey = Random.Range(0, keyList.Count);
    }

    IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(0.3f);
        selectedAuthor = keyList[randomKey];
        DisplayAuthor();
    }

    private void DisplayAuthor()
    {
        authorText.text = "- " + selectedAuthor;
        DisplayAdvice();
    }

    void DisplayAdvice()
    {
        if (advice.ContainsKey(selectedAuthor))
        {
            List<string> authorWisdoms = advice[selectedAuthor];

            if (authorWisdoms.Count > 0)
            {
                int randomIndex = Random.Range(0, authorWisdoms.Count);
                string randomWisdom = authorWisdoms[randomIndex];

                adviceText.text = randomWisdom;
            }
            else
            {
                adviceText.text = "No wisdom entries for " + selectedAuthor;
            }
        }

        oldKey = randomKey;
    }
}

