using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float trajectoryVariance = 30.0f;
    public float spawnRate = 2.0f;
    public int spawnAmount = 2;
    public float spawnDistance = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance); 
            Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, spawnRotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(spawnRotation * -spawnDirection);
        }
    }
}
