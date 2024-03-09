using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameItem : MonoBehaviour
{
    public TileCell cell;

    public TileState state { get; private set; }

    public int number { get; private set; }

    private Image background;
    private TextMeshProUGUI text;


    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        background.color = state.backgroundColor;
        text.color = state.textColor;

        text.text = number.ToString();
    }

    public void MoveTo(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.item = null;
        }

        this.cell = cell;
        cell.item = this;

        StartCoroutine(Animate(cell.transform.position, false));
    }

    public void Merge(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.item = null;
        }

        this.cell = null;
        StartCoroutine(Animate(cell.transform.position, true));
    }
    

    private IEnumerator Animate(Vector3 to, bool merging)
    {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        if (merging)
        {
            Destroy(gameObject);
        }
    }

}
