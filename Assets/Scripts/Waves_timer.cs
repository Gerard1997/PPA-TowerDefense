using UnityEngine;
using System.Collections;

public class Waves_timer : MonoBehaviour
{
    private float fSpawntime_Acum;
    private GameObject controller;
    private float ffWave_timer;
    public int iWave_waveduration;
    public GameObject Plane_test_enemy;
    public float enemySpawnTime;
    public bool bWave_active = true;

    void Start(){
      controller = GameObject.FindWithTag("MainCamera");
      ffWave_timer = 0;
      fSpawntime_Acum = enemySpawnTime;
    }
    void Update()
    {
      if (controller.GetComponent<WaveScreen>().onPlay){
        if (bWave_active == true)
        {
          ffWave_timer += Time.deltaTime;
          fSpawntime_Acum += Time.deltaTime;
          if (fSpawntime_Acum >= enemySpawnTime)
          {
            Instantiate(Plane_test_enemy, transform.position, Quaternion.identity);
            fSpawntime_Acum = 0;
          }
          if (ffWave_timer > iWave_waveduration)
          {
            bWave_active = false;
          }
        }
      }
    }
}
