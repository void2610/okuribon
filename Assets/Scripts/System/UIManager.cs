using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider seSlider;
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
    private CanvasGroup gameOver;
    [SerializeField]
    private CanvasGroup clear;
    [SerializeField]
    private CanvasGroup tutorial;
    [SerializeField]
    private CanvasGroup story;
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
            foreach (Transform child in playerActions.transform)
            {
                child.GetComponent<TweenButton>()?.CheckMouseAndTween();
            }
        }
        else
        {
            playerActions.interactable = false;
            foreach (Transform child in playerActions.transform)
            {
                child.GetComponent<TweenButton>()?.ResetScale();
            }
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

    public void EnableGameOver(bool e)
    {
        if (e)
        {
            gameOver.alpha = 1;
            gameOver.interactable = true;
            gameOver.blocksRaycasts = true;
        }
        else
        {
            gameOver.alpha = 0;
            gameOver.interactable = false;
            gameOver.blocksRaycasts = false;
        }
    }

    public void EnableClear(bool e)
    {
        if (e)
        {
            clear.alpha = 1;
            clear.interactable = true;
            clear.blocksRaycasts = true;
        }
        else
        {
            clear.alpha = 0;
            clear.interactable = false;
            clear.blocksRaycasts = false;
        }
    }

    public void EnableTutorial(bool e)
    {
        if (e)
        {
            tutorial.alpha = 1;
            tutorial.interactable = true;
            tutorial.blocksRaycasts = true;
        }
        else
        {
            tutorial.alpha = 0;
            tutorial.interactable = false;
            tutorial.blocksRaycasts = false;
        }
    }

    public void EnableStory(bool e)
    {
        if (e)
        {
            story.alpha = 1;
            story.interactable = true;
            story.blocksRaycasts = true;
        }
        else
        {
            story.alpha = 0;
            story.interactable = false;
            story.blocksRaycasts = false;
        }
    }

    public void UpdateCoinText(int amount)
    {
        coinText.text = "おかね: " + amount.ToString();
    }

    public void UpdateExpText(int now, int max)
    {
        expText.text = "けいけんち: " + now + "/" + max;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "レベル: " + level;
    }

    public void UpdateAttackText(float attack)
    {
        attackText.text = "こうげきりょく: " + attack.ToString("F1");
    }

    public void UpdateStageText(int stage)
    {
        int s = Mathf.Max(1, stage);
        stageText.text = "ステージ: " + s;
    }

    public void OnClickAttack()
    {
        SeManager.instance.PlaySe("button");
        GameManager.instance.uiManager.EnablePlayerActions(false);
        Utils.instance.WaitAndInvoke(0.5f, () => player.Attack(GameManager.instance.enemyContainer.GetAllEnemies()));
    }
    public void OnClickSave()
    {
        SeManager.instance.PlaySe("button");
        GameManager.instance.uiManager.EnablePlayerActions(false);
        Utils.instance.WaitAndInvoke(0.5f, () => player.EnableSave(true));
    }
    public void OnClickReturn()
    {
        SeManager.instance.PlaySe("button");
        GameManager.instance.uiManager.EnablePlayerActions(false);
        Utils.instance.WaitAndInvoke(0.5f, () => player.Return(GameManager.instance.enemyContainer.GetAllEnemies()));
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

    public void OnClickTutorial()
    {
        SeManager.instance.PlaySe("button");
        EnableTutorial(true);
    }

    public void OnClickResume()
    {
        SeManager.instance.PlaySe("button");
        EnablePauseMenu(false);
        EnableTutorial(false);
    }

    public void OnClickTitle()
    {
        SeManager.instance.PlaySe("button");
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene("TitleScene"));
    }

    public void OnClickRetry()
    {
        SeManager.instance.PlaySe("button");
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene("MainScene"));
    }

    public void OnClickNext()
    {
        SeManager.instance.PlaySe("button");
        fadeImage.DOFade(1f, 1f).OnComplete(() =>
        {
            EnableStory(false);
            fadeImage.DOFade(0, 1f);
        });
    }

    void Awake()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1.0f);
        seSlider.value = PlayerPrefs.GetFloat("SeVolume", 1.0f);

        EnablePlayerActions(false);
        EnableLevelUpOptions(false);
        EnableShopOptions(false);
        EnablePauseMenu(false);
        EnableGameOver(false);
        EnableClear(false);

        EnableTutorial(true);
        EnableStory(true);
    }

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            BgmManager.instance.BgmVolume = value;
        });

        seSlider.onValueChanged.AddListener((value) =>
        {
            SeManager.instance.SeVolume = value;
        });

        var trigger = seSlider.gameObject.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>((data) =>
        {
            SeManager.instance.PlaySe("button");
        }));
        trigger.triggers.Add(entry);


        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.DOFade(0, 2f);
    }

    void Update()
    {

    }
}
