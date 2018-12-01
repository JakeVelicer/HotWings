using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : MonoBehaviour
{
    public GameObject badge;

    [SerializeField]
    private Transform m_badgeContainer;
    private Transform m_popupContainer;

    [SerializeField]
    private Sprite m_noDmgDuringTutorialBadge;

    [SerializeField]
    private Sprite m_alien1Killed;
    [SerializeField]
    private Sprite m_alien2Killed;
    [SerializeField]
    private Sprite m_alien3Killed;
    [SerializeField]
    private Sprite m_alien4Killed;
    [SerializeField]
    private Sprite m_alien5Killed;


[SerializeField]
private Sprite m_badgePopup1;

private Sprite m_badgePopup2;

private Sprite m_badgePopup3;

private Sprite m_badgePopup4;

private Sprite m_badgePopup5;

    [SerializeField]
    private AchievementManager m_achievementManager;

    void OnEnable()
    {
        if (m_achievementManager.DidNotTakeDamageDuringTutorial)
        {
           GameObject badge =  CreateBadge(m_noDmgDuringTutorialBadge);
        }

        if (m_achievementManager.GetMostKilledAlienNumber() == 1)
        {
            GameObject badge = CreateBadge(m_alien1Killed);
            Button button = badge.AddComponent<Button>();
            button.onClick.AddListener(OnBadgeClicked);

        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 2)
        {
           GameObject badge = CreateBadge(m_alien2Killed);
             Button button = badge.AddComponent<Button>();
            button.onClick.AddListener(OnBadgeClicked);
        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 3)
        {
            GameObject badge = CreateBadge(m_alien3Killed);
             Button button = badge.AddComponent<Button>();
            button.onClick.AddListener(OnBadgeClicked);
        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 4)
        {
           GameObject badge = CreateBadge(m_alien4Killed);
             Button button = badge.AddComponent<Button>();
            button.onClick.AddListener(OnBadgeClicked);
        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 5)
        {
           GameObject badge = CreateBadge(m_alien5Killed);
             Button button = badge.AddComponent<Button>();
            button.onClick.AddListener(OnBadgeClicked);
        }
      
    
        
    }
private void OnBadgeClicked()
{
   
   
}
    private GameObject CreateBadge(Sprite sprite)
    {
        GameObject gameObject = new GameObject("Badge");
        gameObject.transform.SetParent(m_badgeContainer);
        gameObject.transform.localScale = Vector3.one;

        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        return gameObject;

    }
     private GameObject CreatePopup(Sprite sprite)
    {
        GameObject gameObject = new GameObject("Popupp");
        gameObject.transform.SetParent(m_popupContainer);
        gameObject.transform.localScale = Vector3.one;

        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        return gameObject;

    }
}
