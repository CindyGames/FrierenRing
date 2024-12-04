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
            // Берём текущие локальные масштабы объекта
            Vector3 localScale = FlipObject.transform.localScale;

            // Меняем знак масштаба по оси X, чтобы флипнуть объект
            localScale.x = Mathf.Abs(localScale.x) * -1;

            // Применяем новый масштаб
            FlipObject.transform.localScale = localScale;
        }
    }


}
