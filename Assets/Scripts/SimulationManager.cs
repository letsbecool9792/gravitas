using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelSpawnModal;
    public GameObject bodyPrefab;
    public Transform bodiesParent;

    public TMP_InputField massField;
    public TMP_InputField radiusField;
    public TMP_InputField posXField, posYField, posZField;
    public TMP_InputField velXField, velYField, velZField;

    [HideInInspector] public bool simulationRunning = false;
    private List<Rigidbody> activeBodies = new List<Rigidbody>();

    void Start()
    {
        panelMainMenu.SetActive(true);
        panelSpawnModal.SetActive(false);
        Time.timeScale = 0f; 
    }

    public void OnSpawnButtonClicked()
    {
        panelMainMenu.SetActive(false);
        panelSpawnModal.SetActive(true);
    }

    public void OnConfirmSpawn()
    {
        float mass = float.Parse(massField.text);
        float radius = float.Parse(radiusField.text);
        Vector3 position = new Vector3(
            float.Parse(posXField.text),
            float.Parse(posYField.text),
            float.Parse(posZField.text)
        );
        Vector3 velocity = new Vector3(
            float.Parse(velXField.text),
            float.Parse(velYField.text),
            float.Parse(velZField.text)
        );

        // Instantiate the body
        GameObject newBody = Instantiate(bodyPrefab, position, Quaternion.identity, bodiesParent);

        // Access CelestialBody component and initialize it
        CelestialBody body = newBody.GetComponent<CelestialBody>();
        body.InitializeBody(mass, radius, velocity, Color.white); 
        // (You can add a color picker later — for now, just keep white)

        // Track its Rigidbody for later
        Rigidbody rb = newBody.GetComponent<Rigidbody>();
        activeBodies.Add(rb);

        Debug.Log($"Spawned new body: {newBody.name}, total bodies: {activeBodies.Count}");

        // Close modal and return to main menu
        panelSpawnModal.SetActive(false);
        panelMainMenu.SetActive(true);
    }


    public void OnStartSimulationClicked()
    {
        Debug.Log("Start Simulation button clicked!");
        simulationRunning = true;
        Time.timeScale = 1f;

        // Wake up all rigidbodies
        foreach (Rigidbody rb in activeBodies)
        {
            if (rb != null)
            {
                rb.WakeUp();
                rb.velocity = rb.velocity;
            }
        }

        panelMainMenu.SetActive(false);
    }
    
    public CelestialBody[] GetActiveBodies()
    {
        List<CelestialBody> list = new List<CelestialBody>();
        foreach (Rigidbody rb in activeBodies)
        {
            if (rb != null)
            {
                CelestialBody cb = rb.GetComponent<CelestialBody>();
                if (cb != null)
                    list.Add(cb);
            }
        }
        return list.ToArray();
    }
}
