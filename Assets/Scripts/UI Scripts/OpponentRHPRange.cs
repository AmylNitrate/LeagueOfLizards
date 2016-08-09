using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpponentRHPRange : MonoBehaviour {

    public static OpponentRHPRange instance;

    [SerializeField]
    GameObject leftBracket, rightBracket, sliderIndicator, ownLeftBracket, ownRightBracket, ownSliderIndicator;

    [SerializeField]
    Slider slider, ownSlider;

    void Awake()
    {
        instance = this;
    }

    public void SetPositions(int min, int max)
    {
        leftBracket.SetActive(true);
        rightBracket.SetActive(true);
        slider.value = min;
        leftBracket.transform.localPosition = sliderIndicator.transform.localPosition;
        slider.value = max;
        rightBracket.transform.localPosition = sliderIndicator.transform.localPosition;
    }

    public void SetOwnPosition(int value)
    {
        ownLeftBracket.SetActive(true);
        ownRightBracket.SetActive(true);
        ownSlider.value = value - 5;
        ownLeftBracket.transform.localPosition = ownSliderIndicator.transform.localPosition;
        ownSlider.value = value + 5;
        ownRightBracket.transform.localPosition = ownSliderIndicator.transform.localPosition;
    }
}
