using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField]
    Transform[] _points;

    [SerializeField]
    float _speed;

    [SerializeField]
    bool _inOrder = true;

    float _minDistance = 0.1f;

    int index = 0;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        transform.position = _points[index].position;
    }

    void MoveToNextPoint()
    {
        float distance = Vector3.Distance(transform.position, _points[index].position);

        if (distance > _minDistance) return;

        if(_inOrder)
        {
            index++;
            if (index > _points.Length - 1) index = 0;
        }
        else
        {
            index--;
            if (index < 0) index = _points.Length - 1;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) collision.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[index].position, _speed * Time.deltaTime);
        MoveToNextPoint();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // 처음 선부터 마지막 선까지 연결해줌
        for (int i = 0; i < _points.Length; i++)
        {
            if(i + 1 == _points.Length)
            {
                Gizmos.DrawLine(_points[_points.Length - 1].position, _points[0].position);
            }
            else
            {
                Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
            }
        }
        
    }
}
