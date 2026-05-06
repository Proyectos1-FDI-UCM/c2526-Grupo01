//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class VolumeController : MonoBehaviour
{
    //el slider de la música
    private Slider musicSlider;

    //constante de volumen x defecto
    private const float VOLUME_MUSIC_DEFAULT = 0.75f;

    private float actualVolume;

    private void Start()
    {
        musicSlider = GetComponent<Slider>();
        musicSlider.value = VOLUME_MUSIC_DEFAULT;
        //incializo el volumen a 0 para que la primera vez sea distinto y entre
        actualVolume = 0;   
    }

    private void Update()
    {
        if(musicSlider.value != actualVolume)
        {
            GameManager.Instance.setMusicVolume(musicSlider.value);
            actualVolume = musicSlider.value;
        }
    }


} 
