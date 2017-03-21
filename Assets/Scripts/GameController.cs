using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    public Camera cam;
    public GameObject[] balls;
    public float timeLeft;
    public Text timeTxt;
    public GameObject gameOver;
    public GameObject buttons;
    public GameObject box;
    public int level;

    private Box boxController;
    private Score scoreController;

    private float maxWidth;
    private int numBallsSpawned = 0;
    private int numRedBallsSpawned = 0;
    private bool playing;

    // Use this for initialization
    void Start()
    {
        playing = false;
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;

        boxController = box.GetComponent<Box>();
        scoreController = box.GetComponent<Score>();

        UpdateText();
        StartCoroutine(Spawn());
    }

    void FixedUpdate()
    {
        if (playing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
                timeLeft = 0;
            UpdateText();
        }
    }

    void UpdateText()
    {
        timeTxt.text = "Tiempo: " + Mathf.RoundToInt(timeLeft);
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);
        playing = true;
        boxController.SetControl(true);
        while (timeLeft > 0)
        {
            GameObject ball = balls[UnityEngine.Random.Range(0, balls.Length)];
            Vector3 spawnPosition = new Vector3(
              UnityEngine.Random.Range(-maxWidth, maxWidth),
              transform.position.y,
              0.0f);

            GameObject insBall = (GameObject)Instantiate(ball, spawnPosition, Quaternion.identity);
            if (insBall.tag == "RedBall")
                numRedBallsSpawned++;
            else
                numBallsSpawned++;
            yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 3.0f));
        }

        yield return new WaitForSeconds(2.0f);
        boxController.SetControl(false);
        Save();
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        buttons.SetActive(true);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("./Data/currentPlayerInfo.dat");
        if (level == 1)
        {
            FileStream file2 = File.Create("./Data/Level1/playerInfo" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".dat");
        }
        else if (level == 2)
        {
            FileStream file2 = File.Create("./Data/Level2/playerInfo" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".dat");
        }
        PlayerData data = new PlayerData();
        data.score = scoreController.getScore();
        data.failHits = scoreController.getWrongs();
        data.balls = numBallsSpawned;
        data.redBalls = numRedBallsSpawned;

        bf.Serialize(file, data);
        file.Close();
    }

}

[Serializable]
class PlayerData
{
    public int score;
    public int failHits;
    public int balls;
    public int redBalls;
}


