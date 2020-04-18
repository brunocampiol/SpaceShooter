using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
    // Enemy skill - between 0 and 100
    // 0 means very dumb
    // 100 means highest skill
    private int _skill;
    private int _maxSkill;
    private System.Random _random;

    public EnemyAI()
    {
        _skill = 75;
        _maxSkill = 100;
        _random = new System.Random();
    }

    public Vector3 GetMovement(Vector3 enemey, Vector3 player, float speed)
    {
        int currentSkill = _random.Next(_maxSkill);


        if (currentSkill <= _skill )
        {
            // do actions
            return GetRealMovement(enemey, player, speed);
        }
        else
        {
            // miss action
            return Vector3.zero;
        }
    }

    private Vector3 GetRealMovement(Vector3 enemey, Vector3 player, float speed)
    {
        int enemyMoveX = GetHorizontalMovement(enemey, player);
        int enemyMoveY = GetVerticalMovement(enemey, player);

        if (IsPlayerMoving(player) && (enemyMoveX != 0F || enemyMoveY != 0F))
        {
            Vector3 movement = new Vector3(enemyMoveX, enemyMoveY, 0.0f);
            return movement * speed;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private int GetHorizontalMovement(Vector3 enemey, Vector3 player)
    {
        // Horizontal
        if (FloatDifference(enemey.x, player.x) >= GameInfoStatic.AIMovementThreshold)
        {
            if (enemey.x > player.x)
            {
                return -1;
            }
            else if (enemey.x < player.x)
            {
                return 1;
            }
        }

        return 0;
    }

    private int GetVerticalMovement(Vector3 enemey, Vector3 player)
    {
        // Horizontal
        if (FloatDifference(enemey.y, player.y) >= GameInfoStatic.AIMovementThreshold)
        {
            if (enemey.y > player.y)
            {
                return -1;
            }
            else if (enemey.y < player.y)
            {
                return 1;
            }
        }

        return 0;
    }

    private bool IsPlayerMoving(Vector3 velocity)
    {
        if (velocity.magnitude != 0F) return true;
        else return false;
    }

    private decimal DecimalDifference(decimal nr1, decimal nr2)
    {
        return Math.Abs(nr1 - nr2);
    }

    private float FloatDifference(float nr1, float nr2)
    {
        return Math.Abs(nr1 - nr2);
    }

}
