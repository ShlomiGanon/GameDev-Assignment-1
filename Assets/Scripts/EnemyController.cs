using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerMovement pm;
    bool IsEngage = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartEngage()
    {
        IsEngage = true;
    }

    public void StopEngage()
    {
        IsEngage = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 MoveDerictions = new Vector2(0f, 0f);
        if(IsEngage)
        {
            float PlayerX = player.transform.position.x;
            float CurrentEnemyX = this.transform.position.x;
            if(PlayerX > CurrentEnemyX)//the player is right to the enemy
            {
                MoveDerictions.x = 1;
            }
            else if (PlayerX < CurrentEnemyX)//the player is left to the enemy
            {
                MoveDerictions.x = -1;
            }
            else//the player and the enemy is in the same position
            {
                MoveDerictions.x = 0;
            }
        }
        else
        {
            MoveDerictions.x = 0;
        }
        pm.HandleHorizontalMove(MoveDerictions.x);
    }
}
