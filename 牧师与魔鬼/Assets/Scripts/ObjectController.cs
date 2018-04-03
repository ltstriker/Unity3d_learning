using UnityEngine;
using UnityEditor;

public class BoatMove : MonoBehaviour
{
    public bool state;
    Vector3 Tar;

    public BoatMove()
    {
        state = false;
        Tar = new Vector3(-3.5f, 0.75f, 0);
    }

    private void Update()
    {
        if (state == true)
            transform.position = Vector3.MoveTowards(transform.position, Tar, 10 * Time.deltaTime);
        if (transform.position == Tar)
        {
            Tar.x = -Tar.x;
            state = false;
        }
    }

    public void ObjMove()
    {
        if (state == false)
            state = true;
    }
}

public class BoatController
{
    readonly GameObject Boat;
    public BoatMove MoveApi;

    bool pos1;
    bool pos2;

    public BoatController()
    {
        Boat = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject))) as GameObject;
        Boat.name = "Boat";
        pos1 = false;
        pos2 = false;
        MoveApi = Boat.AddComponent(typeof(BoatMove)) as BoatMove;
        Boat.AddComponent(typeof(ClickGUI));
    }

    public void GameOver()
    {
        //
    }

    public Vector3 getEmptyPos()
    {
        Vector3 pos = Boat.transform.position;
        pos.y = 1.5f;
        if (!pos1)
        {
            pos.x += 1;
        }
        else if (!pos2)
        {
            pos.x -= 1;
        }
        else
        {
            pos.z = -1;
        }
        return pos;
    }

    public Vector3 getPosition()
    {
        return Boat.transform.position;
    }

    public void move()
    {
        if (pos1 || pos2)
        {
            MoveApi.ObjMove();
        }
    }

    internal void addCount(bool v)
    {
        if (v)
        {
            pos1 = true;
        }
        else
        {
            pos2 = true;
        }
    }

    internal void delCount(bool v)
    {
        if (v)
        {
            pos1 = false;
        }
        else
        {
            pos2 = false;
        }
    }

    internal void Reset()
    {
        if (Boat.transform.position.x < 0)
        {
            MoveApi.ObjMove();
        }
        pos1 = false;
        pos2 = false;
    }
}

public class SideController
{
    readonly GameObject Side;

    public SideController()
    {
        Side = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Side", typeof(GameObject))) as GameObject;
    }
}

public class Side2Controller
{
    readonly GameObject Side2;
    public int count;

    public Side2Controller()
    {
        Side2 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Side2", typeof(GameObject))) as GameObject;
        count = 0;
    }
    public void Restart()
    {
        this.count = 0;
    }
}

public class ObjectMove : MonoBehaviour
{
    public bool state;
    int id;
    //Vector3 mid;
    Vector3 Tar;

    bool hasParent;
    BoatController Parent;
    Vector3 r_parent;

    public ObjectMove(int _id)
    {
        state = false;
        id = _id;

        hasParent = false;
    }

    private void Update()
    {
        if (state == true)
            transform.position = Vector3.MoveTowards(transform.position, Tar, 10 * Time.deltaTime);
        else if (hasParent)
        {
            transform.position = Parent.getPosition() - r_parent;
        }
        if (transform.position == Tar)
        {
            state = false;
        }
    }

    public void ObjMove(Vector3 target)
    {
        if (state == false)
        {
            state = true;
            Tar = target;
        }
    }

    internal void setParent(bool v, BoatController boat)
    {

        this.hasParent = v;
        Parent = boat;
        r_parent = boat.getPosition() - Tar;
    }

    internal void delParent()
    {
        this.hasParent = false;
    }
}

public class ObjectController
{
    readonly GameObject obj;
    readonly ClickGUI clickGUI;
    public ObjectMove MoveApi;
    int position_inCoast;

    bool on_boat;

    public ObjectController(bool witchone, int p)
    {
        if (witchone)
        {
            obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Man", typeof(GameObject))) as GameObject;
            obj.name = "Man";
            MoveApi = obj.AddComponent(typeof(ObjectMove)) as ObjectMove;
        }
        else
        {
            obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Ghost", typeof(GameObject))) as GameObject;
            obj.name = "Ghost";
            MoveApi = obj.AddComponent(typeof(ObjectMove)) as ObjectMove;
        }
        on_boat = false;
        position_inCoast = p;
        clickGUI = obj.AddComponent(typeof(ClickGUI)) as ClickGUI;
        clickGUI.setController(this);
    }

    public bool onBoat()
    {
        return on_boat;
    }

    public Vector3 getPosition()
    {
        return obj.transform.position;
    }

    public void SetPosition(float val)
    {
        obj.transform.position = new Vector3(val, obj.transform.position.y, obj.transform.position.z);
    }

    public void Move(Vector3 tar)
    {
        MoveApi.ObjMove(tar);
        on_boat = !on_boat;
    }

    internal Vector3 getPosInCoast()
    {
        return new Vector3((obj.transform.position.x > 0 ? 1 : -1) * (6f + position_inCoast * 1.5f), 2, 0);
    }

    public GameObject getObj()
    {
        return obj;
    }

    internal void setParent(bool v, BoatController boat)
    {
        MoveApi.setParent(v, boat);
    }

    internal void Reset()
    {
        MoveApi.delParent();
        MoveApi.ObjMove(new Vector3(6f + position_inCoast * 1.5f, 2, 0));
        on_boat = false;
    }
}