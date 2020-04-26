using UnityEngine;

public static class GameInfoStatic
{
    public const float PlayerFireRate = 0.4F;
    public const float PlayerNextFire = 0.2F;
    public const float PlayerSpeed = 15;
    public const float PlayerTilt = 1.2F;
    public const float PlayableAreaBoundryX = 13F;
    public const float PlayableAreaBoundryY = 4.7F;

    public const int DefaultLevelTime = 60;
    public const int DefaultPlayerLives = 3;
    public const int DefaultPlayerScore = 0;
    public const int DefaultShotSpeed = 25;

    public static Vector3 DefaultPlayerPosition = new Vector3(0, 0, 0);
    public static Vector3 DefaultEnemyPosition = new Vector3(0, 0, 50);
    public static Vector3 DefaultShotRotation = new Vector3(90, 0, 0);

    public const int KillScore = 10;

    public const float EnemryMovement = 1F;
    public const float AIMovementThreshold = 0.3F;

    public const string ButtonStart = "Start";
    public const string ButtonOptions = "Options";
    public const string ButtonBack = "Back";

    public const string GameName = "Space Shooter";
    public const string OptionsTitle = "Options";

    public const string TagPlayer = "Player";
    public const string TagEnemy = "Enemy";

    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";


    public const string StartCenterText = "Are you ready?";
    public const string GoCenterText = "GO!";

    public const string WinCenterText = "You win";
    public const string DiedCenterText = "You died";
}
