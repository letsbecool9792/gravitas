using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour
{
    public GameObject panelIntro;
    public GameObject panelMainMenu;
    public GameObject panelSpawnModal;
    public GameObject bodyPrefab;
    public Transform bodiesParent;

    public GameObject introStartButton;
    public GameObject introInformationButton;
    public GameObject informationText;
    public GameObject informationBackButton;

    public TMP_InputField massField;
    public TMP_InputField radiusField;
    public TMP_InputField posXField, posYField, posZField;
    public TMP_InputField velXField, velYField, velZField;

    public GameObject buttonStart;
    public GameObject buttonSpawn;
    public GameObject buttonPause;
    public GameObject buttonStop;
    public GameObject buttonSpeed05x;
    public GameObject buttonSpeed1x;
    public GameObject buttonSpeed2x;

    [HideInInspector] public bool simulationRunning = false;
    public bool isPaused = false;
    public float timeScale = 1f;
    
    private List<Rigidbody> activeBodies = new List<Rigidbody>();

    void Start()
    {
        // Show intro panel first
        panelIntro.SetActive(true);
        panelMainMenu.SetActive(false);
        panelSpawnModal.SetActive(false);
        
        // Intro panel: show buttons, hide info text
        introStartButton.SetActive(true);
        introInformationButton.SetActive(true);
        informationText.SetActive(false);
        informationBackButton.SetActive(false);
        
        // Initially hide sim controls
        buttonStart.SetActive(true);
        buttonSpawn.SetActive(true);
        buttonPause.SetActive(false);
        buttonStop.SetActive(false);
        buttonSpeed05x.SetActive(false);
        buttonSpeed1x.SetActive(false);
        buttonSpeed2x.SetActive(false);
        
        Time.timeScale = 0f; 
    }

    public void OnIntroStartClicked()
    {
        panelIntro.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void OnInformationButtonClicked()
    {
        introStartButton.SetActive(false);
        introInformationButton.SetActive(false);
        informationText.SetActive(true);
        informationBackButton.SetActive(true);
    }

    public void OnInformationBackClicked()
    {
        introStartButton.SetActive(true);
        introInformationButton.SetActive(true);
        informationText.SetActive(false);
        informationBackButton.SetActive(false);
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
        Time.timeScale = timeScale;

        // Wake up all rigidbodies
        foreach (Rigidbody rb in activeBodies)
        {
            if (rb != null)
            {
                rb.WakeUp();
                rb.velocity = rb.velocity;
            }
        }

        // Hide menu buttons, show sim controls
        buttonStart.SetActive(false);
        buttonSpawn.SetActive(false);
        buttonPause.SetActive(true);
        buttonStop.SetActive(true);
        buttonSpeed05x.SetActive(true);
        buttonSpeed1x.SetActive(true);
        buttonSpeed2x.SetActive(true);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = timeScale;
        }
    }

    public void SetSpeed05x()
    {
        timeScale = 0.5f;
        if (!isPaused)
            Time.timeScale = timeScale;
    }

    public void SetSpeed1x()
    {
        timeScale = 1f;
        if (!isPaused)
            Time.timeScale = timeScale;
    }

    public void SetSpeed2x()
    {
        timeScale = 2f;
        if (!isPaused)
            Time.timeScale = timeScale;
    }

    public void StopSimulation()
    {
        simulationRunning = false;
        Time.timeScale = 0f;
        isPaused = false;

        panelMainMenu.SetActive(true);
        
        // Show menu buttons again, hide sim controls
        buttonStart.SetActive(true);
        buttonSpawn.SetActive(true);
        buttonPause.SetActive(false);
        buttonStop.SetActive(false);
        buttonSpeed05x.SetActive(false);
        buttonSpeed1x.SetActive(false);
        buttonSpeed2x.SetActive(false);
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
