using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPannel;
    [SerializeField] private GameObject startMoveText;
    [SerializeField] private GameObject startPannel;
    [SerializeField] private int mainMenuSceneIndex;
    [SerializeField] private PlayerController playerController;
    private Animator animator;

    private bool isPauseMenuActive;
    private bool isVolumeMenuAct;
    private bool isLocked;
    private bool canPauseMenu;

    void Start()
    {
        animator = pauseMenuPannel.GetComponent<Animator>();
        if (animator != null)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canPauseMenu)
        {
            if(!isPauseMenuActive )
            {
                isPauseMenuActive = true;
                pauseMenuPannel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isPauseMenuActive = false;
                animator.SetBool("V_Menu", false);
                pauseMenuPannel.SetActive(false);
                Time.timeScale = 1.0f;
            }
            SetCursorState();
        }
    }

    public void VolumeConButton()
    {
        if (!isVolumeMenuAct)
        {
            isVolumeMenuAct = true;
            animator.SetBool("V_Menu", true);
        }
        else
        {
            isVolumeMenuAct = false;
            animator.SetBool("V_Menu", false);
        }
    }

    public void ContinueB()
    {
        isPauseMenuActive = false;
        animator.SetBool("V_Menu", false);
        pauseMenuPannel.SetActive(false);
        Time.timeScale = 1.0f;
        SetCursorState();
    }

    public void RetryB()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuB()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    public void StartB()
    {
        if (PlayerPrefs.GetInt("SaveFirstLevel") > 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            playerController.CanMove();
            startPannel.SetActive(false);
            startMoveText.SetActive(true);
            Time.timeScale = 1.0f;
            canPauseMenu = true;
            SetCursorState();
        }
    }

    public void CanPauseMenu(bool newState)
    {
        canPauseMenu = newState;
    }

    public void SetCursorState()
    {
        if (!isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false;                  // Hide the cursor
            isLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;                   // Show the cursor
            isLocked = false;
        }
    }
}
