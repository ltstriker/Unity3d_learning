using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class gameConstructor : MonoBehaviour {

    const int button_width = 100;
    const int button_height = 100;

    private bool finish;

    private int[,] Matrix = new int[3, 3];
    private bool turn;
    private int count;

	// Use this for initialization
	void Start () {
        this.Reset();
        count = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
    }

    private void OnGUI()
    {

        for (int c1=0;c1<3;c1++)
            for (int c2=0;c2<3;c2++)
            {
                if (GUI.Button(new Rect(c1*button_width, c2 *button_height, button_width, button_height), ""))
                {
                    Add(c1, c2);
                }
                if (Matrix[c1, c2] == -1)
                {
                    GUI.Button(new Rect(c1*button_width, c2 * button_height, button_width, button_height), "○");
                }
                if (Matrix[c1, c2] == 1)
                {
                    GUI.Button(new Rect(c1 *button_width, c2 * button_height, button_width, button_height), "×");
                }
            }
        if (finish)
        {
            GUI.Label(new Rect(350, 350, 200, 100), count>=9?"no winner":(turn ? "× win!" : "○ win!"));
            if (GUI.Button(new Rect(350, 500, 200, 100), "reset"))
            {
                Reset();
            }
        }
    }

    void Add(int x, int y){
        if (finish||Matrix[x, y]!=0)
            return;
        Matrix[x, y] = turn ? -1 : 1;
        int sign = turn ? -1 : 1;
        turn = !turn;
        count++;

        //row
        for(int c1=0;c1<3;c1++)
        {
            if(Matrix[x, c1]!=sign)
            {
                break;
            }
            else
            {
                if(c1==2)
                {
                    finish = true;
                    return;
                }
            }
        }

        //col

        for (int c1 = 0; c1 < 3; c1++)
        {
            if (Matrix[c1, y] != sign)
            {
                break;
            }
            else
            {
                if (c1 == 2)
                {
                    finish = true;
                    return;
                }
            }
        }

        //diamend
        switch (x + y * 3)
        {
            case 0:
            case 8:
                if (Matrix[0, 0] == Matrix[1, 1] && Matrix[2, 2] == Matrix[1, 1] && Matrix[1, 1] == sign)
                {
                    finish = true;
                }
                break;
            case 2:
            case 6:
                if (Matrix[0, 2] == Matrix[1, 1] && Matrix[2, 0] == Matrix[1, 1] && Matrix[1, 1] == sign)
                {
                    finish = true;
                }
                break;
            case 4:
                if ((Matrix[0, 0] == Matrix[1, 1] && Matrix[2, 2] == Matrix[1, 1] && Matrix[0, 0] == sign) ||
                    (Matrix[0, 2] == Matrix[1, 1] && Matrix[2, 0] == Matrix[1, 1] && Matrix[1, 1] == sign))
                {
                    finish = true;
                }
                break;
            default:
                break;
        }

        if((!finish)&&(count>=9))
        {
            finish = true;
        }
    }

    void Reset()
    {
        turn = false;
        finish=false;
        count = 0;
        for(int c1 =0;c1<3;c1++)
        {
            for(int c2=0;c2<3;c2++)
            {
                Matrix[c1,c2] = 0;
            }
        }
    }
}
