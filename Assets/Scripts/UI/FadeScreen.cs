using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    private void Start()
    {
        fadeImage.DOFade(0, 1f);
    }
}
