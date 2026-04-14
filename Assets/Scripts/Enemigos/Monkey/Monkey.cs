

using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;


public class Monkey : MonoBehaviour
{
    [SerializeField]
    private float DescansoAtaques = 6f; //TIEMPO ENTRE ATAQUES

    [SerializeField]
    private float DescansoProyectiles = 1f; //TIEMPO ENTRE PROYECTILES

    [SerializeField]
    private GameObject ProyectilMono;

    [SerializeField]
    private int NumeroProyectiles = 3;

    [SerializeField]
    private float AlturaSalto = 10f;
    [SerializeField]
    private float VelocidadSalto = 7f;

    private bool isAttacking = false;
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<PlayerMovement>() != null && !isAttacking)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackLoop());
            }
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            playerInRange = false;
        }
    }

    private IEnumerator AttackLoop()
    {
        isAttacking = true;

        yield return new WaitForSeconds(DescansoAtaques/2);

        while (playerInRange)
        {
            int i = Random.Range(1, 4);
            if (i == 3) yield return StartCoroutine(AttackJump());
            else yield return StartCoroutine(AttackNormal());

            yield return new WaitForSeconds(DescansoAtaques);
        }

        isAttacking = false;
    }

    private IEnumerator AttackNormal()
    {
        for (int i = 0; i < NumeroProyectiles; i++)
        {
            SummonWave();
            yield return new WaitForSeconds(DescansoProyectiles);
        }
    }

    private IEnumerator AttackJump()
    {
        float t = 0;

        Vector3 start = transform.position;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, start + Vector3.up * AlturaSalto, t);
            t += Time.deltaTime * VelocidadSalto;
            yield return null;
        }

        yield return StartCoroutine(AttackNormal());

        t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(start + Vector3.up * AlturaSalto, start, t);
            t += Time.deltaTime * VelocidadSalto;
            yield return null;
        }
    }

    private void SummonWave()
    {
        Instantiate(ProyectilMono, transform.position, Quaternion.identity);
    }
} // class Monkey 
// namespace
