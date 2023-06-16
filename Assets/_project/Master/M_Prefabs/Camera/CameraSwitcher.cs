using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public PlayableDirector timeline;
    public Cinemachine.CinemachineVirtualCamera[] virtualCameras;
    private int currentCameraIndex = 0;
    private bool isTimelinePlaying = true;

    private void Start()
    {
        // Deactivate all virtual cameras except the first one
        for (int i = 1; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for arrow key input
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            SwitchToPreviousCamera();
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            SwitchToNextCamera();
        }
    }

    private void SwitchToPreviousCamera()
    {
        // Pause the timeline
        PauseTimeline();

        // Deactivate the current camera
        virtualCameras[currentCameraIndex].gameObject.SetActive(false);

        // Decrement the camera index
        currentCameraIndex--;

        // Wrap around to the last camera if index becomes negative
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = virtualCameras.Length - 1;
        }

        // Activate the new camera
        virtualCameras[currentCameraIndex].gameObject.SetActive(true);

        // Jump to the corresponding timeline time
        timeline.time = (double)currentCameraIndex / virtualCameras.Length * timeline.duration;
        timeline.Evaluate();
    }

    private void SwitchToNextCamera()
    {
        // Pause the timeline
        PauseTimeline();

        // Deactivate the current camera
        virtualCameras[currentCameraIndex].gameObject.SetActive(false);

        // Increment the camera index
        currentCameraIndex++;

        // Wrap around to the first camera if index exceeds the number of cameras
        if (currentCameraIndex >= virtualCameras.Length)
        {
            currentCameraIndex = 0;
        }

        // Activate the new camera
        virtualCameras[currentCameraIndex].gameObject.SetActive(true);

        // Jump to the corresponding timeline time
        timeline.time = (double)currentCameraIndex / virtualCameras.Length * timeline.duration;
        timeline.Evaluate();
    }

    private void PauseTimeline()
    {
        if (isTimelinePlaying)
        {
            timeline.Pause();
            isTimelinePlaying = false;
        }
    }
}
