//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class TutorialHUD : MonoBehaviour
{
    [SerializeField] private float appearTime = 0.4f;
    [SerializeField] private float waitTime = 3.0f;
    [SerializeField] private float exitTime = 0.4f;
    [SerializeField] private float hiddenY = 200f;
    [SerializeField] private float visibleY = -80f;

    private Image image;
    private RectTransform posTutorial;
    private float timer;

    private enum State {Inicio, Appear, Wait, Exit}
    private State actualState = State.Inicio;

    private void Start()
    {
        image = GetComponent<Image>();
        posTutorial = image.rectTransform;
        SetY(hiddenY);
        image.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (actualState == State.Inicio)
        {
            return;
        }

        timer += Time.deltaTime;

        if (actualState == State.Appear)
        {
            float t = Mathf.Clamp(timer / appearTime, 0f, 1f);
            float suavizado = Mathf.SmoothStep(0f, 1f, t);
            SetY(Mathf.Lerp(hiddenY, visibleY, suavizado));

            if (timer >= appearTime)
            {
                timer = 0f;
                actualState = State.Wait;
            }
        }
        else if (actualState == State.Wait)
        {
            if (timer >= waitTime)
            {
                timer = 0f;
                actualState = State.Exit;
            }
        }
        else if (actualState == State.Exit)
        {
            float t = Mathf.Clamp(timer / exitTime, 0f, 1f);
            float suavizado = t * t;
            SetY(Mathf.Lerp(visibleY, hiddenY, suavizado));

            if (timer >= exitTime)
            {
                image.gameObject.SetActive(false);
                actualState = State.Inicio;
            }
        }
    }

    public void Show(Sprite sprite)
    {

        image.sprite = sprite;
        image.gameObject.SetActive(true);
        SetY(hiddenY);

        timer = 0f;
        actualState = State.Appear;
    }

    private void SetY(float y)
    {
        Vector2 pos = posTutorial.anchoredPosition;
        pos.y = y;
        posTutorial.anchoredPosition = pos;
    }
} 