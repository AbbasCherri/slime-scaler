using UnityEngine;

public class Gem : MonoBehaviour
{
    private bool collected = false;

    void Start()
    {
        LevelManager.Instance.RegisterGem();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            LevelManager.Instance.CollectGem();
            Destroy(gameObject);
        }
    }
}