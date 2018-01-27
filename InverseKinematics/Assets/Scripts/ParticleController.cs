using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    // Variables simulación
    public GameObject slider1, slider2, slider3, particle;
    public GameObject velDown, forceDown, velUp, forceUp, forceMuelle;
    private Rigidbody rbParticula;
    public float gravityY = -9.8f;
    Vec3 gravedad, particlePos;
    public int masaParticula = 5;
    public int k = 50;
    int masaMuelle = 2;
    float longitudMuelleInicial;
    float longitudMuelleFinal = 5f;
    float fuerzaParticula;
    bool empezar = true;
    float fuerzaMuelle;

    // Variables HUD
    public Text textoTiempo;
    private float tiempo = 3f;

    // Use this for initialization
    void Start () {
        gravedad = new Vec3(0, gravityY, 0);
        rbParticula = GetComponent<Rigidbody>();
        GameObject muelle = GameObject.Find("Muelle");
        longitudMuelleInicial = muelle.GetComponent<Collider>().bounds.size.y;
        MostrarHUD();
        particlePos = transform.position;

        // Disable up and enable down
        forceMuelle.SetActive(false);
        velUp.SetActive(false);
        forceUp.SetActive(false);
        velDown.SetActive(true);
        forceDown.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        // Aplicamos la aceleración a la particula (en caida libre es la gravedad) F = m*a ; F = 5*9'8, lo aplicamos en todo momento
        fuerzaParticula = masaParticula * gravedad.y;
        rbParticula.AddForce(new Vec3(0, fuerzaParticula, 0), ForceMode.Force);

        // Reiniciar simulación cada 5 segundos
        if (empezar) tiempo -= Time.deltaTime;
        if (empezar && tiempo < 0) {
            transform.position = particlePos;
            GetComponent<Rigidbody>().velocity = Vec3.zero;

            // Disable up and enable down
            forceMuelle.SetActive(false);
            velUp.SetActive(false);
            forceUp.SetActive(false);
            velDown.SetActive(true);
            forceDown.SetActive(true);

            empezar = false;
            tiempo = 3f;
        }

        MostrarHUD();
    }

    void OnTriggerEnter (Collider other) {
        empezar = true;
        // Cuando la particula toca la plataforma aplicar fuerza sobre el muelle
        if (other.gameObject.CompareTag("Plataforma")) {
            //Debug.Log("La particula ha colisionado con la plataforma!");
            GameObject muelle = GameObject.Find("Muelle");
            muelle.GetComponent<Rigidbody>().AddForce(new Vec3(0, fuerzaParticula, 0), ForceMode.Impulse);

            // Aplicar la fuerza que ejerce el muelle usando hooke's law a la particula f = -k (l - lo)
            float incrementoLongitud = longitudMuelleFinal - longitudMuelleInicial;
            fuerzaMuelle = -k * incrementoLongitud;
            rbParticula.AddForce(new Vec3(0, fuerzaMuelle, 0), ForceMode.Impulse);

            // Disable down and enable up
            forceMuelle.SetActive(true);
            velUp.SetActive(true);
            forceUp.SetActive(true);
            velDown.SetActive(false);
            forceDown.SetActive(false);
        }
    }

    void MostrarHUD() {
        if (fuerzaParticula < 0) fuerzaParticula *= -1;
        textoTiempo.text = ("Reinici de la simulació en: " + Mathf.Round(tiempo).ToString() + "s\n\n" +
                            "Gravetat: " + gravityY.ToString() + "\nMassa Partícula: " + masaParticula.ToString() + 
                            "\nRigidesa Molla: " + k.ToString() + "\n\nMassa Molla: " + masaMuelle.ToString() + 
                            "\nLongitud Inicial Molla: " + longitudMuelleInicial.ToString() + "\nLongitud Final Molla: " + longitudMuelleFinal.ToString() +
                            "\n\nForça Partícula: " + fuerzaParticula.ToString() + "\nForça Molla: " + fuerzaMuelle.ToString());
    }

    public void getGravity() {
        gravityY = slider1.GetComponent<Slider>().value;
    }

    public void getMasaParticula() {
        masaParticula = Mathf.RoundToInt(slider2.GetComponent<Slider>().value);
    }

    public void getRigidesaMolla() {
        k = Mathf.RoundToInt(slider3.GetComponent<Slider>().value);
    }
}
