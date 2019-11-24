using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorTitleScreen : MonoBehaviour
{
    public String startScene;

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

        if (Input.GetButtonDown("Jump"))
        {
            select();
        }
    }

    private void select()
    {
        switch (current)
        {
            case Selection.START:
                {
                    SceneManager.LoadScene(startScene, LoadSceneMode.Single);
                    hideUI(GameObject.Find("Canvas").transform);
                    break;
                }
            case Selection.CREDITS:
                {
                    showUI(GameObject.Find("CreditsPanel").transform);
                    current = Selection.INCREDITS;
                    moveTo(current);
                    break;
                }
            case Selection.INCREDITS:
                {
                    hideUI(GameObject.Find("CreditsPanel").transform);
                    current = Selection.CREDITS;
                    moveTo(current);
                    break;
                }
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
            case Selection.INCREDITS: { return "Close Credits"; }
        }
        return "Start";
    }

    private static void showUI(Transform transform)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);
    }

    private static void hideUI(Transform transform)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 100000000);
    }
}