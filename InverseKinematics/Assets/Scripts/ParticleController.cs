using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    // Variables simulación
    private Rigidbody rbParticula;

    Vec3 gravedad = new Vec3(0, -9.8f, 0);
    int masaParticula = 5;
    int k = 50;
    int masaMuelle = 2;
    float longitudMuelleInicial;
    float longitudMuelleFinal = 5f;
    float fuerzaParticula;
    bool empezar = true;
    float fuerzaMuelle;

    // Variables HUD
    public Text textoTiempo;
    private float tiempo = 5f;

    // Use this for initialization
    void Start () {
        rbParticula = GetComponent<Rigidbody>();
        GameObject muelle = GameObject.Find("Muelle");
        longitudMuelleInicial = muelle.GetComponent<Collider>().bounds.size.y;
        MostrarHUD();
    }
	
	// Update is called once per frame
	void Update () {
        // Aplicamos la aceleración a la particula (en caida libre es la gravedad) F = m*a ; F = 5*9'8, lo aplicamos en todo momento
        fuerzaParticula = masaParticula * gravedad.y;
        rbParticula.AddForce(new Vec3(0, fuerzaParticula, 0), ForceMode.Force);

        // En tiro horizontal hay que calcular la aceleración utilizando las formulas MRUA

        // Reiniciar simulación cada 5 segundos
        tiempo -= Time.deltaTime;
        //if (tiempo < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        MostrarHUD();
    }

    void OnTriggerEnter (Collider other) {
        if (empezar) {
            // Cuando la particula toca la plataforma aplicar fuerza sobre el muelle
            if (other.gameObject.CompareTag("Plataforma")) {
                //Debug.Log("La particula ha colisionado con la plataforma!");
                GameObject muelle = GameObject.Find("Muelle");
                muelle.GetComponent<Rigidbody>().AddForce(new Vec3(0, fuerzaParticula, 0), ForceMode.Impulse);

                // Aplicar la fuerza que ejerce el muelle usando hooke's law a la particula f = -k (l - lo)
                float incrementoLongitud = longitudMuelleFinal - longitudMuelleInicial;
                fuerzaMuelle = -k * incrementoLongitud;
                rbParticula.AddForce(new Vec3(0, fuerzaMuelle, 0), ForceMode.Impulse);
            }
            empezar = false;
        }
    }

    void MostrarHUD() {
        textoTiempo.text = ("Reinicio simulación en: " + Mathf.Round(tiempo).ToString() + "s\n" +
                            "Gravedad: " + gravedad.y.ToString() + "\nMasa Particula: " + masaParticula.ToString() + 
                            "\nK (Rigidez Muelle): " + k.ToString() + "\nMasa Muelle: " + masaMuelle.ToString() + 
                            "\nLongitud Inicial Muelle: " + longitudMuelleInicial.ToString() + "\nLongitud Final Muelle: " + longitudMuelleFinal.ToString() +
                            "\nFuerza Particula: " + fuerzaParticula.ToString() + "\nFuerza Muelle: " + fuerzaMuelle.ToString());
    }
}
