using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {
    GameObject Sun;
    GameObject Water;
    GameObject Gold;
    GameObject Earth, Month;
    GameObject Mars;
    GameObject Wood, 
                      Solid, 
                      Up, 
                      Sea;



	// Use this for initialization
	void Start () {
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        Sun = Instantiate(Resources.Load("Prefabs/Sun", typeof(GameObject)),Vector3.zero, Quaternion.identity) as GameObject;
        Water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), new Vector3(25, 0, 0), Quaternion.identity) as GameObject;
        Gold = Instantiate(Resources.Load("Prefabs/Gold", typeof(GameObject)), new Vector3(38, 0, 0), Quaternion.identity) as GameObject;
        Earth = Instantiate(Resources.Load("Prefabs/Earth", typeof(GameObject)), new Vector3(55, 0, 0), Quaternion.identity) as GameObject;
        Month = Instantiate(Resources.Load("Prefabs/Month", typeof(GameObject)), new Vector3(62, 0, 0), Quaternion.identity) as GameObject;
        Mars = Instantiate(Resources.Load("Prefabs/Mars", typeof(GameObject)), new Vector3(80, 0, 0), Quaternion.identity) as GameObject;
        Wood = Instantiate(Resources.Load("Prefabs/Wood", typeof(GameObject)), new Vector3(110, 0, 0), Quaternion.identity) as GameObject;
        Solid = Instantiate(Resources.Load("Prefabs/Solid", typeof(GameObject)), new Vector3(158, 0, 0), Quaternion.identity) as GameObject;
        Up = Instantiate(Resources.Load("Prefabs/Up", typeof(GameObject)), new Vector3(190, 0, 0), Quaternion.identity) as GameObject;
        Sea = Instantiate(Resources.Load("Prefabs/Sea", typeof(GameObject)), new Vector3(250, 0, 0), Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        Water.transform.RotateAround(Sun.transform.position, new Vector3(10, 100, 0), 40 * Time.deltaTime);
        Gold.transform.RotateAround(Sun.transform.position, new Vector3(-10, 100, 0), 15 * Time.deltaTime);
        Earth.transform.RotateAround(Sun.transform.position, Vector3.up, 10 * Time.deltaTime);
        Month.transform.RotateAround(Earth.transform.position, new Vector3(0, 1, 0), 359 * Time.deltaTime);
        Mars.transform.RotateAround(Sun.transform.position, new Vector3(5, 100, 0), 5 * Time.deltaTime);
        Wood.transform.RotateAround(Sun.transform.position, new Vector3(8, 95, 0), 9 * Time.deltaTime);
        Solid.transform.RotateAround(Sun.transform.position, new Vector3(-5, 100, 0), 8 * Time.deltaTime);
        Up.transform.RotateAround(Sun.transform.position, new Vector3(-15, 100, 0), 5 * Time.deltaTime);
        Sea.transform.RotateAround(Sun.transform.position, new Vector3(-8, 100, 0), 2 * Time.deltaTime);

        Earth.transform.Rotate(Vector3.up * Time.deltaTime * 30);
    }
}
