using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{

    public Image BlackCover;
    public Image BloodBlur;
    public Slider HPSlider;
    public Text GameOver;

    // Use this for initialization
    void Start()
    {
        BlackCover.color = Color.black;
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, new Color(0, 0, 0, 0), 1f);
        GameOver.enabled = false;
    }

    Tweener tweenAnimation;

    public void PlayHitAnimation()
    {
        if (tweenAnimation != null)
            tweenAnimation.Kill();

        BloodBlur.color = Color.black;
        tweenAnimation = DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, new Color(0, 0, 0, 0), 0.5f);

    }

    public void PlayerDiedAnimation()
    {
        BloodBlur.color = Color.black;
        GameOver.enabled = true;
        Time.timeScale = 0.0f;
        
        //DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, new Color(0, 0, 0, 1), 1f).SetDelay(3);
    }

    public void SetHP(float hp)
    {
        HPSlider.value = hp / 100.0f;
    }

}

