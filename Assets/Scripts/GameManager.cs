using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public int score = 0;
    
    public GameObject lifeRemainingPrefab;
    private List<GameObject> lifeIcons = new List<GameObject>();

    private void Start()
    {
        InitializeLives();
    }

    private void InitializeLives()
    {
        for (int i = 0; i < lives; i++)
        {
            AddLifeIcon(i);
        }
    }

    private void AddLifeIcon(int index)
    {
        var lifeIcon = Instantiate(lifeRemainingPrefab, Vector3.zero, Quaternion.identity, transform);
        RectTransform rectTransform = lifeIcon.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2((index * 60 - 900), (rectTransform.anchoredPosition.y + 1000));
        lifeIcons.Add(lifeIcon);
    }

    private void RemoveLifeIcon()
    {
        if (lifeIcons.Count > 0)
        {
            GameObject iconToRemove = lifeIcons[lifeIcons.Count - 1];
            lifeIcons.RemoveAt(lifeIcons.Count - 1);
            Destroy(iconToRemove);
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        switch (asteroid.size)
        {
            case (< 1.0f): this.score += 300;
                break;
            case (< 1.5f): this.score += 200;
                break;
            case (< 2.0f): this.score += 100;
                break;
        }
    }
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        
        this.lives--;
        
        RemoveLifeIcon();

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);   
        }
    }

    private void Respawn()
    {
        this.player.gameObject.SetActive(true);
        this.player.transform.position = Vector3.zero;
    }

    private void GameOver()
    {
        foreach (GameObject icon in lifeIcons)
        {
            Destroy(icon);
        }
        lifeIcons.Clear();
        this.lives = 3;
        this.score = 0;
        InitializeLives();
        Invoke(nameof(Respawn), this.respawnTime);
    }
}
