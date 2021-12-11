using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] public RectTransform panel;
    [SerializeField] public Font font;
    public List<GameObject> curr_items;
    public int selected=0;
    public GameObject selected_Prefab;
    // Start is called before the first frame update
    void Start()
    {
        Redraw();
    }
    private void Update()
    {
        curr_items = new List<GameObject>();
        for (int i = 0; i < panel.childCount; i++)
        {
            Destroy(panel.GetChild(i).gameObject);
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            var item = inventory.items[i];
            AddElement(item.sprite, item.name, item.count, selected == i ? true : false);
        }
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown((KeyCode)(48 + i)))
                selected = i-1;
            if (Input.GetKeyDown(KeyCode.Alpha0))
                selected = 0;
        }
    }
    // Update is called once per frame
    void Redraw()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            var item = inventory.items[i];
            AddElement(item.sprite, item.name,item.count,selected==i?true:false);
        }
    }

    void AddElement(Sprite sprite, string name,int count,bool selected)
    {
        var icon = new GameObject(name);
        icon.AddComponent<Image>().sprite = sprite;
        icon.transform.parent = panel;
        icon.transform.localScale = new Vector3(1, 1, 1);
        icon.transform.position = new Vector3(0, 0, 0);
        curr_items.Add(icon);
        //
        var count_text = new GameObject(name+"_count");
        count_text.AddComponent<Text>().text = count.ToString();
        count_text.GetComponent<Text>().font = font;
        count_text.transform.parent = icon.transform;
        count_text.transform.localScale = new Vector3(1, 1, 1);
        count_text.transform.position = new Vector3(icon.transform.position.x+5f, icon.transform.position.y + 5f, icon.transform.position.z);
        count_text.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
        if (selected)
        {
            Instantiate(selected_Prefab, icon.transform);
        }
    }
}
