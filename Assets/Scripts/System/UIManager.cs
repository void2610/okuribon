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

    public void EnablePlayerActions(bool e)
    {
        if (e)
        {
            playerActions.alpha = 1;
            playerActions.interactable = true;
            playerActions.blocksRaycasts = true;
        }
        else
        {
            playerActions.alpha = 0;
            playerActions.interactable = false;
            playerActions.blocksRaycasts = false;
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
