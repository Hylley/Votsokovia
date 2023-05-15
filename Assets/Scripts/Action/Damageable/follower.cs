using UnityEngine;

public interface IFollower
{
    public void Catch(PlayerMove player);
    public PlayerMove GetPlayer();
    public void Uncatch();
}