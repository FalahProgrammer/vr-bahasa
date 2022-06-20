using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControlBehaviour : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float _speed;
    public int mode;

    void Update () {

        if (mode == 1)
        {
            //MAJU

            Player.Translate(Vector3.forward * _speed *Time.deltaTime);


            Debug.Log(mode);
        }

        if (mode == 2)
        {
            //MUNDUR

            Player.Translate(-Vector3.forward * _speed * Time.deltaTime);

            Debug.Log(mode);
        }

        if (mode == 3)
        {
            //KANAN

            Player.Translate(Vector3.right * _speed * Time.deltaTime);

            Debug.Log(mode);
        }

        if (mode == 4)
        {
            //KIRI

            Player.Translate(-Vector3.right * _speed * Time.deltaTime);

            Debug.Log(mode);
        }
        if (mode == 5)
        {
            //ATAS

            Player.Translate(-Vector3.down * _speed * Time.deltaTime);

            Debug.Log(mode);
        }

        if (mode == 6)
        {
            //DOWN

            Player.Translate(Vector3.down * _speed * Time.deltaTime);

            Debug.Log(mode);
        }

    }

    public void MovePlayer(int i)
    {
        mode = i;
    }

    public void StopPlayer()
    {
        mode = 0;
    }
}
