using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private CanvasGroup playerActions;
    [SerializeField]
    private CanvasGroup levlUpOptions;
    [SerializeField]
    private CanvasGroup shopOptions;
    [SerializeField]
    private CanvasGroup pauseMenu;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private TextMeshProUGUI expText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI attackText;
    [SerializeField]
    private TextMeshProUGUI stageText;


    public int remainingLevelUps = 0;
    private Player player => GameManager.instance.player;

    public void EnablePlayerActions(bool e)
    {
        if (e)
        {
            playerActions.interactable = true;
        }
        else
        {
            playerActions.interactable = false;
        }
    }

    public void EnableLevelUpOptions(bool e)
    {
        if (e)
        {
            levlUpOptions.alpha = 1;
            levlUpOptions.interactable = true;
            levlUpOptions.blocksRaycasts = true;
        }
        else
        {
            levlUpOptions.alpha = 0;
            levlUpOptions.interactable = false;
            levlUpOptions.blocksRaycasts = false;
        }
    }

    public void EnableShopOptions(bool e)
    {
        if (e)
        {
            shopOptions.alpha = 1;
            shopOptions.interactable = true;
            shopOptions.blocksRaycasts = true;
        }
        else
        {
            shopOptions.alpha = 0;
            shopOptions.interactable = false;
            shopOptions.blocksRaycasts = false;
        }
    }

    public void EnablePauseMenu(bool e)
    {
        if (e)
        {
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
        }
        else
        {
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;
            pauseMenu.blocksRaycasts = false;
        }
    }

    public void UpdateCoinText(int amount)
    {
        coinText.text = "Coin: " + amount.ToString();
    }

    public void UpdateExpText(int now, int max)
    {
        expText.text = "EXP: " + now + "/" + max;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "Level: " + level;
    }

    public void UpdateAttackText(float attack)
    {
        attackText.text = "Attack: " + attack.ToString("F1");
    }

    public void UpdateStageText(int stage)
    {
        stageText.text = "Stage: " + stage;
    }

    public void OnClickAttack()
    {
        SeManager.instance.PlaySe("button");
        StartCoroutine(WaitAndInvoke(0.5f, () => player.Attack(GameManager.instance.enemyContainer.GetAllEnemies())));
    }
    public void OnClickSave()
    {
        SeManager.instance.PlaySe("button");
        StartCoroutine(WaitAndInvoke(0.5f, () => player.EnableSave(true)));
    }
    public void OnClickEscape()
    {
        SeManager.instance.PlaySe("button");
        StartCoroutine(WaitAndInvoke(0.5f, () => GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack)));
    }

    public void OnClickShopExit()
    {
        SeManager.instance.PlaySe("button");
        GameManager.instance.ChangeState(GameManager.GameState.StageMoving);
    }

    public void OnClickGrowAttack()
    {
        SeManager.instance.PlaySe("button");
        player.GrowAttack();
        if (--remainingLevelUps <= 0)
            GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack);
    }

    public void OnClickGrowSave()
    {
        SeManager.instance.PlaySe("button");
        player.GrowSave();
        if (--remainingLevelUps <= 0)
            GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack);
    }

    public void OnClickGrowHp()
    {
        SeManager.instance.PlaySe("button");
        player.GrowHp();
        if (--remainingLevelUps <= 0)
            GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack);
    }

    public void OnClickPause()
    {
        SeManager.instance.PlaySe("button");
        EnablePauseMenu(true);
    }

    public void OnClickResume()
    {
        SeManager.instance.PlaySe("button");
        EnablePauseMenu(false);
    }

    public void OnClickTitle()
    {
        SeManager.instance.PlaySe("button");
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene("TitleScene"));
    }

    private IEnumerator WaitAndInvoke(float time, System.Action action)
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(time);
        action();
    }

    void Awake()
    {
        EnablePlayerActions(false);
        EnableLevelUpOptions(false);
        EnableShopOptions(false);
        EnablePauseMenu(false);
    }

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.DOFade(0, 1f);
    }

    void Update()
    {

    }
}
