using UnityEngine;
using DG.Tweening;

public class Mirror : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private float attackPos;
    [SerializeField]
    private float transitionDuration;
    [SerializeField]
    private float returnDuration;

    private float defaultPos;

    public void NormalPos()
    {
        if(transform.position.x == defaultPos) return;
        transform.DOMoveX(defaultPos, transitionDuration).SetEase(Ease.OutCubic);
    }

    public void AttackPos()
    {
        SeManager.instance.PlaySe("mirror");
        transform.DOMoveX(defaultPos+attackPos, transitionDuration).SetEase(Ease.OutCubic);
    }

    public void LaserOn()
    {
        SeManager.instance.PlaySe("laser");
        laser.SetActive(true);
        Utils.instance.WaitAndInvoke(0.5f,() => laser.SetActive(false));
    }

    private void Awake()
    {
        defaultPos = this.transform.position.x;
        laser.SetActive(false);
    }
}
