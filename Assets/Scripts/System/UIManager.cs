using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup playerActions;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
