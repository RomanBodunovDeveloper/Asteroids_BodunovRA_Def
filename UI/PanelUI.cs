using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    public List<GameObject> childPanels { get; private set; }
    public virtual void OnEnable()
    {
        childPanels = new List<GameObject>();
        foreach (Transform child in this.transform)
        {
            childPanels.Add(child.gameObject);
        }
    }

    public virtual void OnDisable()
    {
        
    }

    public virtual void ActivateChildrenPanel()
    {
        foreach (GameObject child in childPanels)
        {
            child.SetActive(true);
        }
    }

    public virtual void DeactivateChildrenPanel()
    {
        foreach (GameObject child in childPanels)
        {
            child.SetActive(false);
        }
    }

    public Text InitChildPanelText(string childPanelName)
    {
        for (int i = 0; i < childPanels.Count; i++)
        {
            if ( childPanels[i].name == childPanelName)
            {
                return childPanels[i].GetComponent<Text>();
            }
        }
        return null;
    }
}
