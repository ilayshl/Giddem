using UnityEngine;

/// <summary>
/// Responsible for handling the animations of the player.
/// </summary>
public class AnimationManager : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

}
