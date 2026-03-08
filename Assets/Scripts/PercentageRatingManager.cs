using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PercentageRatingManager : MonoBehaviour
{   
    public static PercentageRatingManager Instance;

    public float maxRating = 100f;
    public float deliveryPenalty = 3f;

    public Slider ratingSlider;
    public TMP_Text ratingText;


    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        ratingSlider.maxValue = 100f;
        ratingSlider.minValue = 0f;
        ratingSlider.value = maxRating;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WrongDelivery()
    {
        maxRating -= deliveryPenalty;
        maxRating = Mathf.Clamp(maxRating, 0f, 100f);

        Debug.Log("Rating: " + maxRating.ToString("0") + "%");

        UpdateUI();
    }

    public void UpdateUI()
    {   
        ratingSlider.value = maxRating;
        ratingText.text = maxRating.ToString("0") + "%";
    }
}
