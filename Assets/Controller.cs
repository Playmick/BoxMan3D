using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform target; //цель или игрок

    public float speed; //скорость приближения камеры к игроку/цели
	public float turnSpeed; //"чувствительность" камеры
	
	
	void Start()
	{
		transform.eulerAngles = new Vector3(0, 0, 0);
	}
	
	void Update()
	{
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);//блокируем наклон камеры
	}
	
	void FixedUpdate()
    {
		
		//Vector3.Lerp(начальная позиция, конечная, прогресс); движение из точки А в точку Б
        transform.position = Vector3.Lerp(transform.position, target.position, speed*Time.deltaTime);//плавное приближение камеры к игроку/цели
		transform.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed, 0);//движение 3d камеры по горизонтали, а точнее вращение объекта к которому привязана камера
		if( (transform.eulerAngles.x<70 || transform.eulerAngles.x>290 || (transform.eulerAngles.x>70&&Input.GetAxis("Mouse Y")>0&&transform.eulerAngles.x<100) || (transform.eulerAngles.x>110&&Input.GetAxis("Mouse Y")<0&&transform.eulerAngles.x<290))&&(Input.GetAxis("Fire 4")==1f))
			transform.Rotate(-Input.GetAxis("Mouse Y") * turnSpeed, 0, 0);//движение 3d камеры по вертикали
		
		
		//Debug.Log(Input.GetAxis("Fire1"));
		//if(Input.GetMouseButtonUp(0))
		//	Debug.Log("Отпустили");
		//Debug.Log(transform.eulerAngles.x);
		//if(transform.eulerAngles.z>5 || transform.eulerAngles.z<355)
			
		
		
		
		
    }
}