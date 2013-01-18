using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NewDLL;


[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// Base class
/// </summary>
public class BaseClass : MonoBehaviour
{
    #region variable

    [SerializeField]
    protected Vector3 dir;
    protected Vector3 start;
    protected Vector3 end;
    protected Vector3 pos;
    protected Transform _transform;
    protected Rigidbody _rigidbody;

    // Line and dots variables / variables pour ligne et points
    private LineRendererScript line;
    DotScript dot;

    protected bool ready ;
	protected bool thrown;
	protected bool flying;

    protected float force;
    protected float dist;

    //Camera Variables
    protected Camera other;
    CameraScript cam;
    //protected RaycastHit hit;

    #endregion
    #region Start
    /// <summary>
    /// Start function to initialize variables
    /// Is called once before the first Update
    /// </summary>
    public virtual void Start()
    {
        //Cache variables
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();

        //Set Line renderer variables / Definit les variables du Line renderer
        line = GameObject.Find("Renderer").GetComponent<LineRendererScript>();

        //Get camera / Ttrouver la caméra
        other = (Camera)FindObjectOfType(typeof(Camera));
        cam = other.GetComponent<CameraScript>();
        dot = other.GetComponent<DotScript>();
        GameManager.ball = 2;
		force  = 500;
        SetStart();
    }
    #endregion
    #region Update
    /// <summary>
    /// Update function is called once per frame
    /// Retrieve input to be sent to the FixedUpdate
    /// </summary>
     public virtual void Update()
    {
        // Reload level 
        if (Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(0);

        if (GameManager.ball >= 0)           //Do we still have balls
        {

            if (!thrown)                    // Is the ball gone
            {     							
                if (Input.GetMouseButton(0))
                {
					RaycastHit hitI;
                    if (Physics.Raycast(other.ScreenPointToRay(Input.mousePosition), out hitI))
                    {
                        pos = hitI.point;
                        pos.z = 1f;
                    }
                    line.DrawLine(_transform.position, pos);
                }

                if (Input.GetMouseButtonUp(0))
                {
					RaycastHit hitII;
                    if (Physics.Raycast(other.ScreenPointToRay(Input.mousePosition), out hitII))
                    {
                        end = hitII.point;
                        end.z = 1f;
                    }
                    dir = NewClass.GetDirection(_transform.position,end);
                    ready = true;
                    line.EnableLine(false);
                    thrown = true;
					flying = true;
                    dot.StartDrawDot();
                }
            }
        }
        // Reset fonction / Fonction pour recommencer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.ball -= 1;
            _transform.position = start;
            SetStart();
            dot.RemoveDots();
        }
    }
    #endregion
    #region FixedUpdate
    public virtual void FixedUpdate()
    {
        if(ready){
			_rigidbody.isKinematic = false;
			_rigidbody.AddForce(dir*force);
			ready = false;
		}
    }
    #endregion
    #region fonctions

    //The ball collides with anything we want to stop the dots and place the camera in the center
    // la balle a touché quelque chose, on arrête les points rouges, on place la camera au centre 
    void OnCollisionEnter()
    {
        dot.StopDrawDot();
        cam.center = true;
        flying = false;
    }

    protected virtual void SetStart()
    {
        thrown = false;
        _rigidbody.isKinematic = true;
        ready = false;
        cam.center = false;
        start = _transform.position;
        flying = false;
    }
    #endregion
}
