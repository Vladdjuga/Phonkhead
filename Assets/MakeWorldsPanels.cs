using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MakeWorldsPanels : MonoBehaviour
{
    public GameObject parent;
    public GameObject prefab;
    string assetsFolderPath;
    string levelFolder;
    void Start()
    {
        string assetsFolderPath = Application.dataPath;
        string levelFolder = assetsFolderPath + "/Resources/worlds/";
        string[] files = Directory.GetFiles(levelFolder);
        float y_plus = 0f;
        foreach (var item in files)
        {
            string filename = Path.GetFileName(item);
            if (Path.GetExtension(filename) == ".voxelwbf")
            {
                GameObject panel=Instantiate(prefab, parent.transform);
                panel.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = Path.GetFileNameWithoutExtension(filename);
                Vector3 panel_pos = panel.GetComponent<RectTransform>().anchoredPosition;
                panel.GetComponent<RectTransform>().localPosition = new Vector3(panel_pos.x, panel_pos.y);
                y_plus += 1f;
            }
        }
    }

    void Update()
    {
        
    }
}
