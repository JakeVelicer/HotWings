using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : MonoBehaviour
{

    [SerializeField]
    private Transform m_badgeContainer;

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
    private AchievementManager m_achievementManager;

    void OnEnable()
    {
        if (m_achievementManager.DidNotTakeDamageDuringTutorial)
        {
            CreateBadge(m_noDmgDuringTutorialBadge);
        }

        if (m_achievementManager.GetMostKilledAlienNumber() == 1)
        {
            CreateBadge(m_alien1Killed);

        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 2)
        {
            CreateBadge(m_alien2Killed);
        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 3)
        {
            CreateBadge(m_alien3Killed);
        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 4)
        {
            CreateBadge(m_alien4Killed);
        }
        else if (m_achievementManager.GetMostKilledAlienNumber() == 5)
        {
            CreateBadge(m_alien5Killed);
        }
      

        
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
}
