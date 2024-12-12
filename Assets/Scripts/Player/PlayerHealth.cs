using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;

    private int _health;
    public int MaxHealth { get; private set; } = 6;

    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChanged?.Invoke(_health);
            if(_health <= 0)
            {
                PlayerDie();
            }

        }
    }

    public event Action<int> OnHealthChanged;

    void Start()
    {
        Health = MaxHealth;

        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void PlayerDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (!(Health <= 0))
        {
            playerController.RespawnPlayer();
            animator.Play("SpawnAnim");
        }
    }

    public void Heal(int amount)
    {
        Health += amount;
    }
}
