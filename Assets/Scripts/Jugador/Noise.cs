//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
///Lógicakjnejd
/// </summary>
public class Noise : MonoBehaviour
{
    [SerializeField]
    private int noiseLevel = 0;


   
    
    public void takeNoise(int noiseDamage)
    {
        noiseLevel += noiseDamage;
        Debug.Log("Ruido hacido. Tienes este ruido julai: " + noiseLevel);

    }

    
    private void Start()
    {
        noiseLevel = 0;
        
    }

   

   



} 
