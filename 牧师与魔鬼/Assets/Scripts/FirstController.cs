using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController: MonoBehaviour, ISceneController, IuserAction
{
    GameObject River;
    Side2Controller Coast_left;
    SideController Coast_right;
    BoatController Boat;
    UserGUI userGUI;

    ObjectController[] Obj;

    void Awake()
    {
        Director director = Director.getInstance();
        director.SetFPS(60);
        director.CurrentController = this;
        Obj = new ObjectController[6];
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        director.CurrentController.LoadResources();
    }

    public void LoadResources ()
    {
        River = Instantiate(Resources.Load("Prefabs/River", typeof(GameObject))) as GameObject;
        Coast_left = new Side2Controller();
        Coast_right = new SideController();
        Boat = new BoatController();

        for(int c1=0;c1<3;c1++)
        {
            ObjectController new_obj = new ObjectController(true, c1);
            new_obj.SetPosition(6 + 1.5f * c1);
            Obj[c1] = new_obj;
        }

        for (int c1 = 3; c1 < 6; c1++)
        {
            Obj[c1] = new ObjectController(false, c1);
            Obj[c1].SetPosition(6 + 1.5f * c1);
        }
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }

    public void GameOver(int res)
    {
        userGUI.status = res;
    }

    public void Restart()
    {
        Boat.Reset();

        for(int c1=0;c1<6;c1++)
        {
            Obj[c1].Reset();
        }
    }

    public void moveBoat()
    {
        Boat.move();
        this.CheckeOver();
    }

    public void moveObject(ObjectController obj)
    {
        if(!Boat.MoveApi.state&&!obj.MoveApi.state)
        {
            if(!((Boat.getPosition().x<0)^(obj.getPosition().x<0)))
            {
                bool parent = false;
                Vector3 pos = Boat.getEmptyPos();
                if(obj.onBoat())
                {
                    pos = obj.getPosInCoast();
                    Boat.delCount(Boat.getPosition().x < obj.getPosition().x);
                    if(Boat.getPosition().x<0)
                    {
                        Coast_left.count++;
                    }
                }
                else
                {
                    if (Boat.getEmptyPos().z == -1)
                        return;
                    Boat.addCount(Boat.getPosition().x < pos.x);
                    parent = true;
                    if (Boat.getPosition().x < 0)
                    {
                        Coast_left.count--;
                    }
                }
                if(pos.z == -1)
                {
                    return;
                }
                obj.Move(pos);
                obj.setParent(parent, Boat);
                this.CheckeOver();
            }
        }
    }

    private void CheckeOver()
    {
         //check for win
        if(Coast_left.count ==6)
        {
            GameOver(2);
        }

        //check side
        int side=0, side2=0;
        bool sideHasMan = false, side2HasMan = false;
        for (int c1 = 0; c1 < 6; c1++)
        {
            if (Obj[c1].getPosition().x <= -6)
            {
                if(Obj[c1].getObj().name == "Man")
                {
                    side2++;
                    side2HasMan = true;
                }
                else
                {
                    side2--;
                }
            }
            else if(Obj[c1].getPosition().x >= 6)
            {
                if (Obj[c1].getObj().name == "Man")
                {
                    side++;
                    sideHasMan = true;
                }
                else
                {
                    side--;
                }
            }
        }

        if (Boat.MoveApi.state)
        {
            if((side<0&&sideHasMan)||(side2<0&&side2HasMan))
            {
                GameOver(1);
            }
        }
        else if (Boat.getPosition().x > 0)
        {
            int sideSum = -side - side2;
            if(sideSum + side < 0 &&sideHasMan)
            {
                GameOver(1);
            }
        }
        else
        {
            int sideSum = -side - side2;
            if (sideSum + side2 < 0 &&side2HasMan)
            {
                GameOver(1);
            }
        }

    }
}

