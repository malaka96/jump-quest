using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerObejcts : MonoBehaviour
{
    [SerializeField] private UpdateUi updateUi;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private int spikeDamage = 1;
    [SerializeField] private GameObject gameOverImage;

    //to first level scene
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject jumpText;
    [SerializeField] private GameObject wallJumpText;
    [SerializeField] private GameObject portalText;
    [SerializeField] private int nextLevel;

    [SerializeField] private PauseMenu pauseMenu;

    private PlayerHealth playerHealth;
    private PlayerController playerController;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            playerHealth.TakeDamage(spikeDamage);
            AudioManager.instance.PlaySpikeSfx();

        }
        if (collision.gameObject.CompareTag("Banana"))
        {
            updateUi.UpdateBananaScore(1);
            AudioManager.instance.PlayBananaSfx();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            respawnPoint.transform.position = collision.gameObject.transform.position;
        }
        if (collision.gameObject.CompareTag("JTigger"))
        {
            playerController.CanJump(true);
            startText.SetActive(false);
            jumpText.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("WTigger"))
        {
            jumpText.SetActive(false);
            wallJumpText.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PTigger"))
        {
            wallJumpText.SetActive(false);
            portalText.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Portal"))
        {
            if (gameOverImage == null)
            {
                SceneManager.LoadScene(nextLevel);
                PlayerPrefs.SetInt("SaveFirstLevel", 1);
            }
            else
            {
                gameOverImage.SetActive(true);
                pauseMenu.CanPauseMenu(false);
                AudioManager.instance.PlayVictorySfx();
                StartCoroutine(ReturnToMainMenu());
                Time.timeScale = 0;
            }
        }

    }//ontriggerenter

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSecondsRealtime(5.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
