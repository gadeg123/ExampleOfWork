using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class VoiceRecog : MonoBehaviour
{
    [SerializeField]
    private string[] m_keywords;

    [SerializeField]
    private string[] m_keywords2;

    [SerializeField]
    private string[] m_keywords3;

    private KeywordRecognizer m_recognizer;
    private KeywordRecognizer m_recognizer2;
    private KeywordRecognizer m_recognizer3;
    public Mower mower;
    public Ghost ghost;




    // Use this for initialization
    void Start ()
    {
        m_keywords = new string[1];
        m_keywords[0] = "Restart";
        m_recognizer = new KeywordRecognizer(m_keywords);
        m_recognizer.OnPhraseRecognized += OnPhraserecognized;
        m_recognizer.Start();
        m_keywords2 = new string[1];
        m_keywords2[0] = "Go";
        m_recognizer2 = new KeywordRecognizer(m_keywords2);
        m_recognizer2.OnPhraseRecognized += OnPhraserecognized;
        m_recognizer2.Start();
        m_keywords3 = new string[1];
        m_keywords3[0] = "Simulate";
        m_recognizer3 = new KeywordRecognizer(m_keywords3);
        m_recognizer3.OnPhraseRecognized += OnPhraserecognized;
        m_recognizer3.Start();
    }

    private void OnPhraserecognized(PhraseRecognizedEventArgs args)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        float newX = Random.Range(-3, 3);
        float newZ = Random.Range(-3, 3);

        if (args.text == m_keywords[0])
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            m_recognizer.Stop();
            m_recognizer.Dispose();
            Start();
        }

        if (args.text == m_keywords2[0])
        {
            
            mower.GetMowerMovementData();
            mower.GetMowerRotationData();
            mower.StartCoroutine(mower.MowerMove(mower.mowerMovement));
            ghost.GetGhostMovementData();
            ghost.GetGhostRotationData();
            ghost.StartCoroutine(ghost.GhostMove(ghost.ghostMovement));
            m_recognizer2.Stop();
            m_recognizer2.Dispose();
            Start();
        }

        if (args.text == m_keywords3[0])
        {
            mower.startsimulation();
            m_recognizer2.Stop();
            m_recognizer2.Dispose();
            Start();
        }
    }

   // private void Update()
    ///{
        //PhraseRecognizedEventArgs args = new PhraseRecognizedEventArgs();

       // if (args.text == m_keywords2[0])
        //{
        //    mower.GetMowerMovementData();
       ///     mower.GetMowerRotationData();
         //   mower.StartCoroutine(mower.MowerMove(mower.mowerMovement));
       // }
   // }

    
}
