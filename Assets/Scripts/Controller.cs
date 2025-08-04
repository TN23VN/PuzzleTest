using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Color[] bottleColors;
    public SpriteRenderer bottleMaskSR;

    public AnimationCurve ScaleAndRotationMultiplierCurve;
    public AnimationCurve FillAmountCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateColorsOnShader();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            StartCoroutine(RotateBottle());
        }
    }

    void UpdateColorsOnShader()
    {
        bottleMaskSR.material.SetColor("_Color01", bottleColors[0]);
        bottleMaskSR.material.SetColor("_Color02", bottleColors[1]);
        bottleMaskSR.material.SetColor("_Color03", bottleColors[2]);
        bottleMaskSR.material.SetColor("_Color04", bottleColors[3]);
    }

    public float timeToRotate = 1.0f;
    IEnumerator RotateBottle()
    {
        float t = 0;
        float leftValue;
        float angelValue;

        while (t < timeToRotate)
        {
            leftValue = t / timeToRotate;
            angelValue = Mathf.Lerp(0.0f, 90.0f, leftValue);

            transform.eulerAngles = new Vector3(0,0,angelValue);
            bottleMaskSR.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angelValue));

            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        angelValue = 90.0f;
        transform.eulerAngles = new Vector3(0, 0, angelValue);
        bottleMaskSR.material.SetFloat("_SARM", ScaleAndRotationMultiplierCurve.Evaluate(angelValue));
    }
}
