using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UpdateUi : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    [SerializeField] private GameObject startBananaText;
    [SerializeField] private GameObject bananaCanvas;
    [SerializeField] private TMP_Text bananaScoreText;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private PlayerHealth playerHealth;

    private int bananaScore = 0;

    private PauseMenu pauseMenu;
    void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
        pauseMenu.CanPauseMenu(true);
        pauseMenu.SetCursorState();

        bananaScoreText.text = "x0";
        healthSlider.value = 6;

        playerHealth.OnHealthChanged += UpdatePlayerHealth;

        StartCoroutine(StartBananaText());
    }
    
    public void UpdateBananaScore(int score)
    {
        bananaScore += score;
        if (bananaScore >= 23)
        {
            AudioManager.instance.PlayAllBananasSfx();
            portal.SetActive(true);
        }
        bananaScoreText.text = $"x{bananaScore}";
    }

    private void UpdatePlayerHealth(int damage)
    {
        healthSlider.value = damage;
    }

    private void OnDestroy()
    {
        playerHealth.OnHealthChanged -= UpdatePlayerHealth;
    }

    IEnumerator StartBananaText()
    {
        yield return new WaitForSeconds(4);
        startBananaText.SetActive(false);
        bananaCanvas.SetActive(true);
        healthSlider.gameObject.SetActive(true);
    }

}
