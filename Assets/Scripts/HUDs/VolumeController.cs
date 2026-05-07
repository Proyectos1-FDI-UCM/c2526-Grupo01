//---------------------------------------------------------
// Controlador de volumen de la música
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Este script controla el volumen de la música del juego
/// mediante un Slider en la interfaz del menú de pausa.
/// El valor del Slider se envía al GameManager para actualizar
/// el volumen general de la música en tiempo real, 
/// comenzando con un volumen por defecto definido mediante una constante.
/// </summary>
public class VolumeController : MonoBehaviour
{
    //el slider de la música
    private Slider musicSlider;

    //constante de volumen por defecto
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
            //actualizo el volumen de la música desde el GameManager
            GameManager.Instance.setMusicVolume(musicSlider.value);
            actualVolume = musicSlider.value;
        }
    }


} 
