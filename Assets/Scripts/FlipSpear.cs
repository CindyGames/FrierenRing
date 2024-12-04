using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpear : MonoBehaviour
{
    public GameObject FlipObject;

    // Start is called before the first frame update
    void Start()
    {

        if (transform.parent.transform.parent.tag == "Spawn 1")
        {
            // ���� ������� ��������� �������� �������
            Vector3 localScale = FlipObject.transform.localScale;

            // ������ ���� �������� �� ��� X, ����� �������� ������
            localScale.x = Mathf.Abs(localScale.x) * -1;

            // ��������� ����� �������
            FlipObject.transform.localScale = localScale;
        }
    }


}
