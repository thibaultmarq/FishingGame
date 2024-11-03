using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bait : MonoBehaviour
{
    Vector3 goal;
    public int score;
    private void Awake()
    {
        goal = transform.position;
        gameObject.SetActive(false);
    }
    public void MoveFromTo(Vector2 oldPos,float throwDistance, Vector2 throwAngle)
    {
        transform.position = oldPos;
        goal = throwDistance * 10 * throwAngle/Norm(throwAngle);
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
            Debug.Log("Excellente visée");
            GameManager.Instance.StockFish(1);
            GameManager.Instance.NextGameState();
            score = 1;

        }
        else if (Score < 2)
        {
            Debug.Log("Bonne visée");
            GameManager.Instance.StockFish(2);
            GameManager.Instance.NextGameState();
            score = 2;
        }
        else
        {
            Debug.Log("trop loin");
            GameManager.Instance.GoToState(GameState.MENU);
            score = 0;
            
        }

        
        
    }


}
