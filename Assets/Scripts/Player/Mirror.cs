using UnityEngine;
using DG.Tweening;

public class Mirror : MonoBehaviour
{
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
        transform.DOMoveX(defaultPos+attackPos, transitionDuration).SetEase(Ease.OutCubic);
    }

    private void Awake()
    {
        defaultPos = this.transform.position.x;
    }
}
