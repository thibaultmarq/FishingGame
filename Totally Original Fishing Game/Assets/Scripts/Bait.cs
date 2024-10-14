using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    Vector3 goal;

    private void Awake()
    {
        goal = transform.position;
        gameObject.SetActive(false);
    }
    public void MoveFromTo(Vector2 oldPos, Vector2 newPos)
    {
        transform.position = oldPos;
        goal = newPos;
    }

    private void Update()
    {

        if (gameObject.activeSelf)
        {
            float norme = Norm(goal - transform.position);
            if ( norme > 0.05)
                transform.position += (goal - transform.position) / Norm(goal - transform.position) * Time.deltaTime*5;
            else
            {
                CheckAimingScore();
                gameObject.SetActive(false);
            }
        }
    }


    private float Norm(Vector2 v)
    {
        return Mathf.Sqrt(v.x*v.x + v.y*v.y);
    }

    private void CheckAimingScore()
    {
        float Score = Norm(transform.position - GameManager.Instance.GetCurrentFishingSpotPosition());
        if (Score < 1)
        {
            Debug.Log("Excelente visée");
        }
        else if (Score < 2)
        {
            Debug.Log("Bonne visée");
        }
        else
        {
            Debug.Log("trop loin");
            GameManager.Instance.NextGameState();
        }
    }


}
