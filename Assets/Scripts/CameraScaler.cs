using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f;  // ������� ����������� ������ (��������, 16:9)

    void Start()
    {
        ScaleCamera();
    }

    void ScaleCamera()
    {
        // ������� ����������� ������ ������
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // ������� ����������� ������
        float scaleWidth = windowAspect / targetAspectRatio;

        Camera camera = GetComponent<Camera>();

        // ���� ������� ����������� ������ ������ �������� (����� ����), ������������ ������ ������
        if (scaleWidth >= 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
        else
        {
            // ���� ����� ��� �������� �����������, ��������� ������ ������
            camera.rect = new Rect(0, 0, 1, 1);
        }
    }
}
