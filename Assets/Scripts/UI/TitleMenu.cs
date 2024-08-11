using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider seSlider;

    public void PlayButtonSe()
    {
        if (Time.time > 0.5f)
            SeManager.instance.PlaySe("button");
    }

    private void InitPlayerPrefs()
    {
        PlayerPrefs.SetFloat("BgmVolume", 1.0f);
        PlayerPrefs.SetFloat("SeVolume", 1.0f);
    }
    public void ResetSetting()
    {
        PlayerPrefs.SetFloat("BgmVolume", 1.0f);
        PlayerPrefs.SetFloat("SeVolume", 1.0f);
        bgmSlider.value = 1.0f;
        seSlider.value = 1.0f;
    }

    void Awake()
    {
        if (!PlayerPrefs.HasKey("BgmVolume")) InitPlayerPrefs();
    }

    void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1.0f);
        seSlider.value = PlayerPrefs.GetFloat("SeVolume", 1.0f);

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
    }

    void Update()
    {

    }
}
