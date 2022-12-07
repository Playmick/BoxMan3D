using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Move : MonoBehaviour {

	public float freq = 10;//частота проверки игрока на движение(сейчас проверка каждые 20 милисекунд)
	public float dist = 8f;//дистанция проверки игрока на движение
	
	public float speed = 1f;//объявляем переменную скорости
	
	public Rigidbody rb;
	
	public Transform CamObj;
	
	public Text X;
	public Text Y;
	public Text Z;
	
	private float Xthrust = 0f;
	private float Ythrust = 7.5f;
	private float Zthrust = 0f;
	
	private float perc = 0;
	private int t = 0;
	private bool isMoving;
	private float power;
	private float power2;
	
	private float PrevX;
	private float PrevY;
	private float PrevZ;
	
	private float CurX;
	private float CurY;
	private float CurZ;
	
	private float difX;
	private float difY;
	private float difZ;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		isMoving = false;
	}

	void Update () 
	{
		
	}

	void FixedUpdate () 
	{
		perc = 0;
		
		CurX = transform.position.x;
		CurY = transform.position.y;
		CurZ = transform.position.z;
		
		//Debug.Log(CamObj.eulerAngles.y);
		//направление
		if (CamObj.eulerAngles.y>270)
		{
			//правая нижняя четверть, где 270 это 0%, а 360 это 100%
			
			perc = (CamObj.eulerAngles.y-270)/90;
			
			Zthrust=4.3f*perc;
			Xthrust= -4.3f*(1f-perc);
		}
		
		if (CamObj.eulerAngles.y>0 && CamObj.eulerAngles.y<90)
		{
			//левая нижняя четверть, где 0 это 0%, а 90 это 100%
			perc = CamObj.eulerAngles.y/90;
			Xthrust=4.3f*perc;
			Zthrust= 4.3f*(1f-perc);
		}
		
		if (CamObj.eulerAngles.y>90 && CamObj.eulerAngles.y<180)
		{
			//левая верхняя четверть, где 90 это 0%, а 180 это 100%
			perc = (CamObj.eulerAngles.y-90)/90;
			
			Zthrust= -4.3f*perc;
			Xthrust= 4.3f*(1f-perc);
		}
		
		if (CamObj.eulerAngles.y<270 && CamObj.eulerAngles.y>180)
		{
			//правая верхняя четверть, где 180 это 0%, а 270 это 100%
			perc = (CamObj.eulerAngles.y-180f)/90;
			
			Xthrust= -4.3f*perc;
			Zthrust= -4.3f*(1f-perc);
		}
		
		
		//сила-высота прыжка
		if(Input.GetAxis("Fire1")==1 && !isMoving)
		{
			power+=Input.GetAxis("Mouse Y");
			power2=0;
		}
		if (power>0 && Input.GetAxis("Fire1")==0 && !isMoving)
			{
				//Debug.Log(power);
				power2=power;
				if(power2>5)
					power2 = 5;
				if(power2<0)
					power2 = 0;
				power=0;
				power2 = power2/5;
			}
		
		/*
		X.text = "X " + Xthrust;
		Y.text = "Y " + Ythrust;
		Z.text = "Z " + Zthrust;
		*/
		if((!isMoving)&&(power2>0))
		{
			rb.AddForce(Xthrust, Ythrust*power2, Zthrust, ForceMode.Impulse);
			
			power2=0;
			isMoving=true;
			PrevX = transform.position.x;
			PrevY = transform.position.y;
			PrevZ = transform.position.z;
			t=0;
			X.text="Двигаемся";
		}
		
		if (t>=freq && isMoving)
			if ((PrevX!=CurX || PrevY!=CurY || PrevZ!=CurZ))
			{
				difX = CurX-PrevX;
				if(difX<0)
					difX=difX*(-1);
				
				difY = CurY-PrevY;
				if(difY<0)
					difY=difY*(-1);	
				
				difZ = CurZ-PrevZ;
				if(difZ<0)
					difZ=difZ*(-1);
				
				if((difX+difY+difZ)<dist)
				{
					isMoving=false;
					X.text="Стоим";
				}
				
				PrevX=CurX; 
				PrevY=CurY;
				PrevZ=CurZ;
				
				t=0;
			}
		t++;
	}
}