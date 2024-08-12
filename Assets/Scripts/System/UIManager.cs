using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup playerActions;
    [SerializeField]
    private CanvasGroup levlUpOptions;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private TextMeshProUGUI expText;
    [SerializeField]
    private TextMeshProUGUI levelText;
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

    public void UpdateStageText(int stage)
    {
        stageText.text = "Stage: " + stage;
    }

    public void OnClickAttack()
    {
        SeManager.instance.PlaySe("button");
        player.Attack(GameManager.instance.enemyContainer.GetAllEnemies());
    }
    public void OnClickSave()
    {
        SeManager.instance.PlaySe("button");
        player.Save();
    }
    public void OnClickEscape()
    {
        SeManager.instance.PlaySe("button");
        GameManager.instance.ChangeState(GameManager.GameState.EnemyAttack);
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

    void Awake()
    {
        EnablePlayerActions(false);
        EnableLevelUpOptions(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
