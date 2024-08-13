using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    internal class ItemData
    {
        public GameObject prefab;
        public float probability;
    }

    [SerializeField]
    private List<ItemData> items = new List<ItemData>();

    private List<GameObject> currentItems = new List<GameObject>();
    private int itemNum = 3;
    private float alignment = 3;

    private List<Vector3> positions = new List<Vector3>();

    public void SetItem(int count = 1)
    {
        if (count > itemNum) return;
        currentItems.Clear();
        for (int i = 0; i < count; i++)
        {
            float total = 0;
            foreach (ItemData itemData in items)
            {
                total += itemData.probability;
            }
            float randomPoint = GameManager.instance.RandomRange(0.0f, total);

            foreach (ItemData itemData in items)
            {
                if (randomPoint < itemData.probability)
                {
                    var g = Instantiate(itemData.prefab, this.transform);
                    currentItems.Add(g);
                    g.transform.position = positions[i];
                    SetOnClick(g);
                    break;
                }
                randomPoint -= itemData.probability;
            }
        }
    }

    public void ResetItem()
    {
        foreach (GameObject g in currentItems)
        {
            Destroy(g);
        }
        currentItems.Clear();
    }

    private void SetOnClick(GameObject g)
    {
        Button b = g.transform.Find("Canvas").transform.Find("Image").gameObject.AddComponent<Button>();
        if (b != null)
        {
            b.onClick.AddListener(() =>
            {
                var item = g.GetComponent<ItemBase>();
                if (item == null) return;
                if (GameManager.instance.player.gold >= item.price)
                {
                    GameManager.instance.player.AddGold(-item.price);
                    item.Use(GameManager.instance.player);
                    Destroy(g);
                    currentItems.Remove(g);
                    SeManager.instance.PlaySe("buy");
                }
                else
                {
                    SeManager.instance.PlaySe("error");
                }
            });
        }
    }

    private void Awake()
    {
        positions.Add(this.transform.position + new Vector3(-alignment, 0, 0));
        positions.Add(this.transform.position);
        positions.Add(this.transform.position + new Vector3(alignment, 0, 0));
    }
}
