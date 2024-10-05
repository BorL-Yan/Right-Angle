using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    public TextMeshProUGUI _text;
    
    [SerializeField] private float _rotationSpeed = 100f;
    
    
    [SerializeField] private float m_valueRightDistance;
    [SerializeField] private Vector3 m_fromTheRightAngle;
    [SerializeField] private float m_endGameTimer;
    
    [SerializeField] private Color m_startColor;
    [SerializeField] private Color m_endColor;
    
    private Material _material;
    private bool _isDraging;
    private bool _endGame;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.SetColor("_Color", m_startColor);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Input.touchCount < 1 && !_endGame)
        {
            RotateObject();
            _isDraging = true;
        }
        else
        {
            _isDraging = false;
        }
    }
    void RotateObject()
    {
        // Получаем движение мыши по осям X и Y
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        Vector3 rotationForvord = new Vector3(-mouseY, -mouseX, 0).normalized;
        
        transform.Rotate(rotationForvord, _rotationSpeed * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        if (_isDraging)
        {
            Vector3 rotation = this.gameObject.transform.eulerAngles;
            _text.text = Distance(rotation).ToString();
            
            if (Distance(rotation) < m_valueRightDistance)
            {
                _text.text = "End Game";
                _endGame = true;
                StartCoroutine(EndGame());
            }
        }
    }

    private IEnumerator EndGame()
    {
        float time = m_endGameTimer;
        float Timer = 0;
        Vector3 toRotation = this.gameObject.transform.eulerAngles;
        
        while (Timer < time)
        {
            this.gameObject.transform.rotation=Quaternion.Euler(Vector3.Lerp(toRotation, m_fromTheRightAngle, Timer/time));
            _material.SetColor("_Color", Color.Lerp(m_startColor, m_endColor, Timer/time));
            yield return null;
            Timer += Time.deltaTime;
        }
        this.gameObject.transform.rotation = Quaternion.Euler(m_fromTheRightAngle);
    }

    private float Distance( Vector3 to)
    {
        return Mathf.Abs((to - m_fromTheRightAngle).magnitude);
    }


    
}
