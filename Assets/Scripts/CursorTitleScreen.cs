using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTitleScreen : MonoBehaviour
{
    enum Selection { START, CREDITS, INCREDITS }

    Selection current = Selection.START;
    bool positiveEdge = true;

    void Start()
    {
        moveTo(current);
        this.GetComponent<Animator>().SetFloat("XSpeed", 1);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (positiveEdge)
            {
                current = next(current);
                moveTo(current);
            }
            positiveEdge = false;
        }
        else
        {
            positiveEdge = true;
        }
    }

    private Selection next(Selection sel)
    {
        switch (sel)
        {
            case Selection.START: { return Selection.CREDITS; }
            case Selection.CREDITS: { return Selection.START; }
            case Selection.INCREDITS: { return Selection.INCREDITS; }
        }
        return Selection.START;
    }

    private void moveTo(Selection to)
    {
        this.transform.SetParent(GameObject.Find(SelectionToString(to)).transform, false);
    }

    private string SelectionToString(Selection sel)
    {
        switch (sel)
        {
            case Selection.START: { return "Start"; }
            case Selection.CREDITS: { return "Credits"; }
            case Selection.INCREDITS: { return "In Credits"; }
        }
        return "Start";
    }
}
