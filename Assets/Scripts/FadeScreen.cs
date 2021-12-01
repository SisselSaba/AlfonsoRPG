using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] float fadeDurationInSeconds = 0.5f;
    [SerializeField] bool fadeIn = false;

    public bool fadingToScene = false;
    public int sceneNumber = 0;

    bool fadingOut = false;
    RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = gameObject.GetComponent<RawImage>();

        if (!fadeIn)
        {
            Color initColor = rawImage.color;
            initColor.a = fadeIn ? 1 : 0;
            rawImage.color = initColor;
        }
        else
            fadingOut = true;
    }

    public void FadeToScene()
    {
        Color nextColor = rawImage.color;
        nextColor.a = nextColor.a + (Time.deltaTime / fadeDurationInSeconds);
        rawImage.color = nextColor;

        if (rawImage.color.a >= 1)
            SceneManager.LoadScene(sceneNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingToScene)
        {
            FadeToScene();
        }
        else if (fadingOut)
        {
            Color nextColor = rawImage.color;
            nextColor.a = nextColor.a - (Time.deltaTime / fadeDurationInSeconds);
            rawImage.color = nextColor;

            if (rawImage.color.a <= 0)
                fadingOut = false;
        }
    }
}
